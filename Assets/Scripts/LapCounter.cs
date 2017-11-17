using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour {
    public Transform path;
    private List<Transform> nodes;
    private int currentNode = 0;
    private int laps = 0;

    // Use this for initialization
    void Start () {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        CheckWaypointDistance();
	}

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
                laps++;
                print("lap: " + laps);
            }
            else
            {
                currentNode++;
                print("currentPlayerNode = " + currentNode);
            }
        }
    }
}
