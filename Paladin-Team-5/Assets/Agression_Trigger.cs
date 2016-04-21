using UnityEngine;

public class Agression_Trigger : MonoBehaviour
{
	public float amount_Of_Time_To_Cease_Aggression = 10.0f;
	public System.Collections.Generic.List<Vector3> patrol_Locations;
	public float aggression_Speed;
	public float attack_Range;

	private Vector3 current_Patrol_Location;
	private int current_Patrol_Location_Index = 0;
	private float time_To_End_Aggression = float.MinValue;
	private Animator enemy_Animator;

	private NavMeshAgent navigator;
	private float patrol_Speed;
	private float patrol_Stopping_Distance;

	private Enemy enemy_Script;

	void Start()
	{
		this.enemy_Animator = this.GetComponentInParent<Animator>();

		this.navigator = this.GetComponentInParent<NavMeshAgent>();
		this.patrol_Speed = this.navigator.speed;

		this.enemy_Script = this.GetComponentInParent<Enemy>();

		if(this.patrol_Locations.Count > 0)
		{
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
		else
		{
			this.patrol_Locations.Add(this.transform.position);
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
		this.navigator.SetDestination(this.current_Patrol_Location);
	}

	void FixedUpdate()
	{
		if(this.time_To_End_Aggression >= Time.fixedTime)
		{
//			if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
			if(this.enemy_Script.state != Enemy.enemy_State.Attacking)
			{
				if(this.navigator.remainingDistance <= this.attack_Range + 0.01f)
				{
					if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Repeater") == false)
					{
						this.enemy_Animator.Play("Idle");
					}
					this.transform.parent.eulerAngles = new Vector3(0.0f, Mathf.Atan2((Player.player_Game_Object.transform.position - this.transform.parent.position).x, (Player.player_Game_Object.transform.position - this.transform.parent.position).z) * Mathf.Rad2Deg, 0.0f);
				}
				else if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Running") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Running Repeater") == false)
				{
					this.enemy_Animator.Play("Running");
				}
				if(this.navigator.destination != Player.player_Game_Object.transform.position)
				{
					this.navigator.SetDestination(Player.player_Game_Object.transform.position);
				}
				
			}
		}
		else
		{
			if(this.navigator.obstacleAvoidanceType != ObstacleAvoidanceType.NoObstacleAvoidance)
			{
				this.navigator.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
				this.navigator.speed = this.patrol_Speed;
				this.navigator.stoppingDistance = this.patrol_Stopping_Distance;
			}
			if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Repeater") == false)
			{
				this.enemy_Animator.Play("Walking");
			}
			if(this.navigator.remainingDistance < this.patrol_Stopping_Distance + 0.01f)
			{
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
			if(this.navigator.destination != this.current_Patrol_Location)
			{
				this.navigator.SetDestination(this.current_Patrol_Location);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.navigator.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
			this.navigator.speed = this.aggression_Speed;
			this.navigator.stoppingDistance = this.attack_Range;
			this.time_To_End_Aggression = float.MaxValue;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.time_To_End_Aggression = Time.fixedTime + this.amount_Of_Time_To_Cease_Aggression;
		}
	}
}
