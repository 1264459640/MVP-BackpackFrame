using BackpackFrame.Model;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BackpackFrame.View
{
	public class InventorySlot : MonoBehaviour,IPointerDownHandler
	{
		private Image iconImage;
		private TextMeshProUGUI quantityText;
		
		// public Action<InventorySlot> onSlotClicked;
		public Subject<InventorySlot> onSlotClicked = new();

		private void Awake()
		{
			iconImage = GetComponent<Image>();
			quantityText = GetComponentInChildren<TextMeshProUGUI>();
		}
		public void UpdateSlot(Item item)
		{
			if(item != null){
				iconImage.sprite = item.icon;
				quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
				iconImage.color = Color.white;
			}
			else{
				iconImage.color = new Color(1,1,1,0);
				quantityText.text = "";
			}
		}

		public void ClearSlot()
		{
			iconImage.color = new Color(1,1,1,0);
			quantityText.text = "";
		}

		public (Sprite sprite, string amount) GetItem()
		{
			return (iconImage.sprite, quantityText.text);
		}
		
		public bool HasItem()
		{
			return iconImage.color.a > 0;
		}
		public void OnPointerDown(PointerEventData eventData)
		{
			onSlotClicked?.OnNext(this);
		}
	}
}
