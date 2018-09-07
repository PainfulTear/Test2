using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public StatBar RedStatBar;
    public StatBar BlueStatBar;
    public StatBar GreenStatBar;
    public StatBar GreyStatBar;
    public StatBar Total;

    /*public Character(string name, int race, int[] Stats, int allignment)
    {
        this.CharName = name;
        this.CharRace = race;

        this.Endurance = Stats[0];
        this.CombatSkill = Stats[1];
        this.WillPower = Stats[2];
        this.Agility = Stats[3];
        this.Dexterity = Stats[4];
        this.Cunning = Stats[5];
        this.Intelligence = Stats[6];
        this.Wisdom = Stats[7];
        this.Talent = Stats[8];

        this.Allignment = allignment;

        RedStatBar = new StatBar(Stats[0] + Stats[1] + Stats[2]);
        BlueStatBar = new StatBar(Stats[0] + Stats[1] + Stats[2]);
        GreenStatBar = new StatBar(Stats[0] + Stats[1] + Stats[2]);
        GreyStatBar = new StatBar(15);
        Total = new StatBar(RedStatBar.MaxValue + BlueStatBar.MaxValue + GreenStatBar.MaxValue + GreyStatBar.MaxValue);
    }*/

    public int CharRace;        //  Character's race
    public bool CharSex;        //  Character's sex
    public int Allignment;      //  Character's alignment
    public string CharName;     //  Character's name

    public int Endurance;
    public int CombatSkill;
    public int WillPower;
    public int Agility;
    public int Dexterity;
    public int Cunning;
    public int Intelligence;
    public int Wisdom;
    public int Talent;

    public string[] Aliignments = new string[] {    "Lawful Good",
                                                    "Lawful Neutral",
                                                    "Lawful Evil",
                                                    "Neutral Good",
                                                    "True Neutral",
                                                    "Neutral Evil",
                                                    "Chaotic Good",
                                                    "Chaotic Neutral",
                                                    "Chaotic Evil"
    };

    public string[] Races = new string[] {  "Human",
                                            "Dwarf",
                                            "Elf",
                                            "Drow",
                                            "Lizard",
                                            "Cat",
                                            "Monkey"
    };

    void Start ()
    {
        RedStatBar = new StatBar(this.Endurance + this.CombatSkill + this.WillPower);
        BlueStatBar = new StatBar(this.Agility + this.Dexterity + this.Cunning);
        GreenStatBar = new StatBar(this.Intelligence + this.Wisdom + this.Talent);
        GreyStatBar = new StatBar(15);
        Total = new StatBar(RedStatBar.MaxValue + BlueStatBar.MaxValue + GreenStatBar.MaxValue + GreyStatBar.MaxValue);
    }

    void Update ()
    {
        if (Total.CurrentValue <=0)
        {
            this.gameObject.SetActive(false);
        }
    }

}


