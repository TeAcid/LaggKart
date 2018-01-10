using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccidentHandler : MonoBehaviour
{
    private int count = 0;
    private GameObject car;
    private LapCounter lc;

    // Use this for initialization
    void Start()
    {
        //transform.Rotate(Vector3.left, 180.0f); // Za test ...
        car = GameObject.Find("Car");
        lc = car.GetComponent<LapCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCarTilted();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Proga" && collision.gameObject.tag != "Enemy_Car")
        {
            count++;
        }
        else
        {
            count = 0;
        }

        if (count > 10)
        {
            transform.position = lc.nodes[lc.currentNode].position;
            transform.eulerAngles = new Vector3(0, 90, 0);
            count = 0;
        }
    }

    private void IsCarTilted()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0.5f)
        {
            //Debug.Log("I fell over :-(");
            transform.rotation = Quaternion.identity;
            transform.position = lc.nodes[lc.currentNode].position;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
