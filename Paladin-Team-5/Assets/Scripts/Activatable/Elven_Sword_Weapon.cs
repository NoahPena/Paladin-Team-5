using UnityEngine;
using System.Collections;

public class Elven_Sword_Weapon : Activatable
{

	public override void activate(GameObject player)
	{
		if (this.activated == false && player.GetComponent<Player> ().inventory.is_Full () == false) 
		{
			this.activated = true;
			//player.GetComponent<Player>().inventory.add_Item(this.
		}
	}
		

}
