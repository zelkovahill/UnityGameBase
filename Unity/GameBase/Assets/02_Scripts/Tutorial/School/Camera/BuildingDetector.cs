using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.School.Camera;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPostion;
    public float moveThreshold = 0.1f;
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currentBuildingCrafter;

    private void Start()
    {
        lastPostion = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(lastPostion, transform.position) > moveThreshold)
        {
            lastPostion = transform.position;
            CheckForBuilding();
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        ConstructibleBuilding closestBuilding = null;
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();

            if (building is not null)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();
                }
            }
        }

        if (closestBuilding != currentNearbyBuilding)
        {
            currentNearbyBuilding = closestBuilding;
            currentBuildingCrafter = closesCrafter;

            if (currentBuildingCrafter is not null && !currentNearbyBuilding.isConstructed)
            {
                if (FloatingTextManager.instance is not null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] 키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
