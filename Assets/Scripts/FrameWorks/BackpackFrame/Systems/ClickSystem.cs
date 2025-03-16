using FrameWorks.BackpackFrame.Context;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.Template;
using R3;

namespace FrameWorks.BackpackFrame.Systems
{
	public class ClickSystem : VObject<InventoryRootContext>
	{
		public readonly Subject<int> slotClick = new();
		
		public readonly Subject<Item> cancelDrag = new();
		
		public readonly Subject<bool> isDrag = new();

		
	}
}