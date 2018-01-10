using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public GameObject enemy; //ko jih bo več terba polje
    public GameObject shield;
    public GameObject nails;
    public GameObject fake;
    public GameObject rocket;
    public GameObject star_rocket;
    bool isActive_shield = false;
    bool isActive_star = false;
    bool isActive_nails = false;
    bool isActive_rocket = false;
    bool isActive_fake = false;

    float timeleft = 10;
    int starCounter = 0;

    

    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        shield = (GameObject)Resources.Load("Shield");
        nails = (GameObject)Resources.Load("Nails");
        fake = (GameObject)Resources.Load("PU_fake");
        rocket = (GameObject)Resources.Load("Rocket");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        star_rocket = (GameObject)Resources.Load("Star_Rocket");
    }

    private void Shield_Active()
    {
        Debug.Log("ŠILD EKTIV");
        Behaviour halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = enabled;
        if(timeleft == 0)
        {
            halo.enabled = enabled;
            timeleft = 10;
            return;
        }
    }

    private void Nails_Active()
    {
        Debug.Log("Sem unity in sem prizadet");
        Vector3 spawn_point = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z);
        Debug.Log("NEJLZ EKTIV");
        Instantiate(nails, spawn_point, player.transform.rotation);
        isActive_nails = false;

    }

    private void Rocket_Active()
    {
        Vector3 spawn_point = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z);

        Debug.Log("ROKET EKTIV");
        var r = (GameObject)Instantiate(rocket, spawn_point, player.transform.rotation);
        r.GetComponent<Rigidbody>().velocity = player.transform.forward  * 50;
    }

    private void Fake_Active()
    {
        Debug.Log("FEJK EKTIV");
        Vector3 spawn_point = new Vector3(player.transform.position.x, 3f, player.transform.position.z);
        Instantiate(fake, spawn_point, player.transform.rotation);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PU_shield")
        {
            Destroy(collision.gameObject);
            Shield_Active();
        }
        else if (collision.gameObject.tag == "PU_star")
        {
            Destroy(collision.gameObject);
            timeleft = 20;
            starCounter++;
            isActive_star = true;
        }
        else if (collision.gameObject.tag == "PU_fake")
        {
            Destroy(collision.gameObject);
            isActive_fake = true;
        }
        else if (collision.gameObject.tag == "PU_nails")
        { 
            Destroy(collision.gameObject);
            isActive_nails = true;
        }
        else if (collision.gameObject.tag == "PU_rocket")
        {
            Destroy(collision.gameObject);
            isActive_rocket = true;    
        }
        else if (collision.gameObject.tag == "FirsAid")
        {
            if ((LifePoints_P.lifepoints + 20) > 100)
                LifePoints_P.lifepoints = 100;
            else
                LifePoints_P.lifepoints += 20;
        }
    }
    // Update is called once per frame
    void Update () {
        {
            timeleft--;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("USTAŠ");
                if(isActive_fake == true)
                {
                    Debug.Log("USTAŠ FEJK");
                    Fake_Active();
                    isActive_fake = false;
                }
                else if(isActive_nails == true)
                {
                    Debug.Log("USTAŠ CVEKI");
                    Nails_Active();
                    isActive_nails = false;
                }
                else if(isActive_rocket == true)
                {
                    Debug.Log("USTAŠ RAKETA");
                    Rocket_Active();
                    isActive_rocket = false;
                }
                else if (isActive_star == true)
                {
                    Debug.Log("USTAŠ STAR");
                    Vector3 spawn_point = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z);

                    Debug.Log("ROKET STAR EKTIV");
                    var r = (GameObject)Instantiate(star_rocket, spawn_point, player.transform.rotation);
                }
            }
        }
    }
}
