using UnityEngine;

public class Inventory : Interface_Window
{
	private Equipment equipment;

	private System.Collections.Generic.List<GameObject> inventory_Interface;
	private GameObject inventory_Item;

//	private RectTransform window_Transform;
//	private float window_Half_Width = 232.5f;	//I can't seem to access the RectTransform's stored width and height parameters, so these are used instead
//	private float window_Half_Height = 225.0f;
//	private bool drag_Window = false;
//	private float previous_Update_Mouse_X_Position;
//	private float previous_Update_Mouse_Y_Position;

//	public GameObject inventory_Canvas;

	public System.Collections.Generic.List<Item> items;
	public int maximum_Number_Of_Items;


	public void Start()
	{
		this.initialize_Interface_Window();
//		this.inventory_Canvas.SetActive(false);	//Fix for weird Canvas rendering issues- ensure the canvas is active in the scene
		this.equipment = this.gameObject.GetComponent<Equipment>();
		this.inventory_Interface = new System.Collections.Generic.List<GameObject>();
		this.inventory_Item = Resources.Load<GameObject>("Inventory Item");
//		this.window_Transform = this.inventory_Canvas.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
	}

	/*public void Update()
	{
		if(this.drag_Window == true && this.inventory_Canvas.transform.parent.GetChild(0) == this.inventory_Canvas.transform)
		{
			this.window_Transform.anchoredPosition = new Vector2(this.window_Transform.anchoredPosition.x + (Input.mousePosition.x - this.previous_Update_Mouse_X_Position), this.window_Transform.anchoredPosition.y + (Input.mousePosition.y - this.previous_Update_Mouse_Y_Position));
		}
		if(Input.GetMouseButtonDown(0))
		{
			//If the click is within the current window's rectangle...
			if(this.inventory_Canvas.activeSelf == true && Input.mousePosition.x >= this.window_Transform.position.x - this.window_Half_Width && Input.mousePosition.x <= this.window_Transform.position.x + this.window_Half_Width && Input.mousePosition.y >= this.window_Transform.position.y - this.window_Half_Height && Input.mousePosition.y <= this.window_Transform.position.y + this.window_Half_Height)
			{
				this.inventory_Canvas.transform.SetAsFirstSibling();
				this.inventory_Canvas.SetActive(false);					//Fix for weird Canvas rendering issues
				this.inventory_Canvas.SetActive(true);
				this.drag_Window = true;
			}
//			else if()//Add dragging window when menu bar clicked and held instead of the entire box
//			{

//			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			this.drag_Window = false;
		}
		this.previous_Update_Mouse_X_Position = Input.mousePosition.x;
		this.previous_Update_Mouse_Y_Position = Input.mousePosition.y;
	}*/

	public void add_Item(Item item_To_Add)
	{
		if(this.is_Full() == false)
		{
			this.items.Add(item_To_Add);
			this.inventory_Interface.Add((GameObject)Object.Instantiate(this.inventory_Item));
			this.inventory_Interface[this.inventory_Interface.Count - 1].transform.SetParent(this.window_Transform);
			this.inventory_Interface[this.inventory_Interface.Count - 1].transform.localPosition = new Vector3(-110.0f + (220.0f * ((this.inventory_Interface.Count - 1) / 15)), 100.0f - ((this.inventory_Interface.Count - 1)% 15) * 20.0f, 0.0f);
			this.inventory_Interface[this.inventory_Interface.Count - 1].GetComponentInChildren<UnityEngine.UI.Text>().text = "Item " + this.items.Count + ": " + item_To_Add.item_Name;
			this.inventory_Interface[this.inventory_Interface.Count - 1].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
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
				this.inventory_Interface[i].transform.localPosition = new Vector3(-110.0f + (220.0f * (i / 15)), 100.0f - (i % 15) * 20.0f, 0.0f);
				this.inventory_Interface[i].GetComponentInChildren<UnityEngine.UI.Text>().text = "Item " + (i + 1) + ": " + this.items[i].item_Name;
				this.inventory_Interface[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
				int inventory_Index = i;
				this.inventory_Interface[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => this.equipment.equip_Item(inventory_Index));
			}
		}
	}

	public bool is_Full()
	{
		return this.items.Count >= this.maximum_Number_Of_Items;
	}

/*	public void toggle_Inventory()
	{
		if(this.inventory_Canvas.activeSelf == false)
		{
			this.inventory_Canvas.transform.SetAsFirstSibling();
		}
		this.inventory_Canvas.SetActive(!this.inventory_Canvas.activeSelf);
		this.drag_Window = false;
	}*/
}
