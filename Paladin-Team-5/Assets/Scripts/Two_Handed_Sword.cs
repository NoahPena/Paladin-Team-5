using UnityEngine;
using System.Collections;

public class Two_Handed_Sword : Weapon 
{

	[System.NonSerialized]
	public Transform impactPrefab;

	[System.NonSerialized]
	public LayerMask mask;

	[System.NonSerialized]
	public bool canAttack = false;

	[System.NonSerialized]
	public WeaponType type = WeaponType.TWO_HANDED_SWORD;

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
