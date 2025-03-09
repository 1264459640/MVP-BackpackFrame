using System;
using System.Diagnostics.CodeAnalysis;
using BackpackFrame.Model;
using BackpackFrame.View;
using Cysharp.Threading.Tasks;
using Events;
using Manager;
using R3;
using VContainer;
using VContainer.Unity;

namespace BackpackFrame.Presenter
{
	[SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
	public class InventoryPresenter: IStartable,IDisposable,IInventoryPresenter
	{
		[Inject]
		private InventoryModel model;

		[Inject] 
		private IInventoryView view;

		private readonly CompositeDisposable disposables = new();
		private CompositeDisposable viewDisposables;

		public void Start()
		{
			model.ItemList.Subscribe(_ => UpdateView()).AddTo(disposables);
			InputManager.Instance.Cancel.Subscribe(_ => HandleCancel()).AddTo(disposables);
			InputManager.Instance.BackPack.Subscribe(_ => UIEvents.BackpackShow?.Invoke()).AddTo(disposables);
			
		}

		public void InitializeView()
		{
			viewDisposables = new();
			view?.Initialize(model.maxSlotCount);
			view?.OnDrop.AsObservable().Subscribe(HandleDrop).AddTo(viewDisposables);
			
			UpdateView();
		}
		
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
		
		private void HandleDrop((int draggedIndex, int targetIndex) indices)
		{
			if (TryMergeItems(indices.draggedIndex,indices.targetIndex)) return;
			
			SwapItems(indices.draggedIndex,indices.targetIndex);
		}

		private void HandleCancel()
		{
			InputManager.Instance.highestPriority = false;
			StopDrag();
			UpdateView();
		}

		private void StopDrag()
		{
			view?.HideDraggingIcon();
		}

		public void ViewDispose()
		{
			viewDisposables?.Dispose();
		}
	
		public void Dispose()
		{
			viewDisposables?.Dispose();
			disposables?.Dispose();
		}
	}
}
