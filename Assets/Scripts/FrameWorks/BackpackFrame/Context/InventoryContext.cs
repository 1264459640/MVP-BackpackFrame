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
		private InventoryModel Model => gameObject.GetOrAddComponent<InventoryModel>();
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponent(Model);
			builder.RegisterComponentInHierarchy<InventoryView>().AsImplementedInterfaces();
			builder.RegisterEntryPoint<InventoryPresenter>();

		}

		
	}
}