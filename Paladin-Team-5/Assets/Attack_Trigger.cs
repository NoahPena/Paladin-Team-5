using UnityEngine;

public class Attack_Trigger : MonoBehaviour
{
	public float attack_Delay = 2.0f;

	private float time_Of_Next_Attack_Available = 0.0f;
	private bool is_In_Attack_Range = false;
	private Animator enemy_Animator;

	void Start()
	{
		this.enemy_Animator = this.GetComponentInParent<Animator>();
	}

	void FixedUpdate()
	{
		if(this.time_Of_Next_Attack_Available <= Time.fixedTime && this.is_In_Attack_Range == true)
		{
//			Debug.Log("You are being attacked.");
			this.enemy_Animator.Play("Attacking");
			this.time_Of_Next_Attack_Available = Time.fixedTime + this.attack_Delay;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.is_In_Attack_Range = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.is_In_Attack_Range = false;
		}
	}
}
