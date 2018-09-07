using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour {

    public LayerMask NPCLayer;    //doesn't work for some reason
    Renderer NPCRenderer;
    SphereCollider detectionArea;
    public Projector projector;
    public Player player;
    public static float CurrentAbilityDistance = 5f;
    public static int ActionType = 0;

    private int ActionDetector = 0;

    // Use this for initialization
    void Start () {
        detectionArea = player.GetComponentInChildren<SphereCollider>();
        ActionType = 0;
        
        MouseController.CurrentActionBar = PlayerStatsUI.ActionBarRed;
        MouseController.CurrentStatBar = player.RedStatBar;
    }
	
	// Update is called once per frame
	void Update () {
        //  Highlighting possible targets in combat mode
        if (!GameControl.isPaused)
        {
            if (GameControl.CombatMode)
            {
                //ActionDetector = 0;
                //CurrentAbilityDistance = 5f;

                if (Input.GetButton("Action") && ActionType == 0)
                {
                    //detectionArea.enabled = true;
                    detectionArea.radius = 5f;
                    CurrentAbilityDistance = 5f;
                    ActionType = 0;
                    projector.material.color = Color.red;
                    projector.orthographicSize = 5f + 1f;
                    projector.enabled = true;
                    ActionDetector = 0;
                }

                if (Input.GetButton("Use Cunning") && ActionType == 0)
                {
                    //detectionArea.enabled = true;
                    detectionArea.radius = 3f;
                    CurrentAbilityDistance = 3f;
                    ActionType = 1;
                    MouseController.CurrentActionBar = PlayerStatsUI.ActionBarGreen;
                    MouseController.CurrentStatBar = player.GreenStatBar;
                    projector.material.color = Color.green;
                    projector.orthographicSize = 3f + 1f;
                    projector.enabled = true;
                    ActionDetector = 1;
                }

                if (Input.GetButton("Use Magic") && ActionType == 0)
                {
                    //detectionArea.enabled = true;
                    detectionArea.radius = 10f;
                    CurrentAbilityDistance = 10f;
                    ActionType = 2;
                    MouseController.CurrentActionBar = PlayerStatsUI.ActionBarBlue;
                    MouseController.CurrentStatBar = player.BlueStatBar;
                    projector.material.color = Color.blue;
                    projector.orthographicSize = 10f + 1f;
                    projector.enabled = true;
                    ActionDetector = 2;
                }

                if (Input.GetButtonUp("Action") && ActionType == 0)
                {
                    detectionArea.radius = 0.1f;
                    projector.enabled = false;
                }

                if (Input.GetButtonUp("Use Cunning") && ActionType == 1)
                {
                    ActionType = 0;
                    MouseController.CurrentActionBar = PlayerStatsUI.ActionBarRed;
                    MouseController.CurrentStatBar = player.RedStatBar;
                    detectionArea.radius = 0.1f;
                    projector.enabled = false;
                }

                if (Input.GetButtonUp("Use Magic") && ActionType == 2)
                {
                    ActionType = 0;
                    MouseController.CurrentActionBar = PlayerStatsUI.ActionBarRed;
                    MouseController.CurrentStatBar = player.RedStatBar;
                    detectionArea.radius = 0.1f;
                    projector.enabled = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer + " " + NPCLayer.value);
        if (other.gameObject.layer == 9 && GameControl.CombatMode)    //  magic number
        {
            NPCRenderer = other.gameObject.GetComponentInChildren<Renderer>();
            NPCRenderer.materials[0].shader = Shader.Find("Toon/Basic Outline");
            switch (ActionDetector)
            {
                case 0:
                    NPCRenderer.materials[0].color = Color.red;
                    break;
                case 1:
                    NPCRenderer.materials[0].color = Color.green;
                    break;
                case 2:
                    NPCRenderer.materials[0].color = Color.blue;
                    break;
                default:
                    break;
            }
            }
       Debug.Log(ActionDetector);    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)    //  magic number
        {
            NPCRenderer = other.gameObject.GetComponentInChildren<Renderer>();
            NPCRenderer.materials[0].shader = Shader.Find("Standard");
            
            NPCRenderer.materials[0].color = Color.black;           
        }
        //Debug.Log(other.bounds.size+ "!!!");    
    }
}
