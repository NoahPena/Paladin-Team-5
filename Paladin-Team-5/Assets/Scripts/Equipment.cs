using UnityEngine;

public class Equipment : Interface_Window
{
	private Player player;
	private Inventory inventory;

//	private RectTransform window_Transform;
//	private float window_Half_Width = 140.0f;	//I can't seem to access the RectTransform's stored width and height parameters, so these are used instead
//	private float window_Half_Height = 225.0f;
//	private bool drag_Window = false;
//	private float previous_Update_Mouse_X_Position;
//	private float previous_Update_Mouse_Y_Position;

//	public GameObject equipment_Canvas;

	public Item cloak;
	public UnityEngine.UI.Button cloak_Equipment_Button;
	public Item boots;
	public UnityEngine.UI.Button boots_Equipment_Button;
	public Item leggings;
	public UnityEngine.UI.Button leggings_Equipment_Button;
	public Item gloves;
	public UnityEngine.UI.Button gloves_Equipment_Button;
	public Item hat;
	public UnityEngine.UI.Button hat_Equipment_Button;
	public Item bracelets;
	public UnityEngine.UI.Button bracelets_Equipment_Button;
	public Item amulet;
	public UnityEngine.UI.Button amulet_Equipment_Button;
	public Item rings;
	public UnityEngine.UI.Button rings_Equipment_Button;
	public Item shirt;
	public UnityEngine.UI.Button shirt_Equipment_Button;
	public Item earrings;
	public UnityEngine.UI.Button earrings_Equipment_Button;

	public void Start()
	{
		this.initialize_Interface_Window();
//		this.window_Half_Width = 140.0f;
//		this.window_Half_Height = 225.0f;
		this.player = this.gameObject.GetComponent<Player>();
		this.inventory = this.gameObject.GetComponent<Inventory>();
	}

/*	public void Update()
	{
		if(this.drag_Window == true && this.equipment_Canvas.transform.parent.GetChild(0) == this.equipment_Canvas.transform)
		{
			this.window_Transform.anchoredPosition = new Vector2(this.window_Transform.anchoredPosition.x + (Input.mousePosition.x - this.previous_Update_Mouse_X_Position), this.window_Transform.anchoredPosition.y + (Input.mousePosition.y - this.previous_Update_Mouse_Y_Position));
		}
		if(Input.GetMouseButtonDown(0))
		{
			//If the click is within the current window's rectangle...
			if(this.equipment_Canvas.activeSelf == true && Input.mousePosition.x >= this.window_Transform.position.x - this.window_Half_Width && Input.mousePosition.x <= this.window_Transform.position.x + this.window_Half_Width && Input.mousePosition.y >= this.window_Transform.position.y - this.window_Half_Height && Input.mousePosition.y <= this.window_Transform.position.y + this.window_Half_Height)
			{
				this.equipment_Canvas.transform.SetAsFirstSibling();
				this.equipment_Canvas.SetActive(false);					//Fix for weird Canvas rendering issues
				this.equipment_Canvas.SetActive(true);
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

	public void unequip_Item(int item_Type_Integer_Value)
	{
		Item.Types slot_To_Unequip = (Item.Types)item_Type_Integer_Value;
		if(this.inventory != null && this.inventory.is_Full() == false)
		{
			Item item_To_Unequip = null;
			switch(slot_To_Unequip)
			{
				case Item.Types.Cloak:
					item_To_Unequip = this.cloak;
					this.cloak = null;
					break;

				case Item.Types.Boots:
					item_To_Unequip = this.boots;
					this.boots = null;
					break;

				case Item.Types.Leggings:
					item_To_Unequip = this.leggings;
					this.leggings = null;
					break;

				case Item.Types.Gloves:
					item_To_Unequip = this.gloves;
					this.gloves = null;
					break;

				case Item.Types.Hat:
					item_To_Unequip = this.hat;
					this.hat = null;
					break;

				case Item.Types.Bracelets:
					item_To_Unequip = this.bracelets;
					this.bracelets = null;
					break;

				case Item.Types.Amulet:
					item_To_Unequip = this.amulet;
					this.amulet = null;
					break;

				case Item.Types.Rings:
					item_To_Unequip = this.rings;
					this.rings = null;
					break;

				case Item.Types.Shirt:
					item_To_Unequip = this.shirt;
					this.shirt = null;
					break;

				case Item.Types.Earrings:
					item_To_Unequip = this.earrings;
					this.earrings = null;
					break;
			}
			if(item_To_Unequip != null)
			{
				this.player.maximum_Stamina = this.player.maximum_Stamina - item_To_Unequip.stamina_Increase;
				this.player.stamina_Regeneration = this.player.stamina_Regeneration - item_To_Unequip.stamina_Regneration_Increase;
				this.player.health_Regeneration = this.player.health_Regeneration - item_To_Unequip.health_Regeneration_Increase;
				this.player.mana_Regeneration = this.player.mana_Regeneration - item_To_Unequip.mana_Regeneration_Increase;
				this.inventory.add_Item(item_To_Unequip);
			}
		}
	}

	public void equip_Item(int index_Of_Item_To_Equip)
	{
		Item item_To_Equip = null;
		if(index_Of_Item_To_Equip >= 0 && index_Of_Item_To_Equip < this.inventory.items.Count)
		{
			item_To_Equip = this.inventory.items[index_Of_Item_To_Equip];
		}
//		Debug.Log(item_To_Equip);
		if(item_To_Equip != null)
		{
			Item currently_Equipped_Item = null;
			switch(item_To_Equip.item_Type)
			{
				case Item.Types.Cloak:
					currently_Equipped_Item = this.cloak;
					this.cloak = item_To_Equip;
					break;

				case Item.Types.Boots:
					currently_Equipped_Item = this.boots;
					this.boots = item_To_Equip;
					break;

				case Item.Types.Leggings:
					currently_Equipped_Item = this.leggings;
					this.leggings = item_To_Equip;
					break;

				case Item.Types.Gloves:
					currently_Equipped_Item = this.gloves;
					this.gloves = item_To_Equip;
					break;

				case Item.Types.Hat:
					currently_Equipped_Item = this.hat;
					this.hat = item_To_Equip;
					break;

				case Item.Types.Bracelets:
					currently_Equipped_Item = this.bracelets;
					this.bracelets = item_To_Equip;
					break;

				case Item.Types.Amulet:
					currently_Equipped_Item = this.amulet;
					this.amulet = item_To_Equip;
					break;

				case Item.Types.Rings:
					currently_Equipped_Item = this.rings;
					this.rings = item_To_Equip;
					break;

				case Item.Types.Shirt:
					currently_Equipped_Item = this.shirt;
					this.shirt = item_To_Equip;
					break;

				case Item.Types.Earrings:
					currently_Equipped_Item = this.earrings;
					this.earrings = item_To_Equip;
					break;
			}
			inventory.delete_Item(item_To_Equip);
			if(currently_Equipped_Item != null)
			{
				this.player.maximum_Stamina = this.player.maximum_Stamina - currently_Equipped_Item.stamina_Increase;
				this.player.stamina_Regeneration = this.player.stamina_Regeneration - currently_Equipped_Item.stamina_Regneration_Increase;
				this.player.health_Regeneration = this.player.health_Regeneration - currently_Equipped_Item.health_Regeneration_Increase;
				this.player.mana_Regeneration = this.player.mana_Regeneration - currently_Equipped_Item.mana_Regeneration_Increase;
				inventory.add_Item(currently_Equipped_Item);
			}
			this.player.maximum_Stamina = this.player.maximum_Stamina + item_To_Equip.stamina_Increase;
			this.player.stamina_Regeneration = this.player.stamina_Regeneration + item_To_Equip.stamina_Regneration_Increase;
			this.player.health_Regeneration = this.player.health_Regeneration + item_To_Equip.health_Regeneration_Increase;
			this.player.mana_Regeneration = this.player.mana_Regeneration + item_To_Equip.mana_Regeneration_Increase;
		}
	}

/*	public void toggle_Equipment()
	{
		if(this.equipment_Canvas.activeSelf == false)
		{
			this.equipment_Canvas.transform.SetAsFirstSibling();
		}
		this.equipment_Canvas.gameObject.SetActive(!this.equipment_Canvas.gameObject.activeSelf);
	}*/
}
