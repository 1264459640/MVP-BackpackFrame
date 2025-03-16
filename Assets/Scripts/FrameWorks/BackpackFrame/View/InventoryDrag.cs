using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.Systems;
using Manager;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FrameWorks.BackpackFrame.View
{
	public class InventoryDrag : MonoBehaviour
	{
		[Inject] private ClickSystem clickSystem;
		
		[SerializeField] private Image draggingIcon;
		[SerializeField] private TextMeshProUGUI draggingAmount;

		private CancellationTokenSource cts;
		
		private void Start()
		{
			HideDraggingIcon();
			clickSystem.isDrag.Subscribe(UpdateDragging).AddTo(gameObject.GetCancellationTokenOnDestroy());
		}

		private void UpdateDragging(bool isDragging)
		{
			StopUpdate();
			if (isDragging)
			{
				StartUpdate(cts.Token).Forget();
			}
		}
		
		private async UniTask StartUpdate(CancellationToken ctsToken)
		{
			while (!cts.IsCancellationRequested)
			{
				UpdateDraggingPosition(Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePos));
				await UniTask.NextFrame(ctsToken);
			}
		}
		
		private void StopUpdate()
		{
			cts?.Cancel();
			cts = null;
			cts = new CancellationTokenSource();
		}

		private void UpdateDraggingPosition(Vector2 position) => 
			draggingIcon.rectTransform.position = position;
		
		public void SetDraggingIcon(Sprite icon, int amount)
		{
			draggingIcon.sprite = icon;
			draggingIcon.color = Color.white;
			draggingAmount.text = amount > 1 ? amount.ToString(): "";
		}

		public void HideDraggingIcon()
		{ 
			draggingIcon.color = Color.clear;
			draggingAmount.text = "";
		}
	}
}