using Cysharp.Threading.Tasks;
using Events;
using Extension;
using FrameWorks.UIFrame.Base;
using Manager;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWorks.UIFrame.Screens.InventoryScreens
{
	public sealed class BackpackScreen 
	{
		private readonly GameObject root;
		
		private Button backButton;
		
		
		public BackpackScreen(GameObject root) 
		{
			this.root = root;
			RegisterEvent();
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			backButton = root.GetOrAddComponentInChild<Button>("Back");
		}

		private void RegisterEvent()
		{
			backButton.OnPointerDownAsObservable()
				.Subscribe(_ =>
				{
					Debug.Log(2);
					UIEvents.ScreenClosed?.Invoke();
				})
				.AddTo(root.GetCancellationTokenOnDestroy());
		}
		
	}
}