using UnityEngine;

public class Player : MonoBehaviour
{
	public static GameObject player_Game_Object;

	public float damage;
	public float maximum_Health;
	public float current_Health;
	public float health_Regeneration;
	public float maximum_Stamina;
	public float current_Stamina;
	public float stamina_Regeneration;
	public float maximum_Mana;
	public float current_Mana;
	public float mana_Regeneration;
	public RectTransform current_Health_Bar;
	public RectTransform current_Stamina_Bar;
	public RectTransform current_Mana_Bar;

	public bool blocked = false;

	[HideInInspector]
	public Inventory inventory;
	[HideInInspector]
	public Equipment equipment;
	private Options_Menu menu;

	public enum Actions
	{
		None,
		Activate,
	}

	public Player.Actions current_Action;

	void Start()
	{
		Player.player_Game_Object = this.gameObject;
		this.inventory = this.GetComponent<Inventory>();
		this.equipment = this.GetComponent<Equipment>();
		this.menu = this.GetComponent<Options_Menu>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		if(Input.GetKeyDown("i") == true && this.menu.interface_Canvas.activeSelf == false)
		{
			this.inventory.toggle_Interface();
		}
		if(Input.GetKeyDown("p") == true && this.menu.interface_Canvas.activeSelf == false)
		{
			this.equipment.toggle_Interface();
		}
		if(Input.GetKeyDown("escape") == true)
		{
			if((this.equipment.interface_Canvas.activeSelf == false && this.inventory.interface_Canvas.activeSelf == false) || this.menu.interface_Canvas.activeSelf == true)
			{
				this.menu.toggle_Interface();
			}
			if(this.equipment.interface_Canvas.activeSelf == true)
			{
				this.equipment.toggle_Interface();
			}
			if(this.inventory.interface_Canvas.activeSelf == true)
			{
				this.inventory.toggle_Interface();
			}
		}
	}

	void FixedUpdate()
	{
		if(Input.GetKey("e") == true)
		{
			this.current_Action = Player.Actions.Activate;
		}
		else
		{
			this.current_Action = Player.Actions.None;
		}

		//Regeneration Statistics
		this.current_Health = Mathf.Min(this.maximum_Health, this.current_Health + this.health_Regeneration * Time.fixedDeltaTime);
		this.current_Health_Bar.localScale = new Vector3(this.current_Health / this.maximum_Health, 1.0f, 1.0f);
		this.current_Stamina = Mathf.Min(this.maximum_Stamina, this.current_Stamina + this.stamina_Regeneration * Time.fixedDeltaTime);
		this.current_Stamina_Bar.localScale = new Vector3(this.current_Stamina / this.maximum_Stamina, 1.0f, 1.0f);
		this.current_Mana = Mathf.Min(this.maximum_Mana, this.current_Mana + this.mana_Regeneration * Time.fixedDeltaTime);
		this.current_Mana_Bar.localScale = new Vector3(this.current_Mana / this.maximum_Mana, 1.0f, 1.0f);	
	}
}
