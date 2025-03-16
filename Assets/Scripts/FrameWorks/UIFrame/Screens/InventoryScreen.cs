using System.Collections.Generic;
using FrameWorks.UIFrame.Base;
using FrameWorks.UIFrame.Screens.InventoryScreens;
using Manager;
using UnityEngine;

namespace FrameWorks.UIFrame.Screens
{
	public class InventoryScreen : UIScreen
	{
		private BackpackScreen backpackScreen;
		private DragScreen dragScreen;
		
		public InventoryScreen(GameObject root) : base(root)
		{
			backpackScreen = new BackpackScreen(root.transform.Find("BackpackScreen").gameObject);
			dragScreen = new DragScreen(root.transform.Find("DragScreen").gameObject);
		}

		protected override void RegisterEvent()
		{
			
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