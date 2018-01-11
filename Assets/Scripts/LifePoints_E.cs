using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints_E : MonoBehaviour {

    AIDriving ad;
    LifePoints_E lpE;

    public LifePoints_P lp_player;
    public GameObject enemy;
    public static float x;

    public GameObject Rocket;
    public Transform rocket_spawn;

    public GameObject player;

    private float time = 1.0f;
    public static bool move_enemy = false;

    // Use this for initialization
    void Start ()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        Rocket = (GameObject)Resources.Load("RocketE");
        x = LifePoints_P.lifepoints_enemy;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "rocket")
        {
            //Debug.Log(x);
            x -= 80;
        }
    }

    private void Calculate_angle ()
    {
        float angle = Vector3.Angle(player.transform.position, enemy.transform.forward);
        Debug.Log(angle);
        if(angle >= 22.5f && angle <= 26.5f)
        {
            rocket_spawn = enemy.transform;
            var rocket = (GameObject)Instantiate(Rocket, rocket_spawn.position, rocket_spawn.rotation);
            rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 50;
            Destroy(rocket, 5.0f);
        }
    }

    // Update is called once per frame
    void Update () {
        time = time - Time.deltaTime;
        if (time < 0.0f && GameModes.wz == true)
        {
            Calculate_angle();
            time = 1.0f;
        }
        if (x < 1 && GameModes.wz == true)
        {
            LifePoints_P.gamepoints += 100;
            Debug.Log("Gamepoints = " + LifePoints_P.gamepoints);
            //Destroy(enemy);
            move_enemy = true;
            x = 100;
        }
    }
}
