using FrameWorks.BackpackFrame.ItemData;
using R3;

namespace FrameWorks.BackpackFrame.View
{
	public interface IInventoryView
	{

		Subject<(int, int)> OnDrop { get; set; }
		public Subject<int> OnGrab { get; set; } 
		public int SlotCount();
		void Initialize(int slotCount);
		void UpdateSlot(int index, Item item);
		void HideDraggingIcon();

		void HandleSlotClicked(InventorySlot slot);
	}
}
