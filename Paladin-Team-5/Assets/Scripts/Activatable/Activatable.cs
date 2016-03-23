using UnityEngine;

abstract public class Activatable : MonoBehaviour
{
	public bool activated = false;

	void OnTriggerStay(Collider collider)
	{
		if(collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().current_Action == Player.Actions.Activate)
		{
			this.activate(collider.gameObject);
		}
	}

	public virtual void activate(GameObject player)
	{
		this.activated = !this.activated;
	}
}
