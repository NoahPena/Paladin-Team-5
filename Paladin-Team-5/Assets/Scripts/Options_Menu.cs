using UnityEngine;

public class Options_Menu : Interface_Window
{
	void Start()
	{
		this.initialize_Interface_Window();
	}

	override public void toggle_Interface()
	{
		base.toggle_Interface();
		if(this.interface_Canvas.activeSelf == false)
		{
			Time.timeScale = 1.0f;
		}
		else
		{
			Time.timeScale = 0.0f;
		}
	}
}
