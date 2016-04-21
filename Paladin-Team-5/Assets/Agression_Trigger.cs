using UnityEngine;

public class Agression_Trigger : MonoBehaviour
{
	public float amount_Of_Time_To_Cease_Aggression = 10.0f;
	public System.Collections.Generic.List<Vector3> patrol_Locations;
	public float movement_Speed;

	private Vector3 current_Patrol_Location;
	private int current_Patrol_Location_Index = 0;
	private float time_To_End_Aggression = float.MinValue;
	private Animator enemy_Animator;

	void Start()
	{
		this.enemy_Animator = this.GetComponentInParent<Animator>();
		if(this.patrol_Locations.Count > 0)
		{
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
		else
		{
			this.patrol_Locations.Add(this.transform.position);
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
	}

	void FixedUpdate()
	{
		if(this.time_To_End_Aggression >= Time.fixedTime)
		{
			if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
			{
				if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Running") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Running Repeater") == false)
				{
					this.enemy_Animator.Play("Running");
				}
				this.transform.parent.Translate((Player.player_Game_Object.transform.position - this.transform.parent.position).normalized * this.movement_Speed * 1.75f, Space.World);
				this.transform.parent.eulerAngles = new Vector3(0.0f, Mathf.Atan2((Player.player_Game_Object.transform.position - this.transform.parent.position).x, (Player.player_Game_Object.transform.position - this.transform.parent.position).z) * Mathf.Rad2Deg, 0.0f);
			}
		}
		else
		{
			if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Repeater") == false)
			{
				this.enemy_Animator.Play("Walking");
			}
			this.transform.parent.Translate((this.current_Patrol_Location - this.transform.parent.position).normalized * this.movement_Speed, Space.World);
			this.transform.parent.eulerAngles = new Vector3(0.0f, Mathf.Atan2((this.current_Patrol_Location - this.transform.parent.position).x, (this.current_Patrol_Location - this.transform.parent.position).z) * Mathf.Rad2Deg, 0.0f);
//			Debug.Log("Current Patrol Location: " + this.current_Patrol_Location);
//			Debug.Log("Current parent position: " + this.transform.parent.position);
			if((this.current_Patrol_Location - this.transform.parent.position).magnitude <= this.movement_Speed * 1.05f)
			{
				this.transform.parent.position = this.current_Patrol_Location;
				if(this.current_Patrol_Location_Index + 1 < this.patrol_Locations.Count)
				{
					this.current_Patrol_Location_Index = this.current_Patrol_Location_Index + 1;
					this.current_Patrol_Location = this.patrol_Locations[this.current_Patrol_Location_Index];
				}
				else
				{
					this.current_Patrol_Location_Index = 0;
					this.current_Patrol_Location = this.patrol_Locations[this.current_Patrol_Location_Index];
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
//			Debug.Log("Beginning agression");
			this.time_To_End_Aggression = float.MaxValue;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
//			Debug.Log("Ending agression at " + Time.fixedTime + this.amount_Of_Time_To_Cease_Aggression);
			this.time_To_End_Aggression = Time.fixedTime + this.amount_Of_Time_To_Cease_Aggression;
		}
	}
}
