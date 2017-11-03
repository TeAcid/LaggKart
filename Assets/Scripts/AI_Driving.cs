using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Driving : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 40f;
    private List<Transform> nodes;
    private int currentNode = 0;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    // Use this for initialization
    private void Start () {
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
	private void FixedUpdate () {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
	}

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        wheelFL.motorTorque = 60000f;
        wheelFR.motorTorque = 60000f;
    }

    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 4f)
        {
            if(currentNode == nodes.Count - 1)
            {
                currentNode = 0;
                print("current = 0");
            }
            else
            {
                currentNode++;
                print("current = " + currentNode);
            }
        }
    }
}
