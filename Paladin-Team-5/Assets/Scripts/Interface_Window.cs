using UnityEngine;

public abstract class Interface_Window : MonoBehaviour
{
	static GameObject window_To_Drag;
	private float previous_Update_Mouse_X_Position = 0.0f;
	private float previous_Update_Mouse_Y_Position = 0.0f;
//	static System.Collections.Generic.List<GameObject> windows;
//	static System.Collections.Generic.List<GameObject> windows_At_Click_Location;

	protected RectTransform window_Transform;
	public float window_Half_Width;	//I can't seem to access the RectTransform's stored width and height parameters, so these are used instead
	public float window_Half_Height;

	public GameObject interface_Canvas;

	static Interface_Window()
	{
//		Interface_Window.window_To_Drag = null;
//		Interface_Window.windows = new System.Collections.Generic.List<GameObject>();
//		Interface_Window.windows_At_Click_Location = new System.Collections.Generic.List<GameObject>();
	}

	public void initialize_Interface_Window()
	{
//		Interface_Window.windows.Add(this.interface_Canvas);
		Interface_Window.window_To_Drag = this.transform.parent.gameObject;
		this.interface_Canvas.gameObject.SetActive(false);
		this.window_Transform = this.interface_Canvas.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
	}

	public void Update()
	{
		if(Interface_Window.window_To_Drag != this.transform.parent.gameObject && Interface_Window.window_To_Drag == this.interface_Canvas)// && this.interface_Canvas.transform.parent.GetChild(0))
		{
			this.window_Transform.anchoredPosition = new Vector2(this.window_Transform.anchoredPosition.x + (Input.mousePosition.x - this.previous_Update_Mouse_X_Position), this.window_Transform.anchoredPosition.y + (Input.mousePosition.y - this.previous_Update_Mouse_Y_Position));
		}
		if(Input.GetMouseButtonDown(0))
		{
			//If the click is within the current window's rectangle...
			if(this.interface_Canvas.activeSelf == true && Input.mousePosition.x >= this.window_Transform.position.x - this.window_Half_Width && Input.mousePosition.x <= this.window_Transform.position.x + this.window_Half_Width && Input.mousePosition.y >= this.window_Transform.position.y - this.window_Half_Height && Input.mousePosition.y <= this.window_Transform.position.y + this.window_Half_Height)
			{
				this.interface_Canvas.transform.SetAsFirstSibling();
				this.interface_Canvas.SetActive(false);					//Fix for weird Canvas rendering issues
				this.interface_Canvas.SetActive(true);
				Interface_Window.window_To_Drag = this.interface_Canvas;
			}
//			else if()//Add dragging window when menu bar clicked and held instead of the entire box
//			{

//			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			Interface_Window.window_To_Drag = this.transform.parent.gameObject;
		}
		this.previous_Update_Mouse_X_Position = Input.mousePosition.x;
		this.previous_Update_Mouse_Y_Position = Input.mousePosition.y;
	}

	public virtual void toggle_Interface()
	{
		if(this.interface_Canvas.activeSelf == false)
		{
			Interface_Window.window_To_Drag = this.transform.parent.gameObject;
			this.interface_Canvas.transform.SetAsFirstSibling();
		}
		if (!this.interface_Canvas.gameObject.activeSelf) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		this.interface_Canvas.gameObject.SetActive(!this.interface_Canvas.gameObject.activeSelf);
	}
}
