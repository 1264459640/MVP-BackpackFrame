using FrameWorks.BackpackFrame.Presenter;
using FrameWorks.BackpackFrame.Systems;
using FrameWorks.BackpackFrame.View;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Context
{
	public class InventoryRootContext : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<ClickSystem>().AsSelf();
			builder.RegisterEntryPoint<DragSystem>().AsSelf();
			
			builder.RegisterComponentInHierarchy<InventoryDrag>().AsSelf();
		}
	}
}