using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathScreen : Interface_Window {

	// Use this for initialization
	void Start () 
	{
		this.initialize_Interface_Window ();	
	}
	
	// Update is called once per frame
	override public void toggle_Interface()
	{
		base.toggle_Interface ();
		//Time.timeScale = 0.0f;
	}

	public void restart_game()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void quit_game()
	{
		Application.Quit ();
	}
}
