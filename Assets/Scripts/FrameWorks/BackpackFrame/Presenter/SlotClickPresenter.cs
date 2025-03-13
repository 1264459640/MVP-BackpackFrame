using Cysharp.Threading.Tasks;
using Extension;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.View;
using FrameWorks.Template;
using R3;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Presenter
{
	public class SlotClickPresenter : VObject<InventorySlotContext>, IStartable
	{
		[Inject]
		private IInventoryView inventoryView;
		[Inject]
		private InventorySlot slot;
		void IStartable.Start()
		{
			InitializeView();
		}

		private void InitializeView()
		{
			slot.IconImage.OnPointerDownAsObservable()
				.Subscribe(_ => inventoryView.HandleSlotClicked(slot))
				.AddTo(Context.gameObject.GetCancellationTokenOnDestroy());
		}
	}
}