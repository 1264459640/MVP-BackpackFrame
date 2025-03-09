using System;
using System.Linq;
using BackpackFrame.Model;
using Cysharp.Threading.Tasks;
using Manager;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BackpackFrame.View
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
		private readonly CompositeDisposable disposables = new();

		public int SlotCount() => slots.Length;

		

		private void Update()
		{
			UpdateDraggingPosition(Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePos));
		}

		public void Initialize(int slotCount)
		{
			HideDraggingIcon();
			// 清空旧槽位
			slotsParent.Cast<Transform>().ToList().ForEach(child =>
			{
				var slot = child.GetComponent<InventorySlot>();
				if (slot != null)
				{
					slot.onSlotClicked.Dispose();
				}
				Destroy(child.gameObject);
			});

			slots = new InventorySlot[slotCount];

			// 创建新槽位
			for(int i = 0; i < slotCount; i++)
			{
				var slot = Instantiate(slotPrefab, slotsParent).GetComponent<InventorySlot>();
				slot.UpdateSlot(null);
				slot.onSlotClicked.Subscribe(HandleSlotClicked).AddTo(disposables);
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

		private void SetDraggingIcon(Sprite icon, string amount)
		{
			draggingIcon.sprite = icon;
			draggingAmount.text = amount;
			draggingIcon.gameObject.SetActive(true);
		}
		public void HideDraggingIcon()
		{ 
			draggingAmount.text = "";
			draggingIcon.gameObject.SetActive(false);
		}
		
		public void UpdateDraggingPosition(Vector2 position) => 
			draggingIcon.rectTransform.position = position;

		
		
		private void HandleSlotClicked(InventorySlot clickedSlot)
		{
			int index = Array.IndexOf(slots, clickedSlot);
			if (index != -1)
			{
				HandleClickInput(index);
			}
		}
		private void HandleClickInput(int index)
		{
			if (IsDragging)
				HandleDrop(index);
			else
				HandleGrab(index).Forget();
		}
		
		private async UniTask HandleGrab(int index)
		{
			if(!slots[index].HasItem())
				return;
			draggedIndex = index;
			InputManager.Instance.highestPriority = true;
			await UniTask.WaitUntil(() => IsDragging);
			SetDraggingIcon(slots[index].GetItem().sprite, slots[index].GetItem().amount);
			ClearSlot(index);
		}

		private void HandleDrop(int targetIndex)
		{
			OnDrop.OnNext((draggedIndex.Value, targetIndex));
			InputManager.Instance.highestPriority = false;
			draggedIndex = null;
			HideDraggingIcon();
		}
		private void OnDestroy()
		{
			disposables.Dispose();
		}
	}
}
