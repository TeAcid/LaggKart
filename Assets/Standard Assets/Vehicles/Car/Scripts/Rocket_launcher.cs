using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_launcher : MonoBehaviour {

    public GameObject Rocket;
    public Transform rocket_spawn;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        Rocket = (GameObject)Resources.Load("Rocket");
	}

    void Fire()
    {
        rocket_spawn = Player.transform;
        rocket_spawn.Translate(0, 1.5f, 0);
        var rocket = (GameObject)Instantiate(Rocket, rocket_spawn.position, rocket_spawn.rotation);
        rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 50;
        Destroy(rocket, 5.0f);
    }
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Fire();
        }
	}
}
