using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_collision : MonoBehaviour {
	
	public GameObject Explosion;

    public bool start_timer = false;
    float rotation_time = 0.099f;
    float remaining_time = 0.099f;
    float explosion = 40.0f;
    public bool nailz = false;
    public bool roket = false;
    public bool shield = false;

    public LifePoints_E lp_enemy;
    public LifePoints_P lp_player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "nails" || other.gameObject.tag == "PU_fake")
        {

            Debug.Log("USTAŠ");
            Destroy(other.gameObject);
            nailz = true;
            start_timer = true;
        }
        else if (other.gameObject.tag == "rocket" || other.gameObject.tag == "Star_rocket")
        {
            var exp = (GameObject)Instantiate(Explosion, this.transform.position, this.transform.rotation);
            Destroy(other.gameObject);
            Destroy(exp, 1f);
            roket = true;
            start_timer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "nails" || collision.gameObject.tag == "PU_fake")
        {
            Debug.Log("USTAŠ");
            Destroy(collision.gameObject);
            nailz = true;
            start_timer = true;
        }
        else if (collision.gameObject.tag == "FirstAid")
        {
            if (this.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                LifePoints_P.lifepoints += 15;
            }
            else if (this.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                LifePoints_P.lifepoints_enemy += 15;
            }
        }
    }
        // Use this for initialization
        void Start () {
        lp_player = GetComponent<LifePoints_P>();
        lp_enemy = GetComponent<LifePoints_E>();
    }
	
	// Update is called once per frame
	void Update () {
        Behaviour halo = (Behaviour)gameObject.GetComponent("Halo");
        if (halo.enabled == enabled)
            shield = true;

        if (start_timer && GameModes.wz == false)
        {
            remaining_time -= 0.01f;
            if (nailz == true)
            {
                if (shield == true)
                {
                    halo.enabled = !halo.enabled;
                    shield = false;
                    nailz = false;
                }
                else
                {
                    Debug.Log("rt = " + remaining_time);
                    if (this.gameObject.tag == "Enemy" || this.gameObject.tag == "Player")
                    {
                        if (remaining_time > 0.0f)
                        {
                            this.transform.Rotate(0, Time.deltaTime * 500, 0);
                        }
                        else
                        {
                            start_timer = false;
                            nailz = false;
                            this.transform.Rotate(0, 0, 0);
                        }
                    }
                }
            }
            else if (roket == true)
            {
                if (shield == true)
                {
                    halo.enabled = !halo.enabled;
                    shield = false;
                    roket = false;
                }
                else
                {
                    remaining_time -= 0.01f;
                    if (remaining_time > 0.0f)
                    {
                        Quaternion Jump = Quaternion.Euler(explosion, Time.deltaTime * 500, 0);
                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Jump, 10);
                    }
                    else
                    {
                        start_timer = false;
                        roket = false;
                        this.transform.Rotate(0, 0, 0);
                    }
                }
            }
        }
	}
}
