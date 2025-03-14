using System;
using FrameWorks.BackpackFrame.ItemData;
using Manager;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWorks.BackpackFrame.View
{
	public class InventoryView : MonoBehaviour, IInventoryView
	{
		//TODO 改为自动获取
		[SerializeField] private Transform slotsParent;
		[SerializeField] private GameObject slotPrefab;
		[SerializeField] private Image draggingIcon;
		[SerializeField] private TextMeshProUGUI draggingAmount;

		private InventorySlot[] slots;
		
		private int? draggedIndex;
		
		private bool IsDragging => draggedIndex.HasValue;

		public Subject<(int, int)> OnDrop { get; set; } = new();
		public Subject<int> OnGrab { get; set; } = new();

		public int SlotCount() => slots.Length;
		
		

		public void Initialize(int slotCount)
		{
			// 清空旧槽位
			foreach (Transform child in slotsParent)
			{
				Destroy(child.gameObject);
			}

			slots = new InventorySlot[slotCount];

			// 创建新槽位
			for(var i = 0; i < slotCount; i++)
			{
				var slot = Instantiate(slotPrefab, slotsParent).GetComponent<InventorySlot>();
				slot.UpdateSlot(null);
				slots[i] = slot;
			}
		}

		public void UpdateSlot(int index, Item item)
		{
			slots[index].UpdateSlot(item);
		}

		public void ClearSlot(int index)
		{
			slots[index].ClearSlot();
		}
		public void ClearAllSlots()
		{
			foreach (var slot in slots)
			{
				slot.ClearSlot();
			}
		}

		
		
		public void HandleSlotClicked(InventorySlot clickedSlot)
		{
			var index = Array.IndexOf(slots, clickedSlot);
			if (index != -1)
			{
				HandleClickInput(index);
			}
		}
		private void HandleClickInput(int index)
		{
			HandleGrab(index);
		}
		
		private void HandleGrab(int index)
		{
			Debug.Log("HandleGrab");
			if(!slots[index].HasItem())
				return;
			
			OnGrab?.OnNext(index);
			ClearSlot(index);
		}

		
	}
}
