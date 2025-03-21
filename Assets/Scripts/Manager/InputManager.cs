using System;
using Events;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Manager
{
	public class InputManager: MonoSingleton<InputManager> ,IDisposable
	{
		public PlayerInput input;

		private readonly CompositeDisposable disposables = new();
		public Vector2 MousePos => input.UI.Point.ReadValue<Vector2>();
		
		public Observable<InputAction.CallbackContext> Cancel { get; private set; }
		public Observable<InputAction.CallbackContext> BackPack { get; private set; }
		
		public bool highestPriority = false;
		public bool mediumPriority = false;
		
		protected override void Awake()
		{
			base.Awake();
			input = new PlayerInput();
			input.Enable();
		}

		private void Start()
		{

			BackPack = Observable.FromEvent<InputAction.CallbackContext>(
				handler => input.UI.Backpack.performed += handler,
				handler => input.UI.Backpack.performed -= handler)
				.Where(_=> !UIManager.Instance.isBackPackActive);
			BackPack.Subscribe(_ => UIEvents.InventoryShow?.Invoke()).AddTo(disposables);

			Cancel = Observable.FromEvent<InputAction.CallbackContext>(
					handler => input.UI.Cancel.performed += handler,
					handler => input.UI.Cancel.performed -= handler);
			Cancel.Subscribe(_ => HandleCancelPriority()).AddTo(disposables);

		}
		private void HandleCancelPriority()
		{
			if (highestPriority)
			{
				HighestPriorityStream();
			}
			else if (mediumPriority)
			{
				MediumPriorityStream();
			}
			else
			{
				LowestPriorityStream();
			}
		}
		private void HighestPriorityStream()
		{
			
		}

		private void MediumPriorityStream()
		{
			
		}
		
		private static void LowestPriorityStream()
		{
			UIEvents.ScreenClosed?.Invoke();
		}
		
		
		private void OnDisable()
		{
			input.Disable();
		}

		public void Dispose()
		{
			disposables.Dispose();
			input.Dispose();
		}
	}
}