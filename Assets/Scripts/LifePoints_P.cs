using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints_P : MonoBehaviour {

    public static int lifepoints = 100;
    public static int gamepoints = 0;
    public static int lifepoints_enemy = 100;
    GameObject enemy;
    int tmp = 0;
    public GameObject Explosion;

    Player_checkpoints chp;

	// Use this for initialization
	void Start () {
        //lifepoints = 100;
        //lifepoints_enemy = 100;
        //gamepoints = 0;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "rocketE")
        {
            //var exp = (GameObject)Instantiate(Explosion, this.transform.position, this.transform.rotation);
            Destroy(other.gameObject);
            lifepoints -= 15;
           //Destroy(exp, 1f);
        }
    }

    // Update is called once per frame
    void Update () {
		if(lifepoints < 1)
        {
            Destroy(this);
        }
        if(gamepoints != tmp)
        {
            int index = Player_checkpoints.active_checkpoint;
            Vector3 x = new Vector3(Player_checkpoints.checkpoints[index].transform.position.x, 2.1f, Player_checkpoints.checkpoints[index].transform.position.z);
            Instantiate(enemy, x, Player_checkpoints.checkpoints[index].transform.rotation);
            tmp = gamepoints;
        }
	}
}
