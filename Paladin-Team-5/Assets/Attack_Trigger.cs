using UnityEngine;

public class Attack_Trigger : MonoBehaviour
{
	private Enemy enemy_Script;

	void Start()
	{
		this.enemy_Script = this.GetComponentInParent<Enemy>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.enemy_Script.is_In_Attack_Range = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.enemy_Script.is_In_Attack_Range = false;
		}
	}
}
