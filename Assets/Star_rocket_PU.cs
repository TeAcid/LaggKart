using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_rocket_PU : MonoBehaviour {

    public GameObject Enemy;
	// Use this for initialization
	void Start () {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate((Enemy.transform.position - transform.position).normalized * 50 * Time.deltaTime);
        Destroy(this, 10);
    }
}
