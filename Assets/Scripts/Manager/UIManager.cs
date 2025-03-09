using System.Collections.Generic;
using Events;
using UIFrame.Base;
using UIFrame.Screens;
using UnityEngine;

namespace Manager
{
	public class UIManager : MonoSingleton<UIManager>
	{
		private UIScreen backpackScreen;

		public UIScreen CurrentScreen { get; private set; }
		public bool HasUI => history.Count != 0;

		private readonly Stack<UIScreen> history = new();
		private List<UIScreen> screens = new();
		
		public bool isBackPackActive;

		private void OnEnable()
		{
			SubscribeToEvents();
            
			Initialize();
		}

		private void Initialize()
		{
			backpackScreen = new BackpackScreen(GameObject.Find("BackpackScreen"));
			RegisterScreens();
			HideScreens();
		}

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}
		private void UIEvents_BackpackShown()
		{
			Show(backpackScreen);
		}
		public void UIEvents_ScreenClosed()
		{
			if (HasUI)
			{
				Show(history.Pop(), false);
			}
		}

		private void SubscribeToEvents()
		{
			UIEvents.BackpackShow += UIEvents_BackpackShown;
		}

		private void UnsubscribeFromEvents()
		{
			UIEvents.BackpackShow -= UIEvents_BackpackShown;
		}
		private void RegisterScreens()
		{
			screens = new List<UIScreen>
			{
				backpackScreen
			};
		}
		
		private void HideScreens()
		{
			history.Clear();

			foreach (var screen in screens)
			{
				screen.Hide();
			}
		}
		
		public void Show(UIScreen screen, bool keepInHistory = true)
		{
			if (screen == null)
				return;

			if (CurrentScreen != null)
			{
				CurrentScreen.Hide();
				if (keepInHistory)
				{
					history.Push(CurrentScreen);
				}
			}

			screen.Show();
			CurrentScreen = screen;
		}

		// Shows a UIScreen with the keepInHistory always enabled
		public void Show(UIScreen screen)
		{
			Show(screen, true);
		}
	}
}
