using UnityEngine;

public class Player : MonoBehaviour
{
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
		this.inventory = this.GetComponent<Inventory>();
		this.equipment = this.GetComponent<Equipment>();
		this.menu = this.GetComponent<Options_Menu>();
	}

	void Update()
	{
		if(Input.GetKeyDown("i") == true)
		{
			this.inventory.toggle_Interface();
		}
		if(Input.GetKeyDown("p") == true)
		{
			this.equipment.toggle_Interface();
		}
		if(Input.GetKeyDown("escape") == true)
		{
			if((this.equipment.interface_Canvas.activeSelf == false && this.inventory.interface_Canvas.activeSelf == false) || this.menu.interface_Canvas.activeSelf == true)
			{
				this.menu.toggle_Interface();
			}
			else if(this.equipment.interface_Canvas.activeSelf == true)
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
		this.current_Stamina = Mathf.Min(this.maximum_Stamina, this.current_Stamina + this.stamina_Regeneration * Time.fixedDeltaTime);
		this.current_Mana = Mathf.Min(this.maximum_Mana, this.current_Mana + this.mana_Regeneration * Time.fixedDeltaTime);
	}
}
