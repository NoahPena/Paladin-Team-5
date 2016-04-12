using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour 
{

	public Transform impactPrefab;
	public LayerMask mask;
	public float damage = 25;
	public bool canAttack = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (canAttack) 
		{
			foreach (ContactPoint contact in collision.contacts) 
			{
				Object impact = Instantiate (impactPrefab, contact.point, Quaternion.FromToRotation (Vector3.up, contact.normal));
				collision.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
