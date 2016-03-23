using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
	private Transform camera_Horizontal_Rotation_Transform;

	public float camera_Sensitivity = 5.0f;

	void Start()
	{
		this.camera_Horizontal_Rotation_Transform = this.gameObject.transform.parent;
	}

	void FixedUpdate()
	{
		if((Input.GetAxis("Zoom In") > 0.0f || Input.GetButton("Zoom In")) && this.gameObject.transform.localPosition.z <= -2.05f)
		{
			this.gameObject.transform.Translate(0.0f, 0.0f, 0.1f);
		}
		else if((Input.GetAxis("Zoom Out") < 0.0f || Input.GetButton("Zoom Out")) && this.gameObject.transform.localPosition.z >= -7.45f)
		{
			this.gameObject.transform.Translate(0.0f, 0.0f, -0.1f);
		}
		float camera_X_Angle = this.camera_Horizontal_Rotation_Transform.localEulerAngles.x;
		if(this.camera_Horizontal_Rotation_Transform.localEulerAngles.x > 90.0f)
		{
			camera_X_Angle = camera_X_Angle - 360.0f;
		}
		this.camera_Horizontal_Rotation_Transform.localEulerAngles = new Vector3(Mathf.Ceil(Mathf.Clamp(camera_X_Angle + Input.GetAxis("Rotate Vertical") * this.camera_Sensitivity, -89.9999f, 89.9999f) * 100000000 + 36000000000.0f) / 100000000, this.camera_Horizontal_Rotation_Transform.localEulerAngles.y + Input.GetAxis("Rotate Horizontal") * this.camera_Sensitivity, 0.0f);
	}
}
