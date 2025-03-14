using System.Collections.Generic;
using FrameWorks.UIFrame.Base;
using FrameWorks.UIFrame.Screens.InventoryScreens;
using UnityEngine;

namespace FrameWorks.UIFrame.Screens
{
	public class InventoryScreen : UIScreen
	{
		private UIScreen backpackScreen;
		private UIScreen dragScreen;
		
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
			
		}

		protected override void OnEnable()
		{
			
		}
	}
}