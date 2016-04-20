using UnityEngine;
using System.Collections;

public class Weapon : Item
{

	public enum WeaponType
	{
		ONE_HANDED_SWORD,
		TWO_HANDED_SWORD,
		BOW_AND_ARROW,
		SHIELD
	}
		

	public int ID;

	public WeaponType weaponType;

	//[System.NonSerialized]
	public float Damage;

	//[System.NonSerialized]
	public float Block;

	//[System.NonSerialized]
	//public float Cost;

}
