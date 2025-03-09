using Manager;
using UIFrame.Base;
using UnityEngine;

namespace UIFrame.Screens
{
	public class BackpackScreen : UIScreen
	{
		
		public BackpackScreen(GameObject root) : base(root)
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