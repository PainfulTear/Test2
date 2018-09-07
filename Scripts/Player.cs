using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Character
{
    public static Player control;

    //StateBar RedStateBar;
    //StateBar BlueStateBar;
    //StateBar GreenStateBar;
    //StateBar GreyStateBar;

   // public Player (string name, int race, int[] Stats, int allignment)
   //     : base(name, race, Stats, allignment)
   // {
        //RedStateBar = new StateBar(Stats[0] + Stats[1] + Stats[2]);
        //BlueStateBar = new StateBar(Stats[0] + Stats[1] + Stats[2]);
        //GreenStateBar = new StateBar(Stats[0] + Stats[1] + Stats[2]);
        //GreyStateBar = new StateBar(15);
   // }

    //public string PlayerName;

    void Start()
    {
        Endurance = GameControl.Endurance;
        CombatSkill = GameControl.CombatSkill;
        WillPower = GameControl.WillPower;
        Agility = GameControl.Agility;
        Dexterity = GameControl.Dexterity;
        Cunning = GameControl.Cunning;
        Intelligence = GameControl.Intelligence;
        Wisdom = GameControl.Wisdom;
        Talent = GameControl.Talent;
        RedStatBar = new StatBar(this.Endurance + this.CombatSkill + this.WillPower);
        BlueStatBar = new StatBar(this.Agility + this.Dexterity + this.Cunning);
        GreenStatBar = new StatBar(this.Intelligence + this.Wisdom + this.Talent);
        GreyStatBar = new StatBar(15);
        Total = new StatBar(RedStatBar.MaxValue + BlueStatBar.MaxValue + GreenStatBar.MaxValue + GreyStatBar.MaxValue);
    }

    void Awake()
    {/*
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }*/
    }
}

