using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.Template;
using Manager;
using R3;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Presenter
{
	public class InventoryDragPresenter : VObject<InventoryDragContext>, IStartable
	{
		public readonly ReactiveProperty<Item> dragItem = new(null);
		public bool IsDragging => dragItem.Value != null;
		
		public void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			dragItem
				.Subscribe(x => InputManager.Instance.highestPriority = x != null)
				.AddTo(Context.gameObject.GetCancellationTokenOnDestroy());

		}
		
	}
}