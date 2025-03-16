using FrameWorks.BackpackFrame.ItemData;
using R3;

namespace FrameWorks.BackpackFrame.View
{
	public interface IInventoryView
	{
		
		public int SlotCount();
		void Initialize(int slotCount);
		void UpdateSlot(int index, Item item);

		void ClearSlot(int index);
	}
}
