using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class StatBar
{
    public StatBar(float value)
    {
        this.MaxValue = value;
        this.CurrentValue = MaxValue;
        Mathf.Clamp(this.CurrentValue, 0f, this.MaxValue);
    }

    public float MaxValue;  
    public float CurrentValue; 
    
}
