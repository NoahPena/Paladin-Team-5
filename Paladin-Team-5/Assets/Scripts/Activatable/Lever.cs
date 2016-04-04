using UnityEngine;

public class Lever : Activatable
{
	public enum Actions
	{
		Move,
		Delete
	}

	public GameObject target_Object;
	public Lever.Actions activation_Action;
	public float time_To_Complete_Action;
	public Vector3 target_Inactive_Location;
	public Vector3 target_Active_Location;

	private Vector3 transition_Move = Vector3.zero;

	private float transition_End_Time;
	private bool transitioning_States;

	public override void activate(GameObject player)
	{
		if(this.transitioning_States == false)
		{
			if(this.activated == false)
			{
				this.transitioning_States = true;
				this.activated = true;
				switch(this.activation_Action)
				{
					case Lever.Actions.Move:
						this.transition_Move = (this.target_Active_Location - this.target_Inactive_Location) / this.time_To_Complete_Action * Time.fixedDeltaTime;
						break;

					case Lever.Actions.Delete:
						GameObject.Destroy(this.target_Object, this.time_To_Complete_Action);
						break;

					default:
						break;
				}
				this.transition_End_Time = Time.fixedTime + this.time_To_Complete_Action;
			}
			else
			{
				this.transitioning_States = true;
				this.activated = false;
				switch(this.activation_Action)
				{
					case Lever.Actions.Move:
						this.transition_Move = (this.target_Inactive_Location - this.target_Active_Location) / this.time_To_Complete_Action * Time.fixedDeltaTime;
						break;

					default:
						break;
				}
				this.transition_End_Time = Time.fixedTime + this.time_To_Complete_Action;
			}
		}
	}

	void FixedUpdate()
	{
		if(this.transitioning_States == true)
		{
			if(Time.fixedTime < this.transition_End_Time && this.activation_Action == Lever.Actions.Move)
			{
				this.target_Object.transform.Translate(this.transition_Move);
			}
			else
			{
				this.transitioning_States = false;
				if(this.activation_Action == Lever.Actions.Move)
				{
					if(this.activated == true)
					{
						this.target_Object.transform.position = this.target_Active_Location;
					}
					else
					{
						this.target_Object.transform.position = this.target_Inactive_Location;
					}
				}
			}
		}
	}
}
