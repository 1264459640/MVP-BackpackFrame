using FrameWorks.BackpackFrame.Presenter;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Context
{
	public class InventoryDragContext : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<InventoryDragPresenter>().AsSelf();

		}
	}
}