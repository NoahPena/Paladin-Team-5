using UnityEngine;

public class Lever : Activatable
{
	public enum Actions
	{
//		Move,
		Delete
	}

	public GameObject affected_Object;
	public Lever.Actions activation_Action;
	public float time_To_Complete_Action;

	public override void activate(GameObject player)
	{
		if(this.activated == false)
		{
			this.activated = true;
			if(this.activation_Action == Lever.Actions.Delete)
			{
				GameObject.Destroy(this.affected_Object, this.time_To_Complete_Action);
			}
		}
	}
}
