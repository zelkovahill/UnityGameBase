using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.School.Camera;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public EBuildingType buildingType;
    public string buildingName;
    public int requiredTree = 5;
    public float constructionTime = 2.0f;

    public bool canBuild = true;
    public bool isConstructed = false;

    private Material buildingMaterial;

    private void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;

        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float timer = 0;

        Color color = buildingMaterial.color;

        while (timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1.0f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }

        isConstructed = true;

        if (FloatingTextManager.instance is not null)
        {
            FloatingTextManager.instance.Show($"{buildingName} Constructed", transform.position + Vector3.up);
        }
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed)
        {
            return;
        }

        if (inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(EItemType.Tree, requiredTree);

            if (FloatingTextManager.instance is not null)
            {
                FloatingTextManager.instance.Show($"{buildingName} Construction Started", transform.position + Vector3.up);
            }

            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if (FloatingTextManager.instance is not null)
            {
                FloatingTextManager.instance.Show($"{inventory.treeCount} / {requiredTree}", transform.position + Vector3.up);
            }
        }
    }
}
