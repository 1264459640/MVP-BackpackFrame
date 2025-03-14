using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.BackpackFrame.View;
using FrameWorks.Template;
using Manager;
using R3;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Presenter
{
	public class InventoryDragPresenter : VObject<InventoryDragContext>, IStartable
	{
		[Inject]
		private InventoryDrag dragView;
		
		public readonly ReactiveProperty<Item> dragItem = new(null);
		public bool IsDragging => dragItem.Value != null;
		
		public void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			dragItem
				.Select(x => x != null)
				.Subscribe(x => InputManager.Instance.highestPriority = x)
				.AddTo(Context.gameObject.GetCancellationTokenOnDestroy());

		}
		
	}
}