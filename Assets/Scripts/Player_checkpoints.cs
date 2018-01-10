using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_checkpoints : MonoBehaviour {
    public static GameObject[] checkpoints;
    public int index = 0;
    // Use this for initialization
    Vector3[] spawn_points = new Vector3[5];
    Vector3 sp1 = new Vector3(189.2f, 1.5f, 53.1f);
    Vector3 sp2 = new Vector3(87.6f, 13f, 247.3f);
    Vector3 sp3 = new Vector3(249f, 1.5f, 416f);
    Vector3 sp4 = new Vector3(465f, 1.5f, 213f);
    Vector3 sp5 = new Vector3(388.6f, 1.5f, 62.8f);
    GameObject player;
    GameObject firstAid;
    public GameObject[] power_ups;

    public GameObject powerup;
    public GameObject warzone;

    public static float time = 0.0f;
    public bool startTimer = false;

    public int position;

    public static int active_checkpoint;

    public GameModes gm;

    void Start() {
        powerup = GameObject.FindGameObjectWithTag("Powerup");
        warzone = GameObject.FindGameObjectWithTag("WarZone");


        player = GameObject.FindGameObjectWithTag("Player");
        GameObject shield = (GameObject)Resources.Load("PU_shield");
        GameObject nails = (GameObject)Resources.Load("PU_nails");
        GameObject fake = (GameObject)Resources.Load("PU_fake");
        GameObject rocket = (GameObject)Resources.Load("PU_rocket");
        GameObject star = (GameObject)Resources.Load("Star_Rocket");
        firstAid = (GameObject)Resources.Load("FirstAid");

        spawn_points[0] = sp1;
        spawn_points[1] = sp2;
        spawn_points[2] = sp3;
        spawn_points[3] = sp4;
        spawn_points[4] = sp5;

        power_ups[0] = shield;
        power_ups[1] = nails;
        power_ups[2] = fake;
        power_ups[3] = rocket;
        //power_ups[4] = star;

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            string name = "Checkpoint" + (i+1).ToString();
            //Debug.Log(name + " svija");
            checkpoints[i] = GameObject.Find(name);
            if (checkpoints[i].name != "Checkpoint1") 
                checkpoints[i].SetActive(false);

            //Debug.Log("Polje[" + i + "] = " + checkpoints[i].name);
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            if (index == 3)
            {
                checkpoints[index].SetActive(false);
                checkpoints[index + 1].SetActive(true);
                checkpoints[index + 2].SetActive(true);
                index = 5;
                active_checkpoint = index + 2;
            }
            else if (collision.gameObject.name == "Checkpoint5" || collision.gameObject.name == "Checkpoint6")
            {
                checkpoints[index].SetActive(false);
                checkpoints[index - 1].SetActive(false);
                checkpoints[index + 1].SetActive(true);
                checkpoints[index + 2].SetActive(true);
                active_checkpoint = index + 2;
                index = 7;
            }
            else if (collision.gameObject.name == "Checkpoint7" || collision.gameObject.name == "Checkpoint8")
            {
                checkpoints[index].SetActive(false);
                checkpoints[index - 1].SetActive(false);
                checkpoints[index + 1].SetActive(true);
                active_checkpoint = index + 1;
                index++;
            }
            else if ((index >= 0 && index < 3) || (index > 8 && index < 13))
            {
                if(GameModes.ta == true)
                {
                    if (index == 0)
                    {
                        time = 0.0f;
                        startTimer = true;
                    }
                    if(index == 1)
                    {
                        Debug.Log("Cas kroga : " + time + " s");
                        time = 0.0f;
                    }
                }

                if(index == 0)
                {
                    //time = 0.0f;
                    //startTimer = true;
                    if (GameModes.wz == true)
                    {
                        for (int i = 0; i < spawn_points.Length; i++)
                        {
                            Instantiate(firstAid.transform, spawn_points[i], Quaternion.identity);
                        }
                    }
                }
                checkpoints[index].SetActive(false);
                checkpoints[index + 1].SetActive(true);
                active_checkpoint = index + 1;
                index++;
            }
            else if (index == 13)
            {
                checkpoints[index].SetActive(false);
                checkpoints[0].SetActive(true);
                index = 0;
                active_checkpoint = 0;
                if (powerup.activeSelf)
                {
                    if (GameModes.ta == true)
                    {
                        for (int i = 0; i < spawn_points.Length; i++)
                        {
                            Instantiate(firstAid.transform, spawn_points[i], Quaternion.identity);
                        }
                    }
                    else
                    {
                    int index_PU = 0;
                        for (int i = 0; i < spawn_points.Length; i++)
                        {
                            index_PU = (int)Random.Range(0f, 4f);
                            Instantiate(power_ups[index_PU].transform, spawn_points[i], Quaternion.identity);
                        }
                    }
                }
                else if(warzone.activeSelf)
                {
                    int index_PU = 0;
                    for (int i = 0; i < spawn_points.Length; i++)
                    {
                        index_PU = (int)Random.Range(0f, 4f);
                        Instantiate(firstAid.transform, spawn_points[i], Quaternion.identity);
                    }
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update () {

        if (startTimer)
            time += Time.deltaTime;
	}
}
