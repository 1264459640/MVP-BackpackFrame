using FrameWorks.BackpackFrame.Presenter;
using FrameWorks.BackpackFrame.View;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Context
{
	public class InventoryDragContext : LifetimeScope
	{
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<InventoryDrag>().AsSelf();
			builder.RegisterEntryPoint<InventoryDragPresenter>().AsSelf();

		}
	}
}