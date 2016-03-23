using UnityEngine;

public class Inventory : MonoBehaviour
{
	private Equipment equipment;

	public System.Collections.Generic.List<Item> items;

	public int maximum_Number_Of_Items;

	public void Start()
	{
//		this.items = new System.Collections.Generic.List<Item>();
		this.equipment = this.gameObject.GetComponent<Equipment>();
	}

	public void add_Item(Item item_To_Add)
	{
		if(this.is_Full() == false)
		{
			this.items.Add(item_To_Add);
		}
	}

	public void delete_Item(Item item_To_Delete)
	{
		this.items.Remove(item_To_Delete);
	}

	public bool is_Full()
	{
		if(this.items.Count < this.maximum_Number_Of_Items)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
