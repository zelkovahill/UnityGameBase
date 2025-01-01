using System.Collections;
using System.Collections.Generic;
using Tutorial.School.Camera;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public EItemType itemType;
    public string itemName;
    public float respawnTime = 30.0f;
    public bool canCollect = true;

    public void CollectItem(PlayerInventory inventory)
    {
        if (!canCollect)
        {
            return;
        }

        inventory.AddItem(itemType);

        if (FloatingTextManager.instance is not null)
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;
            FloatingTextManager.instance.Show($" + {itemName}", textPosition);
        }

        Debug.Log($"{itemName} collected");
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        canCollect = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;
    }
}
