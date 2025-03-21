using System;
using System.Collections.Generic;
using System.Linq;
using FrameWorks.BackpackFrame.ItemData;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace FrameWorks.BackpackFrame.Model
{
	public class InventoryModel : MonoBehaviour
	{
		public ReactiveProperty<List<Item>> ItemList { get; private set; }
		public int maxSlotCount = 40;
		

		private void Awake()
		{
			ItemList = new ReactiveProperty<List<Item>>(Enumerable.Repeat<Item>(null, maxSlotCount).ToList());
		}

		/// <summary>
		/// 尝试向背包中添加一个物品。
		/// </summary>
		/// <param name="otherItem">要添加的物品。</param>
		/// <returns>如果成功添加物品，则返回true；否则，返回false。</returns>
		public bool AddItem(Item otherItem)
		{
			if (ItemList.Value
			    .Where(item => item != null)  // 过滤掉列表中为null的物品
			    .Where(item => item.Merge(otherItem))  // 进一步过滤出可以与otherItem合并的物品
			    .Any(_ => otherItem.quantity == 0))  // 检查是否有物品的数量为0，这表明otherItem的数量已经通过合并操作归零
			{
				ItemList.Value.Remove(otherItem); // 从列表中移除数量归零的otherItem
				ItemList.ForceNotify();
				return true;  // 返回true，表示添加成功
			}
	
			// 如果没有物品可以合并，尝试找到一个空位来放置otherItem
			var index = GetEmptyIndex(); // 获取第一个空位的索引
			if (index == -1) return false; // 背包已满，无法添加物品
			// 如果找到空位
			ItemList.Value[index] = otherItem; // 将otherItem放入空位
			ItemList.ForceNotify();
			return true; // 返回true表示添加成功

		}


		public void RemoveItem(int index)
		{
			ItemList.Value[index] = null;
			ItemList.ForceNotify();
		}
		public Item GetItem(int index)
		{
			return ItemList.Value[index];
		}

		public Item SwapItems(Item draggedItem, int targetIndex)
		{
			(draggedItem, ItemList.Value[targetIndex]) = (ItemList.Value[targetIndex], draggedItem);
			
			ItemList.ForceNotify();
			return draggedItem;
		}

		/// <summary>
		/// 合并两个物品栏中的物品。
		/// </summary>
		/// <param name="draggedItem"></param>
		/// <param name="targetIndex">目标物品的索引。</param>
		/// <returns>如果合并成功且拖动的物品数量为0，则返回true，否则返回false。</returns>
		public bool MergeItems(Item draggedItem, int targetIndex, out Item mergedItem)
		{
			var targetItem = GetItem(targetIndex);
			if (targetItem == null || draggedItem == null)
			{
				mergedItem = draggedItem;
				return false;
			}

			try
			{
				var canMerge = targetItem.Merge(draggedItem);
				var success = canMerge && draggedItem.quantity == 0;
				mergedItem = success ? null : draggedItem;
				ItemList.ForceNotify();
				return success;
			}
			catch (Exception ex)
			{
				// 记录异常信息或进行其他处理
				Debug.Log($"Merge failed: {ex.Message}");
				mergedItem = draggedItem;
				return false;
			}
		}
		
		private int GetEmptyIndex()
		{
			for (var i = 0; i < ItemList.Value.Count; i++)
			{
				if (ItemList.Value[i] == null)
				{
					return i;
				}
			}

			return -1;
		}

		
	}
}
