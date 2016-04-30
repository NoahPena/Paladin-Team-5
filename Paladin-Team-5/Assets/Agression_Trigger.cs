using UnityEngine;

public class Agression_Trigger : MonoBehaviour
{
	public float amount_Of_Time_To_Cease_Aggression = 10.0f;
	private Transform[] patrol_Location_Transforms;
	private Vector3[] patrol_Locations;
	public float aggression_Speed;
	public float attack_Range;
	public float patrol_Wait_Time;

	private Vector3 current_Patrol_Location;
	private int current_Patrol_Location_Index = 0;
	private float time_To_End_Aggression = float.MinValue;
	private float time_To_Begin_Patrol = float.MinValue;
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

		//Takes the world positions of each Patrol Transform that are child objects and store them in an internal array, then clears up the Transforms and the references to the Transforms
		this.patrol_Location_Transforms = this.gameObject.GetComponentsInChildren<Transform>();
		this.patrol_Locations = new Vector3[this.patrol_Location_Transforms.Length - 1];
		for(int i = 1; i < this.patrol_Location_Transforms.Length; i++)
		{
			this.patrol_Locations[i - 1] = this.patrol_Location_Transforms[i].position;
			Object.Destroy(this.patrol_Location_Transforms[i].gameObject);
		}
		this.patrol_Location_Transforms = null;

		if(this.patrol_Locations.Length > 0)
		{
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
		else
		{
			this.patrol_Locations = new Vector3[1];
			this.patrol_Locations[0] = this.transform.position;
			this.current_Patrol_Location = this.patrol_Locations[0];
		}
		this.navigator.SetDestination(this.current_Patrol_Location);
	}

	void FixedUpdate()
	{
		if(this.time_To_End_Aggression >= Time.fixedTime)
		{
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
			if(this.navigator.remainingDistance < this.patrol_Stopping_Distance + 0.01f)
			{
				if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Repeater") == false)
				{
					this.enemy_Animator.Play("Idle");
				}
				if(this.enemy_Script.state != Enemy.enemy_State.Idle)
				{
					this.enemy_Script.state = Enemy.enemy_State.Idle;
					this.Invoke("set_Next_Patrol_Location", this.patrol_Wait_Time);
				}
			}
			else if(this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") == false && this.enemy_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Repeater") == false)
			{
				this.enemy_Animator.Play("Walking");
			}
			if(this.navigator.destination != this.current_Patrol_Location && this.time_To_Begin_Patrol < Time.fixedTime)
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

	void set_Next_Patrol_Location()
	{
		this.Invoke("update_State", Time.fixedDeltaTime);
		if(this.current_Patrol_Location_Index + 1 < this.patrol_Locations.Length)
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

	void update_State()
	{
		this.enemy_Script.state = Enemy.enemy_State.Walking;
	}
}
