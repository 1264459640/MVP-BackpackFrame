using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.BackpackFrame.View;
using FrameWorks.Template;
using Manager;
using R3;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Systems
{
	public class DragSystem : VObject<InventoryRootContext>, IStartable 
	{
		[Inject]
		private InventoryDrag dragView;
		
		[Inject]
		private ClickSystem	clickSystem;

		public readonly ReactiveProperty<Item> dragItem = new(null);
		public bool IsDragging => dragItem.Value != null;
		
		public async void Start()
		{
			await UniTask.WaitUntil(() => Context != null);
			InputManager.Instance.Cancel.Subscribe(_ => OnCancelDrag()).AddTo(Context.GetCancellationTokenOnDestroy());
			dragItem
				.Subscribe(SetDragView)
				.AddTo(Context.GetCancellationTokenOnDestroy());
			dragItem
				.Select(x => x != null)
				.Subscribe(x => InputManager.Instance.highestPriority = x)
				.AddTo(Context.GetCancellationTokenOnDestroy());
		}
		
		private void OnCancelDrag()
		{
			clickSystem.cancelDrag?.OnNext(dragItem.Value);
			dragItem.Value = null;
			dragItem.ForceNotify();
		}
		
		private void SetDragView(Item item)
		{
			clickSystem.isDrag?.OnNext(IsDragging);
			if(item == null)
			{
				dragView.HideDraggingIcon();
				return;
			}
			dragView.SetDraggingIcon(item.icon, item.quantity);
		}
		
	}
}