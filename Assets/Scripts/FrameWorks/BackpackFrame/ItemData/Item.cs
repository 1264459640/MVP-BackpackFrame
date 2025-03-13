using System;
using UnityEngine;

namespace FrameWorks.BackpackFrame.ItemData
{
	public class Item
	{
		public int id;
		public int maxQuantity;
		public int quantity;
		public Sprite icon;

		
		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool Merge(Item item)
		{
			if (CanMergeWith(item) && !IsStackMax())
			{
				var totalQuantity = quantity + item.quantity;
				quantity = Math.Min(totalQuantity, maxQuantity);
				item.quantity = Math.Max(0,totalQuantity - maxQuantity);
				return true;
			}
			return false;
		}

		private bool CanMergeWith(Item item)
		{
			var type = GetType();
			var otherType = item.GetType();
			return id == item.id && type == otherType;
		}

		private bool IsStackMax()
		{
			return quantity >= maxQuantity;
		}
	}
	public enum ItemType
	{
		Weapon,
		Consumable,
		Equipment,
		Material,
	}
	
}
