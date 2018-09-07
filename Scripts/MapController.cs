using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        hexMap = GameObject.FindObjectOfType<Map>();
    }

    Map hexMap;
    Hex currentHex;

    public LayerMask LayerIDForHexTitles;

    [SerializeField] private string loadLevel;

    Hex PositionToHex()
    {
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward); ;
        RaycastHit hitInfo;

        int layerMask = LayerIDForHexTitles.value;

        if (Physics.Raycast(cameraRay, out hitInfo, Mathf.Infinity))
        {
            GameObject hexGO = hitInfo.rigidbody.gameObject;

            return hexMap.GetHexFromGameObject(hexGO);
        }

        Debug.Log("Nothing");
        return null;
    }

    

    // Timepass (must be global)

    // Update is called once per frame
    void Update()
    {
        currentHex = PositionToHex();
        // Function for entering locations
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(loadLevel);
        }
        //Debug.Log(currentHex.Q + "_" + currentHex.R);
    }

}
