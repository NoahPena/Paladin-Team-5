using UnityEngine;

public class Inventory : MonoBehaviour
{
	private Equipment equipment;

	private System.Collections.Generic.List<GameObject> inventory_Interface;
	private GameObject inventory_Item;

	public Transform inventory_Canvas;
	public System.Collections.Generic.List<Item> items;
	public int maximum_Number_Of_Items;


	public void Start()
	{
		this.inventory_Canvas.gameObject.SetActive(false);
		this.equipment = this.gameObject.GetComponent<Equipment>();
		this.inventory_Interface = new System.Collections.Generic.List<GameObject>();
		this.inventory_Item = Resources.Load<GameObject>("Inventory Item");
	}

	public void add_Item(Item item_To_Add)
	{
		if(this.is_Full() == false)
		{
			this.items.Add(item_To_Add);
			this.inventory_Interface.Add((GameObject)Object.Instantiate(this.inventory_Item));
			this.inventory_Interface[this.inventory_Interface.Count - 1].transform.SetParent(this.inventory_Canvas);
			this.inventory_Interface[this.inventory_Interface.Count - 1].transform.localPosition = new Vector3(-80.0f, 250.0f - (this.inventory_Interface.Count - 1) * 25.0f, 0.0f);
			this.inventory_Interface[this.inventory_Interface.Count - 1].GetComponent<UnityEngine.UI.Text>().text = "Item " + this.items.Count + ": " + item_To_Add.item_Type;
			this.inventory_Interface[this.inventory_Interface.Count - 1].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
			Debug.Log(item_To_Add);
			Debug.Log("Index of item: " + (this.items.Count - 1));
			int inventory_Index = this.items.Count - 1;
			this.inventory_Interface[this.inventory_Interface.Count - 1].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => this.equipment.equip_Item(inventory_Index));
		}
	}

	public void delete_Item(Item item_To_Delete)
	{
		int index_Of_Item_To_Delete = this.items.FindIndex(item => item == item_To_Delete);
		if(index_Of_Item_To_Delete >= 0)
		{
			this.items.RemoveAt(index_Of_Item_To_Delete);
			Object.Destroy(this.inventory_Interface[index_Of_Item_To_Delete]);
			this.inventory_Interface.RemoveAt(index_Of_Item_To_Delete);
			for(int i = 0; i < this.inventory_Interface.Count && i < this.items.Count; i++)
			{
				this.inventory_Interface[i].transform.localPosition = new Vector3(-80.0f, 250.0f - i * 25.0f, 0.0f);
				this.inventory_Interface[i].GetComponent<UnityEngine.UI.Text>().text = "Item " + (i + 1) + ": " + this.items[i].item_Type;
				this.inventory_Interface[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
				int inventory_Index = i;
				this.inventory_Interface[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => this.equipment.equip_Item(inventory_Index));
			}
		}
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

	public void toggle_Inventory()
	{
		this.inventory_Canvas.gameObject.SetActive(!this.inventory_Canvas.gameObject.activeSelf);
	}
}
