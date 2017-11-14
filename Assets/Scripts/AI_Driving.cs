using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Driving : MonoBehaviour {
    [Header("AI Driving")]
    public Transform path;
    public float maxSteerAngle = 40f;
    private List<Transform> nodes;
    private int currentNode = 0;
    [Header("Wheel Colliders")]
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    [Header("Driving and breaking")]
    public float maxMotorTorque = 60000f;
    public float currentSpeed;
    public float maxSpeed = 220f;
    public float maxBreakTorque = 120000f;
    public Vector3 centerOfMass;
    public bool isBreaking = false;
    [Header("Sensors")]
    public float sensorLength = 5f;


    // Use this for initialization
    private void Start () {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

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
        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
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
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if(currentSpeed < maxSpeed && !isBreaking) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void Braking()
    {
        if (isBreaking)
        {
            wheelRL.brakeTorque = maxBreakTorque;
            wheelRR.brakeTorque = maxBreakTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
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

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        //sensorStartPos.z += 0.5f;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red);
        }
        //Debug.DrawLine(sensorStartPos, transform.forward, Color.blue);
        print("sensorStartPos:" + sensorStartPos);
        //Debug.DrawRay(sensorStartPos, hit.point);
    }
}
