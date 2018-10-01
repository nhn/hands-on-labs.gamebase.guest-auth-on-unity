using System.Collections;
using System.Collections.Generic;
using Toast.Gamebase;
using UnityEngine;
using UnityEngine.UI;

public class GamebaseGuestSample : MonoBehaviour {

    public Text statusText = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GamebaseLogout()
    {
    }

    public void GamebaseGuestLogin()
    {
    }

    public void GamebaseInitialize()
    {
    }

    private bool IsPlayable(int status)
    {
        if (200 <= status && status < 300)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
