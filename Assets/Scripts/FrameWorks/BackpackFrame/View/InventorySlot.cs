using FrameWorks.BackpackFrame.ItemData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWorks.BackpackFrame.View
{
	public class InventorySlot : MonoBehaviour
	{
		public Image IconImage { get; private set; }
		private TextMeshProUGUI quantityText;
		private void Awake()
		{
			IconImage = GetComponent<Image>();
			quantityText = GetComponentInChildren<TextMeshProUGUI>();
			
		}
		public void UpdateSlot(Item item)
		{
			if(item != null){
				IconImage.sprite = item.icon;
				quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
				IconImage.color = Color.white;
			}
			else
			{
				ClearSlot();
			}
		}

		public void ClearSlot()
		{
			IconImage.color = new Color(1,1,1,0);
			quantityText.text = "";
		}
		
	}
}
