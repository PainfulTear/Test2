using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    public GameObject DialogCanvas;
    public DialogNode[] node;
    public int _currentNode;
    public static bool dialogMode = false;

    // Use this for initialization
    void Update () {
		
	}

    public void ShowDialog()
    {
        //DialogCanvas.SetActive(true);
        dialogMode = true;
        GameControl.isPaused = true;
    }


    public void End()
    {
        DialogCanvas.SetActive(false);
        dialogMode = false;
        GameControl.isPaused = false;
    }

    void OnGUI()
    {
        
        if (dialogMode)
        {
            ShowDialog();

            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90),               
                node[_currentNode].NPCText);
            foreach (Answer answer in node[_currentNode].PlayerAnswer) {
                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90),
                    answer.Text))
                {
                    if (answer.SpeakEnd)
                    {
                        End();
                    }
                    _currentNode = answer.ToNode;
                }
            }
            
        }
    }
}

[System.Serializable]
public class DialogNode
{
    public string NPCText;
    public Answer[] PlayerAnswer;
}

[System.Serializable]
public class Answer
{
    public string Text;
    public int ToNode;
    public bool SpeakEnd;
}