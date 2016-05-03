using UnityEngine;

public class Main_Menu_Actions : MonoBehaviour
{
	public GameObject start_Menu;
	public UnityEngine.UI.Button start_Game_Button;
	public UnityEngine.UI.Button options_Menu_Button;
	public UnityEngine.UI.Button exit_Game_Button;

	public GameObject exit_Confirmation_Menu;
	public UnityEngine.UI.Button confirm_Exit_Button;
	public UnityEngine.UI.Button cancel_Exit_Button;

	public GameObject options_Menu;
	public UnityEngine.UI.Text difficulty_Text;

	public void start_Game_Button_Pressed()
	{
		this.start_Menu.SetActive(false);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Boss Zone");
	}

	public void options_Menu_Button_Pressed()
	{
		this.options_Menu.SetActive(true);
		this.start_Game_Button.enabled = false;
		this.options_Menu_Button.enabled = false;
		this.exit_Game_Button.enabled = false;
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

	public void close_Options_Menu()
	{
		this.options_Menu.SetActive(false);
		this.start_Game_Button.enabled = true;
		this.options_Menu_Button.enabled = true;
		this.exit_Game_Button.enabled = true;
	}

	public void difficulty_Button_Pressed()
	{
		if(Attack.enemy_Damage_Multiplier == 1.0f)
		{
			Attack.enemy_Damage_Multiplier = 0.5f;
			this.difficulty_Text.text = "Difficulty :   Medium";
		}
		else if(Attack.enemy_Damage_Multiplier == 0.5f)
		{
			Attack.enemy_Damage_Multiplier = 0.0f;
			this.difficulty_Text.text = "Difficulty :   None";
		}
		else
		{
			Attack.enemy_Damage_Multiplier = 1.0f;
			this.difficulty_Text.text = "Difficulty :   Hard";
		}
	}
}
