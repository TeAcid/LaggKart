using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Driving : MonoBehaviour {
    [Header("AI Driving")]
    public Transform easyPath;
    public Transform mediumPath;
    public Transform hardPath;
    private Transform path;
    public float maxSteerAngle = 40f;
    public int Difficulty;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float turnSpeed = 5;
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
    public float sensorLength = 10f;
    public float frontSideSensorPosition = 2f;
    public float frontSensorAngle = 30f;
    private bool avoiding = false;
    private float targetSteerAngle = 0;

    // Use this for initialization
    private void Start () {
        switch (Difficulty)
        {
            case 0:
                path = easyPath;
                break;
            case 1:
                path = mediumPath;
                break;
            case 2:
                path = hardPath;
                break;
            default:
                path = mediumPath;
                break;
        }

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
        if (Difficulty == 2)
        {
            Sensors();
        }
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        LerpToSteerAngle();
	}

    private void ApplySteer()
    {
        if (avoiding) return;

        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if(currentSpeed < maxSpeed && !isBreaking) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
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
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
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
        float avoidMultiplier = 0;
        avoiding = false;

        ///*********************RAYS*********************
        // front center
        Vector3 sensorOriginPosFC = transform.position + transform.forward * 5.4f;
        Ray rayFC = new Ray
        {
            origin = sensorOriginPosFC
        };
        rayFC.direction = transform.forward;
        // front right
        Vector3 sensorOriginPosFR;
        sensorOriginPosFR.x = sensorOriginPosFC.x + frontSideSensorPosition;
        sensorOriginPosFR.y = sensorOriginPosFC.y;
        sensorOriginPosFR.z = sensorOriginPosFC.z;
        Ray rayFR = new Ray
        {
            origin = sensorOriginPosFR
        };
        rayFR.direction = transform.forward;
        // front left
        Vector3 sensorOriginPosFL;
        sensorOriginPosFL.x = sensorOriginPosFC.x - frontSideSensorPosition;
        sensorOriginPosFL.y = sensorOriginPosFC.y;
        sensorOriginPosFL.z = sensorOriginPosFC.z;
        Ray rayFL = new Ray
        {
            origin = sensorOriginPosFL
        };
        rayFL.direction = transform.forward;
        // front right angle
        Ray rayFRA = new Ray
        {
            origin = sensorOriginPosFR
        };
        rayFRA.direction = Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward;
        // front left angle
        Ray rayFLA = new Ray
        {
            origin = sensorOriginPosFL
        };
        rayFLA.direction = Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward;

        //*********************SENSORS*********************
        // front right
        if (Physics.Raycast(rayFR, out hit, sensorLength))
        {
            if (hit.transform.tag != "Terrain" && hit.transform.tag != "Proga")
            {
                Debug.DrawLine(rayFR.origin, hit.point, Color.red);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        // front right angle
        else if (Physics.Raycast(rayFRA, out hit, sensorLength))
        {
            if (hit.transform.tag != "Terrain" && hit.transform.tag != "Proga")
            {
                Debug.DrawLine(rayFRA.origin, hit.point, Color.red);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }
        // front left
        if (Physics.Raycast(rayFL, out hit, sensorLength))
        {
            if (hit.transform.tag != "Terrain" && hit.transform.tag != "Proga")
            {
                Debug.DrawLine(rayFL.origin, hit.point, Color.red);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }
        // front left angle
        else if (Physics.Raycast(rayFLA, out hit, sensorLength))
        {
            if (hit.transform.tag != "Terrain" && hit.transform.tag != "Proga")
            {
                Debug.DrawLine(rayFLA.origin, hit.point, Color.red);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }
        // front center
        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(rayFC, out hit, sensorLength))
            {
                if (hit.transform.tag != "Terrain" && hit.transform.tag != "Proga")
                {
                    Debug.DrawLine(rayFC.origin, hit.point, Color.red);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                }
            }
        }

        if (avoiding)
        {
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
        }
    }

    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
