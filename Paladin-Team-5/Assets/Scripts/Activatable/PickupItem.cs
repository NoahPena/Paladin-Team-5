using UnityEngine;
using System.Collections;

public class PickupItem : Activatable
{
	public Weapon weaponOnGround;

	public override void activate (GameObject player)
	{
		if (this.activated == false && player.GetComponent<Player> ().inventory.is_Full () == false) 
		{
			this.activated = true;
			player.GetComponent<Player> ().inventory.add_Item (this.weaponOnGround);

			Debug.Log (this.weaponOnGround + " added to inventory");

			weaponOnGround = null;

			Destroy (gameObject);
		}
		else if (this.activated == false) 
		{
			Debug.Log ("Inventory is Full");
		}
			
	}
}
