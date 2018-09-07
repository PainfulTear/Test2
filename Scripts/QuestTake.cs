using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTake : MonoBehaviour {

    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject UIQuest;
    public GameObject ThePlayer;
    public GameObject NoticeCam;

	// Update is called once per frame
	void Update () {
        TheDistance = MouseController.DistanceFromTarget;
	}

    void OnMouseOver ()
    {
        if (TheDistance <= 3)
        {
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }

        if (Input.GetButtonDown ("Action"))
        {
            if (TheDistance <= 3)
            {
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                UIQuest.SetActive(true);                
                ThePlayer.SetActive(false);
            }
        }
    }
        
    void OnMouseExit ()
    {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }

}
