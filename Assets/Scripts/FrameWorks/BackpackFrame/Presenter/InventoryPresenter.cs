using System;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.BackpackFrame.Model;
using FrameWorks.BackpackFrame.View;
using FrameWorks.Template;
using Manager;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Presenter
{
	[SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
	// ReSharper disable once ClassNeverInstantiated.Global
	public class InventoryPresenter : VObject<InventoryContext>, IStartable
	{
		[Inject]
		private readonly InventoryModel model;
		
		[Inject]
		private readonly IInventoryView view;
		
		[Inject]
		private readonly InventoryDragPresenter dragPresenter;


		public async void Start()
		{
			await UniTask.WaitUntil(() => Context != null);
			InputManager.Instance.Cancel.Subscribe(_ => HandleCancel()).AddTo(Context.gameObject.GetCancellationTokenOnDestroy());
			model.ItemList.Subscribe(_ => UpdateView())
				.AddTo(Context.gameObject.GetCancellationTokenOnDestroy());
			view.OnGrab.Subscribe(HandleGrab)
				.AddTo(Context.gameObject.GetCancellationTokenOnDestroy());
			InitializeView();
		}

		private void InitializeView()
		{
			view.Initialize(model.maxSlotCount);
			UpdateView();
		}
		
		//TODO: 只更新更改过的Slot
		private void UpdateView()
		{
			for(var i=0; i<view?.SlotCount(); i++)
			{
				var item = model.GetItem(i);
				view?.UpdateSlot(i, item);
			}
		}
		
		public bool AddItem(Item item)
		{
			var success = model.AddItem(item);
			return success;
		}
		public void RemoveItem(int index)
		{
			model.RemoveItem(index);
		}
		
		private bool TryMergeItems(int draggedIndex, int targetIndex)
		{
			if (model.MergeItems(draggedIndex, targetIndex))
			{
				return true;
			}
			return false;
		}

		private void SwapItems(int draggedIndex, int targetIndex)
		{
			model.SwapItems(draggedIndex, targetIndex);
		}

		private void HandleGrab(int index)
		{
			Debug.Log($"{index}HandleGrab");
			if(dragPresenter.IsDragging) return;
			dragPresenter.dragItem.Value = model.GetItem(index);
		}
		
		private void HandleDrop((int draggedIndex, int targetIndex) indices)
		{
			if (TryMergeItems(indices.draggedIndex,indices.targetIndex)) return;
			
			SwapItems(indices.draggedIndex,indices.targetIndex);
		}

		private void HandleCancel()
		{
			InputManager.Instance.highestPriority = false;
			UpdateView();
		}

		
		
	}
}
