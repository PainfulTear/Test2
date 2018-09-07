using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IAction {

    public string itemName;
    public int type;
    public int stackCount;
    public int spaceInInventory;
    public int equipPosition;
    public Sprite itemIcon;

    public GameObject itemPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void action ()
    {

    }

    public void showInfo()
    {

    }

    public void hideInfo()
    {

    }
}
