using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCrowd : MonoBehaviour {
    private GameObject car;
    private float speed = 2.0f;
    private float height = 5.0f;
    private Vector3 pos;
    private int numChildren = 20;
    private GameObject prefab;
    private List<int> coords;

    private const int MAX_X_OFFSET = 10;
    private const int MIN_X_OFFSET = -10;
    private const int MAX_Z_OFFSET = -10;
    private const int MIN_Z_OFFSET = 20;

    private const float MIN_HEIGHT = 3.0f;
    private const float MAX_HEIGHT = 6.0f;
    private const float MIN_SPEED = 2.0f;
    private const float MAX_SPEED = 3.0f;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        car = GameObject.Find("Car");
        coords = new List<int>();

        if (transform.tag == "Proga")
        {
            //Generiranje gledalcev
            for (int i = 0; i < numChildren; i++)
            {
                prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                prefab.transform.position = new Vector3(0, 0, 0);
                prefab.transform.localScale = new Vector3(2, 2, 2);
                //prefab.transform.Translate(Vector3.up * prefab.transform.localScale.y / 2);
                prefab.transform.parent = transform;

                int offsetX = Random.Range(MIN_X_OFFSET, MAX_X_OFFSET);
                int offsetZ = Random.Range(MIN_Z_OFFSET, MAX_Z_OFFSET);
                int index = coords.FindIndex(item => item == offsetX);

                while (coords.FindIndex(item => item == offsetX) != -1)
                    offsetX = Random.Range(MIN_X_OFFSET, MAX_X_OFFSET);

                while (coords.FindIndex(item => item == offsetZ) != -1)
                    offsetZ = Random.Range(MIN_Z_OFFSET, MAX_Z_OFFSET);

                //float width = prefab.transform.GetComponent<Renderer>().bounds.size.x * 0.04f;
                //prefab.transform.position = new Vector3(pos.x + offset, pos.y, pos.z);
                //Debug.Log(prefab.transform.lossyScale.y / 2);
                if(offsetX < 0)
                {
                    prefab.transform.position = new Vector3(pos.x - 12 + offsetX, pos.y + prefab.transform.lossyScale.y / 2, pos.z + offsetZ);
                }
                else
                {
                    prefab.transform.position = new Vector3(pos.x + 12 + offsetX, pos.y + prefab.transform.lossyScale.y / 2, pos.z + offsetZ);
                }
            }
            
            // Vsakemu otroku se dodeli AnimeCrowd component 
            foreach (Transform obj in transform)
            {
                obj.gameObject.AddComponent<AnimateCrowd>();
            }

            // Objektu Crowd se odstrani skripta 
            Destroy(transform.gameObject.GetComponent<AnimateCrowd>());
            return;
        }

        //transform.position += new Vector3(0, transform.localScale.y / 2, 0);
        height = Random.Range(MIN_HEIGHT, MAX_HEIGHT);
        speed = Random.Range(MIN_SPEED, MAX_SPEED);
        //Debug.Log("Child count: " + transform.childCount);
	}

    void Jump()
    {
        float newY = Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(pos.x, newY * height, pos.z);
        if (transform.position.y < pos.y)
        {
            transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, car.transform.position) < 60)
        {
            //Debug.Log("I am very close to you, my son.");
            Vector3 direction = (car.transform.position - transform.position).normalized;
            Quaternion look = Quaternion.LookRotation(direction);
            transform.rotation = look;

            transform.position = new Vector3(pos.x, pos.y, pos.z);
            Jump();
        }
        else
        {
            if (transform.position.y > pos.y)
            {
                Jump();
            }
        }
	}
}
