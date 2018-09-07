using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic
{
    public Statistic (float value, string name)
    {
        this.Name = name;
        this.BaseValue = value;
        this.CurrentValue = BaseValue;
    }

    public float BaseValue;
    public float CurrentValue;
    public string Name;

    public string[] Stats = new string[] {  "Endurance",
                                            "CombatSkill",
                                            "WillPower",
                                            "Agility",
                                            "Dexterity",
                                            "Cunning",
                                            "Intelligence",
                                            "Wisdom",
                                            "Talent"
    };

}
