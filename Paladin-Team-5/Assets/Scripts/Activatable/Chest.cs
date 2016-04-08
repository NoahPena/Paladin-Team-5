using UnityEngine;

public class Chest : Activatable
{
	public Item contained_Treasure;

	public override void activate(GameObject player)
	{
		if(this.activated == false && player.GetComponent<Player>().inventory.is_Full() == false)
		{
			this.activated = true;
			player.GetComponent<Player>().inventory.add_Item(this.contained_Treasure);
//			Debug.Log(this.contained_Treasure.item_Type + " added to inventory.");
			contained_Treasure = null;
		}
		else if(this.activated == false)
		{
			Debug.Log("Inventory is full. Clear some space and try again.");
		}
	}
}
