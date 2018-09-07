using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public static float DistanceFromTarget;
    private float ToTarget;
    public float MaxDistance = 5f;
    public Player player;

    public Camera cam;
    public Camera camFP;

    public LayerMask NPCLayer;  //doesn't work for some reason

    public static ActionBar CurrentActionBar;
    public static StatBar CurrentStatBar;

    public static bool performingActionRed = false;
    public static bool performingActionGreen = false;
    public static bool performingActionBlue = false;
    private bool isSpending = false;
    private float actionSpended = 0f;
    private int attackPower = 0;
    private bool actionEnabled = true;

    private Camera currentCam;

    private RaycastHit hitInfo;

    Character NPCTarget;
    Item ItemTarget;
    Renderer NPCRenderer;
    IActionObject iaObject;

    void Start()
    {
        //Renderer renderer = new Renderer();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MyBasicBehaviour.FPView)
        {
            currentCam = cam;
        }
        else
        {
            currentCam = camFP;
        }
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray fpsRay = new Ray(FirstViewCamera.gameObject.position, FirstViewCamera.gameObject.transform.forward);  //(Camera.main.transform.position, Camera.main.transform.forward);
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        int layerMask = NPCLayer.value;

        if (!GameControl.isPaused) 
        {
            // Just an alternative CM enter
            if (Input.GetButtonDown("CM enter"))
            {
                GameControl.CombatMode = !GameControl.CombatMode;
                AlternativeToggleCombatModeOn();
            }

            if (!GameControl.CombatMode)
            {
                // IF WE EVER NEED TO ENTER COMBAT MODE WITH INPUT

                /*
                if (Input.GetButtonDown("Attack") || Input.GetButtonDown("Use Cunning") || Input.GetButtonDown("Use Magic"))
                {
                    ToggleCombatModeOn();
                }
                */

                if (Physics.Raycast(ray, out hitInfo, MaxDistance, layerMask))
                {                    
                    ToTarget = hitInfo.distance;
                    DistanceFromTarget = ToTarget;

                    iaObject = hitInfo.collider.gameObject.GetComponent<IActionObject>();

                    if (iaObject != null)
                    {
                        iaObject.showInfo();

                        if (Input.GetButtonDown("Action"))
                        {
                            iaObject.action();
                        }

                    }
                }
                else if (iaObject != null)
                {
                    iaObject.hideInfo();    
                }                                        
            }

            else        //  IF BATTLE MODE ON
            {
                //Target selecting
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    ToTarget = hitInfo.distance;
                    DistanceFromTarget = ToTarget;

                    if (hitInfo.rigidbody != null)
                    {
                        if (DistanceFromTarget < AreaDetection.CurrentAbilityDistance)    //  Ability Distance
                        {
                            NPCTarget = hitInfo.rigidbody.gameObject.GetComponent<Character>();

                            if (CurrentStatBar.CurrentValue >= 1 && CurrentActionBar.CurrentValue >= 1 && !performingActionRed && !performingActionRed && !performingActionRed)
                            {
                                actionEnabled = true;
                            }

                            if (Input.GetButton("Attack") && actionEnabled == true && actionSpended <= 2f)
                            {
                                PerformAttack(AreaDetection.ActionType);
                            }
                            if (performingActionRed || performingActionGreen || performingActionBlue)
                            {
                                if (Input.GetButtonUp("Attack") || actionSpended > 2f || CurrentActionBar.CurrentValue == 0f)
                                {
                                    if (actionSpended < 1f)
                                    {
                                        CurrentActionBar.CurrentValue -= (1f - actionSpended);
                                        CurrentActionBar.CurrentValue = Mathf.Clamp(CurrentActionBar.CurrentValue, 0f, CurrentActionBar.MaxValue);
                                        ReleaseAttack(AreaDetection.ActionType, 1);
                                    }
                                    else if (actionSpended >= 1f && actionSpended < 2f)
                                    {
                                        CurrentActionBar.CurrentValue -= (2f - actionSpended);
                                        CurrentActionBar.CurrentValue = Mathf.Clamp(CurrentActionBar.CurrentValue, 0f, CurrentActionBar.MaxValue);
                                        ReleaseAttack(AreaDetection.ActionType, 2);
                                    }
                                    else
                                    {
                                        CurrentActionBar.CurrentValue -= (3f - actionSpended);
                                        CurrentActionBar.CurrentValue = Mathf.Clamp(CurrentActionBar.CurrentValue, 0f, CurrentActionBar.MaxValue);
                                        ReleaseAttack(AreaDetection.ActionType, 3);
                                    }
                                }
                            }
                        }
                    }
                }

                //Block can be performed from 0 dist
                if (Input.GetButton("Block"))
                {
                    PerformBlock(AreaDetection.ActionType);
                }
                if (Input.GetButtonUp("Block"))
                {
                    ReleaseBlock(AreaDetection.ActionType);
                }

            }
        }

    }

    void PerformAttack(int attackType)
    {
        //decrease AB current and increase some var then use this var for release
        Debug.Log("PerformAttack"); // there must be animation
        switch (attackType)
        {
            case 0:
                performingActionRed = true;
                break;
            case 1:
                performingActionGreen = true;
                break;
            case 2:
                performingActionBlue = true;
                break;
            default:
                break;
        }
        //if (!isSpending)
        //{
            //StartCoroutine(SpendingAB(CurrentActionBar));
            SpendingAB(CurrentActionBar);
        //}
        //CurrentActionBar.CurrentValue -= CurrentActionBar.SpendindRate
    }

    void ReleaseAttack(int attackType, int attackPower)
    {
        //decrese SB current value and cast ability whatever this means
        CurrentStatBar.CurrentValue -= 1f;

        switch (attackType)
        {
            case 0:
                performingActionRed = false;
            break;
            case 1:
                performingActionGreen = false;
            break;
            case 2:
                performingActionBlue = false;
            break;
            default:
                break;
        }
        actionEnabled = false;
        actionSpended = 0;
        Debug.Log("ReleaseAttack"); // there must be animation
    }

    void PerformBlock(int blockType)
    {
        //decrease AB current and wait until incomming attack or perform ability
        switch (blockType)
        {
            case 0:
                performingActionRed = true;
                break;
            case 1:
                performingActionGreen = true;
                break;
            case 2:
                performingActionBlue = true;
                break;
            default:
                break;
        }
        //if (!isSpending)
        //{
            //StartCoroutine(SpendingAB(CurrentActionBar));
            SpendingAB(CurrentActionBar);
        //}

        Debug.Log("PerformBlock");  // there must be animation
    }
    
    void ReleaseBlock(int blockType)
    {
        switch (blockType)
        {
            case 0:
                performingActionRed = false;
                break;
            case 1:
                performingActionGreen = false;
                break;
            case 2:
                performingActionBlue = false;
                break;
            default:
                break;
        }
        actionSpended = 0;
        actionEnabled = false;
        Debug.Log("ReleaseBlock");  // there must be animation
    }

    void SpendingAB(ActionBar ab)
    {
        //isSpending = true;
        //yield return new WaitForSeconds(1f);
        //Debug.Log("regenetating_" + ab.CurrentValue);
        //ab.CurrentValue -= ab.SpendindRate;
        ab.CurrentValue = Mathf.MoveTowards(ab.CurrentValue, 0f, ab.SpendindRate * Time.deltaTime);
        ab.CurrentValue = Mathf.Clamp(ab.CurrentValue, 0f, ab.MaxValue);
        //actionSpended += ab.SpendindRate;
        actionSpended = Mathf.MoveTowards(actionSpended, 3f, ab.SpendindRate * Time.deltaTime);
        actionSpended = Mathf.Clamp(actionSpended, 0f, 3f);
        Debug.Log(actionSpended);
        //isSpending = false;

    }

    void ToggleCombatModeOn()
    {
        PlayerStatsUI.ActionBarRed.CurrentValue = 0f;
        PlayerStatsUI.ActionBarGreen.CurrentValue = 0f;
        PlayerStatsUI.ActionBarBlue.CurrentValue = 0f;
        GameControl.CombatMode = true;
        //PlayerStatsCanvas.SetActive(true);
    }

    // Function for tests only

    void AlternativeToggleCombatModeOn()
    {
        PlayerStatsUI.ActionBarRed.CurrentValue = 0f;
        PlayerStatsUI.ActionBarGreen.CurrentValue = 0f;
        PlayerStatsUI.ActionBarBlue.CurrentValue = 0f;
        //PlayerStatsUI.CombatMode = true;
        //PlayerStatsCanvas.SetActive(true);
    }

    
}

