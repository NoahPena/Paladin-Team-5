using UnityEngine;

public class Attack : MonoBehaviour
{
	public enum attack_Type
	{
		regular,
		magic,
		ranged,
		fire,
		ice
	}

	public GameObject owner;
	public Attack.attack_Type type;

	private float damage;
	private bool has_Hit = false;

	void OnEnable()
	{
		this.has_Hit = false;
		if(this.owner.tag == "Player")
		{
			this.damage = this.owner.GetComponentInChildren<Player>().damage;
		}
		else if(this.owner.tag == "Enemy")
		{
			this.damage = this.owner.GetComponentInChildren<Enemy>().damage;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(this.has_Hit == false)
		{
			switch(this.owner.tag)
			{
				case "Player":
					if(other.tag == "Enemy")
					{
						Debug.Log("The Player has hit the Enemy.");
					}
					break;

				case "Enemy":
					if(other.tag == "Player")
					{
						this.has_Hit = true;
						other.GetComponent<Player>().current_Health = other.GetComponent<Player>().current_Health - this.damage;
						Debug.Log("The Enemy has hit the Player for " + this.damage + " damage.");
					}
					break;

				default:
					if(other.tag == "Enemy" || other.tag == "Player")
					{
						Debug.Log("The Environment has hit the " + other.tag + ".");
					}
					break;
			}
		}
	}
}
