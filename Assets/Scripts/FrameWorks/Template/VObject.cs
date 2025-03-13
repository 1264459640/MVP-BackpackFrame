using System;
using R3;
using VContainer;
using VContainer.Unity;

namespace FrameWorks.Template
{
	public class VObject<T> where T : LifetimeScope, IDisposable
	{
		#region Properties
    
		[Inject] protected LifetimeScope context { private get; set; }

		protected T Context => context as T;
		

		#endregion
	}
}