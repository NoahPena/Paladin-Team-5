using UnityEngine;

public class User_Interface_Actions : MonoBehaviour
{
	public GameObject start_Menu;
	public UnityEngine.UI.Button start_Game_Button;
	public UnityEngine.UI.Button options_Menu_Button;
	public UnityEngine.UI.Button exit_Game_Button;

	public GameObject exit_Confirmation_Menu;
	public UnityEngine.UI.Button confirm_Exit_Button;
	public UnityEngine.UI.Button cancel_Exit_Button;

	public void start_Game_Button_Pressed()
	{
		this.start_Menu.SetActive(false);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Testing Scene");
	}

	public void options_Menu_Button_Pressed()
	{
		Debug.Log("There is no functionality set up for Options yet.");
	}

	public void exit_Game_Button_Pressed()
	{
		this.exit_Confirmation_Menu.SetActive(true);
		this.start_Game_Button.enabled = false;
		this.options_Menu_Button.enabled = false;
		this.exit_Game_Button.enabled = false;
	}

	public void confirm_Exit_Button_Pressed()
	{
		UnityEngine.Application.Quit();
	}

	public void cancel_Exit_Button_Pressed()
	{
		this.exit_Confirmation_Menu.SetActive(false);
		this.start_Game_Button.enabled = true;
		this.options_Menu_Button.enabled = true;
		this.exit_Game_Button.enabled = true;
	}
}
