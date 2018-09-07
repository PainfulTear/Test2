using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public Player player;
    public SkinnedMeshRenderer playerSkin;
    public List<Item> itemsInInventory;
    public List<Ability> availableAbilities;
    public int FreeSlots = 10;
    public InventoryMenu inventoryMenu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            //inventoryMenu.DisplayItems();      
	}

    void EquipItem (GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(playerSkin.transform.parent);
        SkinnedMeshRenderer[] renderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (renderers.Length > 0)
        {
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.bones = playerSkin.bones;
                renderer.rootBone = playerSkin.rootBone;
            }
        }
    }

    public void AddItem(Item item)
    {
        itemsInInventory.Add(item);
        FreeSlots -= item.spaceInInventory;
    }

    public void RemoveItem(Item item)
    {
        itemsInInventory.Remove(item);
        FreeSlots += item.spaceInInventory;
        GameObject obj = Instantiate(item.itemPrefab);
        obj.transform.position = player.gameObject.transform.position + Vector3.forward;
    }
}
