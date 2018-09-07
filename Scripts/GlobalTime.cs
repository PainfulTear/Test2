using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour {

    public static float secondsFromStart = 0;
    public static float minutesFromStart = 0;
    public static float hoursFromStart = 9;
    public static float daysFromStart = 10;
    public static float monthsFromStart = 8;
    public static float yearsFromStart = 90;

	// Use this for initialization
	void Start () {
        secondsFromStart = Time.timeSinceLevelLoad;        
	}
	
	// Update is called once per frame
	void Update () {
        secondsFromStart += Time.deltaTime; //* 10000f;
        if (secondsFromStart >= 60)
        {
            secondsFromStart = 0;
            minutesFromStart++;
        }
        if (minutesFromStart >= 60)
        {
            minutesFromStart = 0;
            hoursFromStart++;
        }
        if (hoursFromStart >= 24)
        {
            hoursFromStart = 0;
            daysFromStart++;
        }
        if (daysFromStart >= 28)
        {
            daysFromStart = 0;
            monthsFromStart++;
        }
        if (monthsFromStart >= 12)
        {
            monthsFromStart = 0;
            yearsFromStart++;
        }
    }
}
