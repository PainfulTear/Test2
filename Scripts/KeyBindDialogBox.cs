using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindDialogBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
        inputManager = FindObjectOfType<InputManager>();

        string[] buttonNames = inputManager.GetButtonNames();
        buttonToLabel = new Dictionary<string, TextMeshProUGUI>();

        for (int i = 0; i < buttonNames.Length; i++)
        {
            string bn;
            bn = buttonNames[i];

            GameObject go = Instantiate(keyItemPrefab);
            go.transform.SetParent(keyList.transform);
            go.transform.localScale = Vector3.one;

            TextMeshProUGUI buttonNameText = go.transform.Find("Button Name").GetComponent<TextMeshProUGUI>();
            buttonNameText.text = bn;

            TextMeshProUGUI keyNameText = go.transform.Find("Button/Key Name").GetComponent<TextMeshProUGUI>();
            keyNameText.text = inputManager.GetKeyNameForButton(bn);
            buttonToLabel[bn] = keyNameText;

            Button keyBindButton = go.transform.Find("Button").GetComponent<Button>();
            keyBindButton.onClick.AddListener(() => { StartRebindForKey(bn); });

        }

	}

    InputManager inputManager;
    public GameObject keyItemPrefab;
    public GameObject keyList;

    public GameObject videoTab;
    public GameObject controlsTab;

    string buttonToRebind = null;
    Dictionary<string, TextMeshProUGUI> buttonToLabel;

    // Update is called once per frame
    void Update ()
    {
		if (buttonToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                Array kcs = Enum.GetValues(typeof(KeyCode));

                foreach(KeyCode kc in kcs)
                {
                    if(Input.GetKeyDown(kc))
                    {
                        inputManager.SetButtonForKey(buttonToRebind, kc);
                        break;
                    }
                }
            }
        }
	}

    public void VideoTab()
    {
        videoTab.SetActive(true);
        controlsTab.SetActive(false);
    }

    public void ControlsTab ()
    {
        videoTab.SetActive(false);
        controlsTab.SetActive(true);
    }

    void StartRebindForKey(string buttonName)
    {
        buttonToRebind = buttonName;
    }
}
