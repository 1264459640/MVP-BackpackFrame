using System;
using Extension;
using FrameWorks.BackpackFrame.ItemData;
using FrameWorks.BackpackFrame.Presenter;
using InspectorButton;
using SO.ItemDataSO;
using UnityEngine;
using VContainer;

public class Test : MonoBehaviour
{
	[Inject]
	private InventoryPresenter inventoryPresenter;
	
	[SerializeField]
	private ItemDataSO itemDataSO;

	public int quantity = 1;

	public int id = 1;
	
	[InspectorButton("AddItem")]
	private void AddItem()
	{
		var item = itemDataSO.CreatNewItem(ItemType.Consumable, id, quantity);
		
		inventoryPresenter.AddItem(item);
	}
}
