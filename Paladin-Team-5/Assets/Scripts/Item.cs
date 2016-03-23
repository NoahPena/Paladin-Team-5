using UnityEngine;

public class Item : MonoBehaviour
{
	public enum Types
	{
		Cloak,
		Boots,
		Leggings,
		Gloves,
		Hat,
		Bracelets,
		Amulet,
		Rings,
		Shirt,
		Earrings
	}

	public Item.Types item_Type;
	public float stamina_Increase;
	public float stamina_Regneration_Increase;
	public float health_Regeneration_Increase;
	public float mana_Regeneration_Increase;
}