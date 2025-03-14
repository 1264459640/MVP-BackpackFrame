using Events;
using Extension;
using FrameWorks.UIFrame.Base;
using Manager;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWorks.UIFrame.Screens.InventoryScreens
{
	public sealed class BackpackScreen : UIScreen
	{
		private Button backButton;
		public BackpackScreen(GameObject root) : base(root)
		{
			RegisterEvent();
		}

		protected override void InitializeComponent()
		{
			base.InitializeComponent();
			backButton = root.GetOrAddComponentInChild<Button>("Back");
		}

		protected override void RegisterEvent()
		{
			backButton.OnPointerDownAsObservable()
				.Subscribe(_ =>
				{
					UIEvents.ScreenClosed?.Invoke();
				})
				.AddTo(disposable);
		}

		protected override void OnDisable()
		{
			UIManager.Instance.isBackPackActive = false;
		}

		protected override void OnEnable()
		{
			UIManager.Instance.isBackPackActive = true;
		}
	}
}