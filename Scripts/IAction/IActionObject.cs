using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IActionObject : MonoBehaviour, IAction {

    bool objectSelected = false;
    Character NPCTarget;
    Item ItemTarget;
    Renderer NPCRenderer;
    Inventory inventory;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<Inventory>();
        if (this.gameObject.GetComponent<Character>() != null)
        {
            NPCTarget = this.gameObject.GetComponent<Character>();
        }
        if (this.gameObject.GetComponent<Item>() != null)
        {
            ItemTarget = this.gameObject.GetComponent<Item>();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void action()
    {
        if (NPCTarget != null)
        {
            Dialog.dialogMode = true;
        }
        if (ItemTarget != null)
        {
            // Move item to inventory; 
            if (inventory.FreeSlots > ItemTarget.spaceInInventory)
            {
                Destroy(ItemTarget.gameObject);
                inventory.AddItem(ItemTarget);
            }

        }
    }

    public void showInfo()
    {
        objectSelected = true;
        if (NPCTarget != null || ItemTarget != null)
        {
            NPCRenderer = this.gameObject.GetComponentInChildren<Renderer>();
            NPCRenderer.materials[0].shader = Shader.Find("Toon/Basic Outline");
        }
    }

    public void hideInfo()
    {
        objectSelected = false;
        if (NPCTarget != null || ItemTarget != null)
        {
            NPCRenderer.materials[0].shader = Shader.Find("Standard");
        }
    }

    void OnGUI()
    {
        if (objectSelected)
        {
            if (NPCTarget != null)
            {
                GUI.Box(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 40, NPCTarget.CharName.Length * 10, 30), NPCTarget.CharName);
            }
            if (ItemTarget != null)
            {
                GUI.Box(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 40, ItemTarget.itemName.Length * 10, 30), ItemTarget.itemName);
            }

        }
    }
}

