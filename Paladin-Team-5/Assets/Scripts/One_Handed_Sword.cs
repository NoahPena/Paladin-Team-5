using UnityEngine;
using System.Collections;

public class One_Handed_Sword : Weapon 
{

	public Transform impactPrefab;
	public LayerMask mask;
	public bool canAttack = false;

	WeaponType type = WeaponType.ONE_HANDED_SWORD;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public WeaponType getWeaponType()
	{
		return type;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (canAttack) 
		{
			foreach (ContactPoint contact in collision.contacts) 
			{
				Object impact = Instantiate (impactPrefab, contact.point, Quaternion.FromToRotation (Vector3.up, contact.normal));
				collision.gameObject.SendMessage ("Damage", Damage, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
