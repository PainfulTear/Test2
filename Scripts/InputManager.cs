using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour {

    void OnEnable()
    {
        buttonKeys = new Dictionary<string, KeyCode>();

        // TODO: consider reading this from a user preferences file
        buttonKeys["Attack"] = KeyCode.Mouse0;
        buttonKeys["Attack2"] = KeyCode.Mouse1;
        buttonKeys["Action"] = KeyCode.E;
        buttonKeys["Jump"] = KeyCode.Space;
        buttonKeys["Use Cunning"] = KeyCode.LeftAlt;
        buttonKeys["Use Magic"] = KeyCode.Q;

    }

    void Start ()
    {
		
	}

    Dictionary<string, KeyCode> buttonKeys;

	// Update is called once per frame
	void Update () {
		
	}

    public bool GetButtonDown (string buttonName)
    {
        if (!buttonKeys.ContainsKey(buttonName))
        {
            Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);
            return false;
        }

        return Input.GetKeyDown(buttonKeys[buttonName]);
    }

    public bool GetButton(string buttonName)
    {
        if (!buttonKeys.ContainsKey(buttonName))
        {
            Debug.LogError("InputManager::GetButton -- No button named: " + buttonName);
            return false;
        }

        return Input.GetKey(buttonKeys[buttonName]);
    }

    public bool GetButtonUp(string buttonName)
    {
        if (!buttonKeys.ContainsKey(buttonName))
        {
            Debug.LogError("InputManager::GetButtonUp -- No button named: " + buttonName);
            return false;
        }

        return Input.GetKeyUp(buttonKeys[buttonName]);
    }

    public string[] GetButtonNames()
    {
        return buttonKeys.Keys.ToArray();
    }

    public string GetKeyNameForButton (string buttonName)
    {
        if (!buttonKeys.ContainsKey(buttonName))
        {
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return "N/A";
        }

        return buttonKeys[buttonName].ToString();
    }

    public void SetButtonForKey (string buttonName, KeyCode keyCode)
    {
        buttonKeys[buttonName] = keyCode;
    }
}
