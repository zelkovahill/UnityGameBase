using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,
    VegetableStew,
    FruitSalad,
    RepairKit,
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPosition;
    public float moveThreshold = 0.1f;
    public CollectibleItem currentNearbyItem;

    private void Start()
    {
        lastPosition = transform.position;

    }

    private void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForItems();
            lastPosition = transform.position;
        }

        if (currentNearbyItem is not null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());
        }
    }

    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        CollectibleItem closestItem = null;

        foreach (Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();

            if (item is not null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;

            if (currentNearbyItem is not null)
            {
                Debug.Log($"[E] Nearby item: {currentNearbyItem.itemName}");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
