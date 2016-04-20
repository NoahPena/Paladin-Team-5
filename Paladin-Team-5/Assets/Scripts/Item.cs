using UnityEngine;

public class Item : MonoBehaviour
{
	public enum Types
	{
		Cloak = 0,
		Boots = 1,
		Leggings = 2,
		Gloves = 3,
		Hat = 4,
		Bracelets = 5,
		Amulet = 6,
		Rings = 7,
		Shirt = 8,
		Earrings = 9,
		Weapon = 10,
		Shield = 11
	}

	public Item.Types item_Type;
	public string item_Name;		//The title of the item, displayed on buttons
	public string item_Description;	//A more in-depth description of the item and additional information, will later be dispalyed along with statistics changes on-hover or item selected
	public float cost;
	public float stamina_Increase;
	public float stamina_Regneration_Increase;
	public float health_Regeneration_Increase;
	public float mana_Regeneration_Increase;
}