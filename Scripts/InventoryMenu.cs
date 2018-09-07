using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour {

    public GameObject InventoryMenuCanvas;

    public GameObject Timer;

    public GameObject[] inventorySlots = new GameObject[10];
    public GameObject[] equipmentSlots = new GameObject[11];
    public GameObject[] weaponSlots = new GameObject[4];
    public GameObject[] abilitySlots = new GameObject[6];


    public GameObject container;

    public GameObject[] Stats = new GameObject[9];

    public Inventory inventory;

    public static bool InventoryMenuToggle = false;

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update () {

        //DisplayItems();
        // TODO REMOVE THIS FROM UPDATE ???

        if (InventoryMenuToggle)
        {
            InventoryMenuCanvas.SetActive(true);
        }
        else
        {
            InventoryMenuCanvas.SetActive(false);
        }

        if (Input.GetButtonUp("Inventory") && !GameMenu.GameMenuToggle)
        {
            if (InventoryMenuToggle)
            {
                HideItems();
            }
            else
            {
                DisplayItems();
            }
            Statistic endurance = new Statistic(GameControl.Endurance, "Endurance");
            Statistic combatSkill = new Statistic(GameControl.CombatSkill, "CombatSkill"); ;
            Statistic willPower = new Statistic(GameControl.WillPower, "WillPower"); ;
            Statistic agility = new Statistic(GameControl.Agility, "Agility"); ;
            Statistic dexterity = new Statistic(GameControl.Dexterity, "Dexterity"); ;
            Statistic cunning = new Statistic(GameControl.Cunning, "Cunning"); ;
            Statistic intelligence = new Statistic(GameControl.Intelligence, "Intelligence"); ;
            Statistic wisdom = new Statistic(GameControl.Wisdom, "Wisdom"); ;
            Statistic talent = new Statistic(GameControl.Talent, "Talent"); ;
            Statistic[] stats = new Statistic[] {   endurance,
                                                    combatSkill,
                                                    willPower,
                                                    agility,
                                                    dexterity,
                                                    cunning,
                                                    intelligence,
                                                    wisdom,
                                                    talent };
            int i = 0;
            foreach (Statistic stat in stats) {
                //stat.GetComponent<TextMeshProUGUI>().text = Endurance.GetComponent<TextMeshProUGUI>().text + " " + GameControl.Endurance.ToString();

                Stats[i].GetComponent<TextMeshProUGUI>().text = stat.Name + " " + stat.CurrentValue.ToString();
                i++;
            }

            Timer.GetComponent<TextMeshProUGUI>().text = GlobalTime.yearsFromStart.ToString() + " y " + GlobalTime.monthsFromStart.ToString() + 
                " m " + GlobalTime.daysFromStart.ToString() + " d " + GlobalTime.hoursFromStart.ToString() + " h " + GlobalTime.minutesFromStart.ToString() + " min ";                
            //+ GlobalTime.secondsFromStart.ToString();

            InventoryMenuToggle = !InventoryMenuToggle;
            GameControl.isPaused = !GameControl.isPaused;
        }
    }

    public void Close ()
    {
        InventoryMenuCanvas.SetActive(false);
        InventoryMenuToggle = !InventoryMenuToggle;
        GameControl.isPaused = !GameControl.isPaused;
    }

    public void DisplayItems ()
    {
        int currentPosition = 0;
        foreach (Item item in inventory.itemsInInventory)
        {
            for (int i = 0; i < item.spaceInInventory; i++)
            {
                GameObject img = Instantiate(container);
                img.transform.SetParent(inventorySlots[currentPosition + i].transform);
                //Image image = inventorySlots[currentPosition + i].GetComponent<Image>();
                img.GetComponent<Image>().sprite = item.itemIcon;
                if (i != 0)
                {
                    img.GetComponent<Image>().color = Color.HSVToRGB(0.5f, 0.5f, 0.5f);                   
                }
            }
            currentPosition += item.spaceInInventory;
            //ChangeIcon for item #
        }
    }

    public void HideItems()
    {
        foreach (GameObject inventorySlot in inventorySlots)
        {
            if (inventorySlot.transform.childCount > 0)
            {
                Destroy(inventorySlot.transform.GetChild(0));
            }
        }
    }

    public void onRightClick (Item item)
    {
        if (Input.GetMouseButtonDown(1))
        {
            inventory.RemoveItem(item);
        }
    }
}

public class InventorySlot : MonoBehaviour {

    int count;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //inventory.RemoveItem(item);
        }
    }

}
