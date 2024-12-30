using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicTrajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 10;
    public float timeStep = 0.1f;

    public Transform launchPoint;
    public float myRoation;
    public float launchPower;
    public float launchAngle;
    public float launchDirection;
    public float gravity = -9.8f;
    public GameObject projecttilePrefabs;

    private void Update()
    {
        RenderTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile(projecttilePrefabs);
        }
    }

    private void RenderTrajectory()
    {
        lineRenderer.positionCount = resolution;
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePositionAtTime(t);
        }

        lineRenderer.SetPositions(points);
    }

    private Vector3 CalculatePositionAtTime(float time)
    {
        float launchAngleRand = Mathf.Deg2Rad * launchAngle;
        float launchDirectionRand = Mathf.Deg2Rad * launchDirection;

        float x = launchPower * time * Mathf.Cos(launchAngleRand) * Mathf.Cos(launchDirectionRand);
        float z = launchPower * time * Mathf.Cos(launchAngleRand) * Mathf.Sin(launchDirectionRand);
        float y = launchPower * time * Mathf.Sin(launchAngleRand) + 0.5f * gravity * time * time;

        return launchPoint.position + new Vector3(x, y, z);
    }

    public void LaunchProjectile(GameObject _object)
    {
        GameObject temp = Instantiate(_object);

        temp.transform.position = launchPoint.position;
        temp.transform.rotation = launchPoint.rotation;

        Rigidbody rb = temp.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = temp.AddComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.isKinematic = false;

            float launchAngleRand = Mathf.Deg2Rad * launchAngle;
            float launchDirectionRand = Mathf.Deg2Rad * launchDirection;

            float initalVelocityX = launchPower * Mathf.Cos(launchAngleRand) * Mathf.Cos(launchDirectionRand);
            float initalVelocityZ = launchPower * Mathf.Cos(launchAngleRand) * Mathf.Sin(launchDirectionRand);
            float initalVelocityY = launchPower * Mathf.Sin(launchAngleRand);

            Vector3 initialVelocity = new Vector3(initalVelocityX, initalVelocityY, initalVelocityZ);

            rb.velocity = initialVelocity;
        }
    }
}
