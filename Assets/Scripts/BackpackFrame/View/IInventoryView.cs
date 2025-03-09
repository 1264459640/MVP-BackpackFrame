using BackpackFrame.Model;
using R3;

namespace BackpackFrame.View
{
	public interface IInventoryView
	{

		Subject<(int, int)> OnDrop { get; set; }

		public int SlotCount();
		void Initialize(int slotCount);
		void UpdateSlot(int index, Item item);
		void HideDraggingIcon();

	}
}
