using System;
using System.Collections.Generic;
using FrameWorks.BackpackFrame.ItemData;
using UnityEngine;

namespace SO.ItemDataSO
{
	public class ItemDataSO : ScriptableObject
	{
		public List<ConsumableItem> consumableList;
		public List<WeaponItem> weaponList;
		public List<EquipmentItem> equipmentList;
		public List<MaterialItem> materialList;

		public Item CreatNewItem(ItemType itemType, int id, int quantity)
		{
			var item = itemType switch
			{
				ItemType.Weapon => weaponList[id].Clone() as Item,
				ItemType.Consumable => consumableList[id].Clone() as Item,
				ItemType.Equipment => equipmentList[id].Clone() as Item,
				ItemType.Material => materialList[id].Clone() as Item,
				_ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
			};
			if (item == null) return null;
			item.quantity = quantity;
			return item;
		}
	}
}