using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Username : MonoBehaviour {

    public string username = "";


	// Use this for initialization
	void Start () {
		
	}

    private void Zapisi_rezultate()
    {
        using (StreamWriter sw = new StreamWriter("C:/Users/lukas/Desktop/rezultati_unity.txt", true))
        {
            sw.Write("/" + username + "|" + LifePoints_P.gamepoints);
            GameModes.game_ended = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(GameModes.game_ended == true)
        {
            //Zapisi_rezultate();
        }
	}
}
