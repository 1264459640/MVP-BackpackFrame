using FrameWorks.BackpackFrame.Model;
using FrameWorks.BackpackFrame.Presenter;
using FrameWorks.BackpackFrame.View;
using Unity.VisualScripting;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.BackpackFrame.Context
{
	public class InventoryContext : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterFactory(() => new InventoryModel(40)).AsSelf();
			builder.RegisterComponentInHierarchy<InventoryView>().AsImplementedInterfaces();
			builder.RegisterEntryPoint<InventoryPresenter>();

		}

		
	}
}