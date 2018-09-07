using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar
{
    public ActionBar()
    {
        this.MaxValue = 3f;
        this.CurrentValue = 0f;
        this.CurrentValue = Mathf.Clamp(this.CurrentValue, 0f, this.MaxValue);

        this.RegenerationSpeed = 1f;
        this.SpendindRate = 0.5f;
    }

    public float MaxValue;  
    public float CurrentValue; 

    public float RegenerationSpeed;     //  +CV per second
    public float SpendindRate;          //  -CV per second
    
}
