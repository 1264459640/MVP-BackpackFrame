using FrameWorks.BackpackFrame.Presenter;
using FrameWorks.BackpackFrame.View;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Context
{
	public class InventorySlotContext : LifetimeScope
	{
		private InventorySlot InventorySlot => GetComponent<InventorySlot>();
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponent(InventorySlot);
			builder.RegisterComponentInHierarchy<InventoryView>().AsImplementedInterfaces();
			builder.RegisterEntryPoint<SlotClickPresenter>();
		}
	}
}