using Cysharp.Threading.Tasks;
using Events;
using Extension;
using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.Systems;
using FrameWorks.BackpackFrame.View;
using FrameWorks.Template;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Presenter
{
	public class SlotClickPresenter : VObject<InventorySlotContext>, IStartable
	{
		
		[Inject]
		private InventorySlot slot;
		
		[Inject]
		private ClickSystem clickSystem;
		
		void IStartable.Start()
		{
			InitializeView();
		}

		private void InitializeView()
		{
			slot.IconImage.OnPointerDownAsObservable()
				.Subscribe(OnClick)
				.AddTo(Context.GetCancellationTokenOnDestroy());
		}

		private void OnClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				clickSystem.slotClick?.OnNext(slot.transform.GetSiblingIndex());
			}
		}
	}
}