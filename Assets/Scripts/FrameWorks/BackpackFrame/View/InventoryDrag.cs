using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWorks.BackpackFrame.View
{
	public class InventoryDrag : MonoBehaviour
	{
		[SerializeField] private Image draggingIcon;
		[SerializeField] private TextMeshProUGUI draggingAmount;

		private void Start()
		{
			HideDraggingIcon();
		}

		private void Update()
		{
			UpdateDraggingPosition(Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePos));
		}
		
		public void UpdateDraggingPosition(Vector2 position) => 
			draggingIcon.rectTransform.position = position;
		
		private void SetDraggingIcon(Sprite icon, string amount)
		{
			draggingIcon.sprite = icon;
			draggingAmount.text = amount;
			draggingIcon.gameObject.SetActive(true);
		}

		private void HideDraggingIcon()
		{ 
			draggingAmount.text = "";
			draggingIcon.gameObject.SetActive(false);
		}
	}
}