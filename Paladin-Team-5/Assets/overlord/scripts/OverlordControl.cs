using UnityEngine;
using System.Collections;

public class OverlordControl : MonoBehaviour 
{

	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public float jumpInterval = 1.5f;
	private float nextJump = 1.2f;

	public float speed = 4;
	public float runSpeed = 8;
	private float moveAmount;
	public float smoothSpeed = 2;
	private float sensitivityX = 6;

	public float gravity = 25;
	public float rotateSpeed = 8.0f;
	public float dampTime = 3;

	private float horizontalSpeed;

	public bool grounded;
	public AudioSource myAudioSource;

	private float nextStep;
	public Transform target;
	public Transform chest;

	private bool running = false;

	private Vector3 forward = Vector3.forward;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 right;
	private bool canRun = true;
	private bool canjump = false;
	private bool isJumping = false;

	public Transform shield;
	public Transform weapon;
	public Transform leftHandPos;
	public Transform rightHandPos;
	public Transform chestPosShield;
	public Transform chestPosWeapon;
	public AudioClip equipOneSound;
	public AudioClip[] wooshSounds;

	public AudioClip holsterOneSound;

	private bool fightModus = false;
	private bool didSelect = false;
	private bool canAttack = false;

	public Transform myCamera;
	private Transform reference;

	// Use this for initialization
	void Start () 
	{
		reference = new GameObject ().transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		reference.eulerAngles = new Vector3 (0, myCamera.eulerAngles.y, 0);
		forward = reference.forward;
		right = new Vector3 (forward.z, 0, -forward.x);

		CharacterController controller = GetComponent<CharacterController> ();
		Animator animator = GetComponent<Animator> ();

		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		Vector3 targetDirection = (hor * right) + (ver * forward);

		Vector3 velocity = controller.velocity;
		float z = velocity.z;
		float x = velocity.x;
		Vector3 horizontalVelocity = new Vector3 (x, 0, z);
		float horizontalSpeed = horizontalVelocity.magnitude;
		Vector3 localMagnitude = transform.InverseTransformDirection (horizontalVelocity);

		if (fightModus) 
		{
			Vector3 localTarget = transform.InverseTransformPoint (target.position);
			float addFloat = (Mathf.Atan2 (localTarget.x, localTarget.z));

			canRun = false;

			Vector3 relativePos = target.transform.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation (relativePos, Vector3.up);
			lookRotation.x = 0;
			lookRotation.z = 0;

			animator.SetFloat ("hor", (localMagnitude.x) + (addFloat * 2), dampTime, 0.8f);
			animator.SetFloat ("ver", (localMagnitude.z), dampTime, 0.8f);
			transform.rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
		}
		else 
		{
			canRun = true;

			if (targetDirection != Vector3.zero) 
			{
				Quaternion lookRotationTwo = Quaternion.LookRotation (targetDirection, Vector3.up);
				lookRotationTwo.x = 0;
				lookRotationTwo.z = 0;
				transform.rotation = Quaternion.Lerp (transform.rotation, lookRotationTwo, Time.deltaTime * rotateSpeed);
			}
		}

		Vector3 targetVelocity = targetDirection;

		if (Input.GetButton ("Fire2") && canRun && !isJumping) 
		{
			targetVelocity *= runSpeed;
		}
		else
		{
			targetVelocity *= speed;
		}

		Sword swordscript = weapon.GetComponentInChildren<Sword> ();

		if (canAttack) 
		{
			bool attackState = animator.GetCurrentAnimatorStateInfo (0).IsName ("attacks");
			swordscript.canAttack = attackState;

			if (Input.GetButtonDown ("Fire1")) {
				if (!attackState) {
					myAudioSource.clip = wooshSounds [Random.Range (0, wooshSounds.Length)];
					myAudioSource.pitch = 0.98f + 0.1f * Random.value;
					myAudioSource.Play ();

					

					gameObject.GetComponent<Player> ().damage = 10;
					weapon.GetComponentInChildren<MeshCollider> ().enabled = true;
					animator.SetBool ("attack", true);
				}
			} else if (Input.GetKey ("1")) {
				Debug.Log ("Block");
				Debug.Log (animator.GetCurrentAnimatorStateInfo (0).ToString ());
				animator.PlayInFixedTime ("Block");
				gameObject.GetComponent<Player> ().blocked = true;
				//animator.SetInteger ("Ability", 1);
			} else if (Input.GetKey ("3")) {
				Debug.Log ("Staggering Blow");
				animator.PlayInFixedTime ("SpinAttack");
				weapon.GetComponentInChildren<MeshCollider> ().enabled = true;
				gameObject.GetComponent<Player> ().damage = 15;
			}
			else
			{
				animator.SetBool ("attack", false);	
				gameObject.GetComponent<Player> ().blocked = false;
				weapon.GetComponentInChildren<MeshCollider> ().enabled = false;
				gameObject.GetComponent<Player> ().damage = 0;
			}
		}

		if (controller.isGrounded) {
			animator.SetFloat ("speed", horizontalSpeed, dampTime, 0.2f);

			if (Input.GetButton ("Jump") && Time.time > nextJump) {
				nextJump = Time.time + jumpInterval;
				moveDirection.y = jumpHeight;
				animator.SetBool ("Jump", true);
				isJumping = true;
			} 
			else if (Input.GetKey ("2") && canAttack && Time.time > nextJump) {
				Debug.Log ("Heavenly Strike");

				nextJump = Time.time + jumpInterval;
				moveDirection.y = jumpHeight * 1.2f;
				isJumping = true;

				animator.PlayInFixedTime ("HeavenlyStrike");

				gameObject.GetComponent<Player> ().damage = 25;
				weapon.GetComponentInChildren<MeshCollider> ().enabled = true;

			}
			else if(Input.GetKey("n"))
			{
				animator.PlayInFixedTime ("Catwalk");
			}
			else
			{
				animator.SetBool ("Jump", false);
				isJumping = false;
				gameObject.GetComponent<Player> ().damage = 0;
				weapon.GetComponentInChildren<MeshCollider> ().enabled = false;
			}
		}
		else
		{
			moveDirection.y -= gravity * Time.deltaTime;
			nextJump = Time.time + jumpInterval;
		}

		moveDirection.z = targetVelocity.z;
		moveDirection.x = targetVelocity.x;
		controller.Move (moveDirection * Time.deltaTime);

		if (Input.GetButtonDown ("Fire3")) 
		{
			Debug.Log ("fine");
			this.weaponSelect ();
			//this.weaponSelect ();
		}

		animator.SetBool ("grounded", controller.isGrounded);
	}

	void equip()
	{
		weapon.parent = rightHandPos;
		weapon.position = rightHandPos.position;
		weapon.rotation = rightHandPos.rotation;
		myAudioSource.clip = equipOneSound;
		myAudioSource.loop = false;
		myAudioSource.pitch = (float)(0.9 + 0.2 * Random.value);
		myAudioSource.Play ();
		shield.parent = leftHandPos;
		shield.position = leftHandPos.position;
		shield.rotation = leftHandPos.rotation;
		fightModus = true;
	}

	void holster()
	{
		shield.parent = chestPosShield;
		shield.position = chestPosShield.position;
		shield.rotation = chestPosShield.rotation;
		myAudioSource.clip = holsterOneSound;
		myAudioSource.loop = false;
		myAudioSource.pitch = (float)(0.9 + 0.2 * Random.value);
		myAudioSource.Play ();
		fightModus = false;
		weapon.parent = chestPosWeapon;
		weapon.position = chestPosWeapon.position;
		weapon.rotation = chestPosWeapon.rotation;
	}

	void OnAnimatorIK()
	{
		Animator animator = GetComponent<Animator> ();

		if (canAttack) 
		{
			animator.SetLookAtPosition (target.position);
			animator.SetLookAtWeight (0.9f, 0.2f, 1f, 1f, 1f);
		}
	}

	//IEnumerator
	void weaponSelect()
	{
		Debug.Log ("first");

		Animator animator = GetComponent<Animator> ();

		Debug.Log (animator);

		Debug.Log (didSelect);

		if (didSelect) 
		{
			animator.CrossFade ("Holster", 0.15f, 0, 0);
			canAttack = false;
			didSelect = false;
			//yield return new WaitForSeconds (1);
		}
		else
		{
			animator.CrossFade ("Equip", 0.15f, 0, 0);
			canAttack = true;
			didSelect = true;
			//yield return new WaitForSeconds (1);
		}
	}

}
