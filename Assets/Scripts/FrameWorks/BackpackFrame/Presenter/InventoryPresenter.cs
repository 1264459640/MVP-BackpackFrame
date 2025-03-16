using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using Events;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.BackpackFrame.Model;
using FrameWorks.BackpackFrame.Systems;
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
		[Inject] private readonly InventoryModel model;
		[Inject] private readonly IInventoryView view;
		[Inject] private readonly DragSystem dragSystem;
		[Inject] private readonly ClickSystem clickSystem;
		
		private bool isDragging;
		
		private readonly List<int> changedSlots = new();
		
		public async void Start()
		{
			await UniTask.WaitUntil(() => Context != null);
			view.Initialize(model.maxSlotCount);
			InputManager.Instance.Cancel.Subscribe(_ => UpdateView()).AddTo(Context.GetCancellationTokenOnDestroy());
			model.ItemList.Subscribe(_ => UpdateView())
				.AddTo(Context.GetCancellationTokenOnDestroy());
			clickSystem.slotClick.Subscribe(ClickInput)
				.AddTo(Context.GetCancellationTokenOnDestroy());
			clickSystem.cancelDrag
				.Subscribe(x =>
				{
					if (isDragging) 
						AddItem(x);
				})
				.AddTo(Context.GetCancellationTokenOnDestroy());
			dragSystem.dragItem
				.Select(x => x != null)
				.Where(x => x == false)
				.Subscribe(x => isDragging = x)
				.AddTo(Context.GetCancellationTokenOnDestroy());
		}
		
		
		//TODO: 只更新更改过的Slot
		private void UpdateView()
		{
			for(var i=0; i<view.SlotCount(); i++)
			{
				var item = model.GetItem(i);
				view?.UpdateSlot(i, item);
			}
		}
		private void UpdateChangedSlots() {
			foreach (var index in changedSlots) {
				view.UpdateSlot(index, model.GetItem(index));
			}
		}
		
		public bool AddItem(Item item) => model.AddItem(item);
		
		public void RemoveItem(int index) => model.RemoveItem(index);
		
		private Item GetItem(int index) => model.GetItem(index);

		private bool TryMergeItems(Item draggedItem, int targetIndex, out Item mergedItem) => model.MergeItems(draggedItem, targetIndex, out mergedItem);

		private Item SwapItems(Item draggedItem, int index2) => model.SwapItems(draggedItem, index2);
		
		private void ClickInput(int index)
		{
			dragSystem.dragItem.Value = HandleDrop(dragSystem.dragItem.Value, index);
			isDragging = dragSystem.IsDragging;
		}
		
		
		private Item HandleDrop(Item draggedItem, int targetIndex)
		{
			return TryMergeItems(draggedItem, targetIndex, out var mergedItem)
				? mergedItem
				: SwapItems(draggedItem, targetIndex);
		}
		
		
	}
}
