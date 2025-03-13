using VContainer;
using VContainer.Unity;

public class TestLifetimeScope2: LifetimeScope
{
	protected override void Configure(IContainerBuilder builder)
	{
		builder.Register<Test2>(Lifetime.Scoped);
	}
}