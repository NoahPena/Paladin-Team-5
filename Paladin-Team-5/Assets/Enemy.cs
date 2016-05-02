using UnityEngine;

public class Enemy : MonoBehaviour
{
	public enum enemy_State
	{
		Idle,
		Walking,
		Running,
		Jumping,
		Attacking,
		Dying
	}

	public Enemy.enemy_State state = Enemy.enemy_State.Walking;
	public float maximum_Health;
	public float current_Health;
	public float damage;
	public float attack_Delay;					//The time in seconds the enemy will not be able to begin another attack for
	public float attack_Collision_Start_Delay;	//The time in seconds after the Attack animation begins that the attack_Box will be activated
	public float attack_Duration;				//The time in seconds after the Attack animation begins that the attack_Box will be deactivated and the enemy_State set to Idle

	[HideInInspector]
	public bool is_In_Attack_Range = false;	//Changed in Attack_Trigger when the Player enters the attached trigger

	public GameObject attack_Box;

	private float time_Of_Next_Attack_Available = 0.0f;
	private Animator enemy_Animator;

	void Start()
	{
		this.enemy_Animator = this.GetComponentInParent<Animator>();
	}

	void FixedUpdate()
	{
		if(this.time_Of_Next_Attack_Available <= Time.fixedTime && this.is_In_Attack_Range == true)
		{
			this.attack();
		}
	}

	void attack()
	{
		this.state = Enemy.enemy_State.Attacking;
		this.enemy_Animator.Play("Attacking");
		this.time_Of_Next_Attack_Available = Time.fixedTime + this.attack_Delay;
		this.CancelInvoke();
		this.Invoke("enable_Attack_Box", this.attack_Collision_Start_Delay);
		this.Invoke("end_Attack", this.attack_Duration);
	}

	void enable_Attack_Box()
	{
		this.attack_Box.SetActive(true);
	}

	void end_Attack()
	{
		this.state = Enemy.enemy_State.Idle;
		this.attack_Box.SetActive(false);
	}

	public void die()
	{
		this.state = Enemy.enemy_State.Dying;
		this.enemy_Animator.Play("Dying");
		this.GetComponent<NavMeshAgent>().baseOffset = 0.0f;
		this.time_Of_Next_Attack_Available = float.MaxValue;
		this.CancelInvoke();
		this.attack_Box.SetActive(false);
		Audio_Manager.clips.Invoke("play_Passive_Clip", 5.0f);
		Object.Destroy(this.gameObject, 5.0f);
	}
}
