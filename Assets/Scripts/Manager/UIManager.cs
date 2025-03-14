using System;
using System.Collections.Generic;
using Events;
using FrameWorks.UIFrame.Base;
using FrameWorks.UIFrame.Screens;
using FrameWorks.UIFrame.Screens.InventoryScreens;
using UnityEngine;

namespace Manager
{
	public class UIManager : MonoSingleton<UIManager>
	{
		private UIScreen inventoryScreen;

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
			inventoryScreen = new InventoryScreen(transform.Find("InventoryScreen").gameObject);
			RegisterScreens();
			HideScreens();
		}

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}
		private void UIEvents_InventoryShown()
		{
			Show(inventoryScreen, true);
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
			UIEvents.InventoryShow += UIEvents_InventoryShown;
			UIEvents.ScreenClosed += UIEvents_ScreenClosed;
		}

		private void UnsubscribeFromEvents()
		{
			UIEvents.InventoryShow -= UIEvents_InventoryShown;
			UIEvents.ScreenClosed -= UIEvents_ScreenClosed;
		}
		private void RegisterScreens()
		{
			screens = new List<UIScreen>
			{
				inventoryScreen
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
