using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour {

    public GameObject PlayerStatsCanvas;

    public GameObject MaxHealthBar;
    public GameObject CurrentHealthBar;
    public GameObject MaxCunningBar;
    public GameObject CurrentCunningBar;
    public GameObject MaxMagicBar;
    public GameObject CurrentMagicBar;
    public GameObject MaxMoveBar;
    public GameObject CurrentMoveBar;

    public static ActionBar ActionBarRed;
    public static ActionBar ActionBarGreen;
    public static ActionBar ActionBarBlue;

    public GameObject player;

    //public static bool CombatMode = false;
    public Texture2D redBarTex;
    public Texture2D greenBarTex;
    public Texture2D blueBarTex;
    public Texture2D greyBarTex;

    //private RectTransform healthBar;
    private bool isRegenerated = false;


    // Use this for initialization
    void Start () {
        ActionBarRed = new ActionBar();
        ActionBarBlue = new ActionBar();
        ActionBarGreen = new ActionBar();
    }
	
	// Update is called once per frame
	void Update () {

        //  Updating statbars

        //healthBar = MaxHealthBar.GetComponent<RectTransform>();
        //healthBar.localScale = new Vector3(player.GetComponent<Player>().RedStatBar.MaxValue / 90f, 1, 1);

        //  Toggle Combat Mode
        


            //Regenerating AB in combat

            if (GameControl.CombatMode)
            {
                if (!MouseController.performingActionRed)
                {
                    //StartCoroutine(Regenerate(ActionBarRed));
                    Regenerate(ActionBarRed);
                }
                if (!MouseController.performingActionGreen)
                {
                    //StartCoroutine(Regenerate(ActionBarGreen));
                    Regenerate(ActionBarGreen);
                }
                if (!MouseController.performingActionBlue)
                {
                    //StartCoroutine(Regenerate(ActionBarBlue));
                    Regenerate(ActionBarBlue);
                }
            }
        
    }

    IEnumerator ToggleCombatModeOff()
    {
        yield return new WaitForSeconds(10f);
        PlayerStatsCanvas.SetActive(false);
    }

    void Regenerate(ActionBar ab)
    {
        //float targetValue = ab.CurrentValue + ab.RegenerationSpeed; ;
        //isRegenerated = true;
        //yield return new WaitForSeconds(0.1f);
        //Debug.Log("regenetating_" + ab.CurrentValue);
        //ab.CurrentValue += ab.RegenerationSpeed;
        ab.CurrentValue = Mathf.MoveTowards(ab.CurrentValue, ab.MaxValue, ab.RegenerationSpeed*Time.deltaTime);
        ab.CurrentValue = Mathf.Clamp(ab.CurrentValue, 0f, ab.MaxValue);
        //isRegenerated = false;
        
    }

    void OnGUI ()
    {
        if (GameControl.CombatMode)
        {
            GUI.Box(new Rect(20, 20, player.GetComponent<Player>().RedStatBar.MaxValue * 10, 10), "");
            GUI.DrawTexture(new Rect(20, 20, player.GetComponent<Player>().RedStatBar.CurrentValue * 10, 10), redBarTex);
            GUI.Box(new Rect(20, 40, player.GetComponent<Player>().GreenStatBar.MaxValue * 10, 10), "");
            GUI.DrawTexture(new Rect(20, 40, player.GetComponent<Player>().GreenStatBar.CurrentValue * 10, 10), greenBarTex);
            GUI.Box(new Rect(20, 60, player.GetComponent<Player>().BlueStatBar.MaxValue * 10, 10), "");
            GUI.DrawTexture(new Rect(20, 60, player.GetComponent<Player>().BlueStatBar.CurrentValue * 10, 10), blueBarTex);
            GUI.Box(new Rect(20, 80, player.GetComponent<Player>().GreyStatBar.MaxValue * 10, 10), "");
            GUI.DrawTexture(new Rect(20, 80, player.GetComponent<Player>().GreyStatBar.CurrentValue * 10, 10), greyBarTex);

            GUI.Box(new Rect(20, 600, ActionBarRed.MaxValue * 30, 20), "");
            GUI.DrawTexture(new Rect(20, 600, ActionBarRed.CurrentValue * 30, 20), redBarTex);
            GUI.Box(new Rect(20, 630, ActionBarGreen.MaxValue * 30, 20), "");
            GUI.DrawTexture(new Rect(20, 630, ActionBarGreen.CurrentValue * 30, 20), greenBarTex);
            GUI.Box(new Rect(20, 660, ActionBarBlue.MaxValue * 30, 20), "");
            GUI.DrawTexture(new Rect(20, 660, ActionBarBlue.CurrentValue * 30, 20), blueBarTex);
        }
    }
}
