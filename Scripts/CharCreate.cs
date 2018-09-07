using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharCreate : MonoBehaviour {

     //("PlayerName", 0, new int[] {15, 15, 15, 15, 15, 15, 15, 15, 15}, 0); // TODO: remove "magick numbers";

    public Dropdown charRaceDropdown;
    public Dropdown charAllignmentDropdown;
    public Dropdown charClassDropdown;
    public Dropdown charSexDropdown;
    public InputField charNameField;

    public string[] races;
    public string[] allignments;
    public string[] classes;
    Player player;
    public GameObject char_shadow;

    private int Endurance = 10;
    private int CombatSkill = 10;
    private int WillPower = 10;
    private int Agility = 10;
    private int Dexterity = 10;
    private int Cunning = 10;
    private int Intelligence = 10;
    private int Wisdom = 10;
    private int Talent = 10;

    // Use this for initialization
    void Awake()
    {
        //player = new Player(name, 0, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 0);
          
        charRaceDropdown.onValueChanged.AddListener(delegate { OnCharRaceChange(); });
        charAllignmentDropdown.onValueChanged.AddListener(delegate { OnCharAllignmentChange(); });
        //charClassDropdown.onValueChanged.AddListener(delegate { OnCharClassChange(); });
        charSexDropdown.onValueChanged.AddListener(delegate { OnCharSexChange(); });

        races = player.Races;
        foreach (string race in races)
        {
            charRaceDropdown.options.Add(new Dropdown.OptionData(race));
        }

        allignments = player.Aliignments;
        foreach (string allignment in allignments)
        {
            charAllignmentDropdown.options.Add(new Dropdown.OptionData(allignment));
        }

        classes = new string[] { "Warrior", "Mage", "Thief" };
        foreach (string charclass in classes)
        {
            charClassDropdown.options.Add(new Dropdown.OptionData(charclass));
        }
    }

    public void OnEnable ()
    {

    }

    public void OnCharRaceChange()
    {
        player.CharRace = charRaceDropdown.value;
    }

    public void OnCharAllignmentChange()
    {
        player.Allignment = charAllignmentDropdown.value;
    }

    public void OnCharSexChange()
    {
        if (charSexDropdown.value == 0)
        {
            player.CharSex = true;
        }
        else
        {
            player.CharSex = false;
        }
    }

    public void StartGame()
    {
        //  Send charinfo to gamecontrol
        GameControl.Endurance = Endurance;
        GameControl.CombatSkill = CombatSkill;
        GameControl.WillPower = WillPower;
        GameControl.Agility = Agility;
        GameControl.Dexterity = Dexterity;
        GameControl.Cunning = Cunning;
        GameControl.Intelligence = Intelligence;
        GameControl.Wisdom = Wisdom;
        GameControl.Talent = Talent;

        if (charNameField.text != "")
        {
            GameControl.Name = charNameField.text;
            if (player.CharRace < 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
