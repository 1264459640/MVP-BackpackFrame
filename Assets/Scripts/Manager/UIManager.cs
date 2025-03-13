using System;
using System.Collections.Generic;
using Events;
using FrameWorks.UIFrame.Base;
using FrameWorks.UIFrame.Screens;
using UnityEngine;

namespace Manager
{
	public class UIManager : MonoSingleton<UIManager>
	{
		private UIScreen backpackScreen;

		public UIScreen CurrentScreen { get; private set; }
		public bool HasUI => history.Count > 0;

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
			backpackScreen = new BackpackScreen(gameObject.transform.Find("BackpackScreen").gameObject);
			RegisterScreens();
			HideScreens();
		}

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}
		private void UIEvents_BackpackShown()
		{
			Show(backpackScreen, true);
		}

		private void UIEvents_ScreenClosed()
		{
			if (HasUI)
			{
				Show(history.Pop(), false);
			}
			else if(CurrentScreen != null)
			{
				Show(null,false);
			}
		}

		private void SubscribeToEvents()
		{
			UIEvents.BackpackShow += UIEvents_BackpackShown;
			UIEvents.ScreenClosed += UIEvents_ScreenClosed;
		}

		private void UnsubscribeFromEvents()
		{
			UIEvents.BackpackShow -= UIEvents_BackpackShown;
			UIEvents.ScreenClosed -= UIEvents_ScreenClosed;
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

		private void Show(UIScreen screen, bool keepInHistory = true)
		{
			if (CurrentScreen != null)
			{
				CurrentScreen.Hide();
				if (keepInHistory)
				{
					history.Push(CurrentScreen);
				}
			}
			screen?.Show();
			CurrentScreen = screen;
		}
		
	}
}
