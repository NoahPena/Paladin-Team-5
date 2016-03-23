using UnityEngine;

public class Equipment : MonoBehaviour
{
	private Player player;
	private Inventory inventory;

	public Item cloak;
	public Item boots;
	public Item leggings;
	public Item gloves;
	public Item hat;
	public Item bracelets;
	public Item amulet;
	public Item rings;
	public Item shirt;
	public Item earrings;

	public void Start()
	{
		this.player = this.gameObject.GetComponent<Player>();
		this.inventory = this.gameObject.GetComponent<Inventory>();
	}

	public void unequip_Item(Item item_To_Unequip)
	{
		if(item_To_Unequip != null && this.inventory != null && this.inventory.is_Full() == false)
		{
			this.player.maximum_Stamina = this.player.maximum_Stamina - item_To_Unequip.stamina_Increase;
			this.player.stamina_Regeneration = this.player.stamina_Regeneration - item_To_Unequip.stamina_Regneration_Increase;
			this.player.health_Regeneration = this.player.health_Regeneration - item_To_Unequip.health_Regeneration_Increase;
			this.player.mana_Regeneration = this.player.mana_Regeneration - item_To_Unequip.mana_Regeneration_Increase;
			switch(item_To_Unequip.item_Type)
			{
				case Item.Types.Cloak:
					this.inventory.add_Item(item_To_Unequip);
					this.cloak = null;
					break;

				case Item.Types.Boots:
					this.inventory.add_Item(item_To_Unequip);
					this.boots = null;
					break;

				case Item.Types.Leggings:
					this.inventory.add_Item(item_To_Unequip);
					this.leggings = null;
					break;

				case Item.Types.Gloves:
					this.inventory.add_Item(item_To_Unequip);
					this.gloves = null;
					break;

				case Item.Types.Hat:
					this.inventory.add_Item(item_To_Unequip);
					this.hat = null;
					break;

				case Item.Types.Bracelets:
					this.inventory.add_Item(item_To_Unequip);
					this.bracelets = null;
					break;

				case Item.Types.Amulet:
					this.inventory.add_Item(item_To_Unequip);
					this.amulet = null;
					break;

				case Item.Types.Rings:
					this.inventory.add_Item(item_To_Unequip);
					this.rings = null;
					break;

				case Item.Types.Shirt:
					this.inventory.add_Item(item_To_Unequip);
					this.shirt = null;
					break;

				case Item.Types.Earrings:
					this.inventory.add_Item(item_To_Unequip);
					this.earrings = null;
					break;
			}
		}
	}

	public void equip_Item(Item item_To_Equip)
	{
		if(item_To_Equip != null && this.inventory != null)
		{
			Item currently_Equipped_Item = null;
			switch(item_To_Equip.item_Type)
			{
				case Item.Types.Cloak:
					currently_Equipped_Item = this.cloak;
					this.cloak = item_To_Equip;
					break;

				case Item.Types.Boots:
					currently_Equipped_Item = this.boots;
					this.boots = item_To_Equip;
					break;

				case Item.Types.Leggings:
					currently_Equipped_Item = this.leggings;
					this.leggings = item_To_Equip;
					break;

				case Item.Types.Gloves:
					currently_Equipped_Item = this.gloves;
					this.gloves = item_To_Equip;
					break;

				case Item.Types.Hat:
					currently_Equipped_Item = this.hat;
					this.hat = item_To_Equip;
					break;

				case Item.Types.Bracelets:
					currently_Equipped_Item = this.bracelets;
					this.bracelets = item_To_Equip;
					break;

				case Item.Types.Amulet:
					currently_Equipped_Item = this.amulet;
					this.amulet = item_To_Equip;
					break;

				case Item.Types.Rings:
					currently_Equipped_Item = this.rings;
					this.rings = item_To_Equip;
					break;

				case Item.Types.Shirt:
					currently_Equipped_Item = this.shirt;
					this.shirt = item_To_Equip;
					break;

				case Item.Types.Earrings:
					currently_Equipped_Item = this.earrings;
					this.earrings = item_To_Equip;
					break;
			}
			inventory.delete_Item(item_To_Equip);
			if(currently_Equipped_Item != null)
			{
				this.player.maximum_Stamina = this.player.maximum_Stamina - currently_Equipped_Item.stamina_Increase;
				this.player.stamina_Regeneration = this.player.stamina_Regeneration - currently_Equipped_Item.stamina_Regneration_Increase;
				this.player.health_Regeneration = this.player.health_Regeneration - currently_Equipped_Item.health_Regeneration_Increase;
				this.player.mana_Regeneration = this.player.mana_Regeneration - currently_Equipped_Item.mana_Regeneration_Increase;
				inventory.add_Item(currently_Equipped_Item);
			}
			this.player.maximum_Stamina = this.player.maximum_Stamina + item_To_Equip.stamina_Increase;
			this.player.stamina_Regeneration = this.player.stamina_Regeneration + item_To_Equip.stamina_Regneration_Increase;
			this.player.health_Regeneration = this.player.health_Regeneration + item_To_Equip.health_Regeneration_Increase;
			this.player.mana_Regeneration = this.player.mana_Regeneration + item_To_Equip.mana_Regeneration_Increase;
		}
	}
}
