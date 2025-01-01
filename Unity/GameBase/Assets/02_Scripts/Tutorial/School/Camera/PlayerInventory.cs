using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;

    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public int vegetableStewCount = 0;
    public int fruitSaledCount = 0;
    public int repairKitCount = 0;

    private void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(EItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)
        {
            return;
        }
    }

    public void AddItem(EItemType itemType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    public void AddItem(EItemType itemType)
    {
        switch (itemType)
        {
            case EItemType.Crystal:
                crystalCount++;
                Debug.Log("Crystal: " + crystalCount);
                break;

            case EItemType.Plant:
                plantCount++;
                Debug.Log("Plant: " + plantCount);
                break;

            case EItemType.Bush:
                bushCount++;
                Debug.Log("Bush: " + bushCount);
                break;

            case EItemType.Tree:
                treeCount++;
                Debug.Log("Tree: " + treeCount);
                break;

            case EItemType.VegetableStew:
                vegetableStewCount++;
                Debug.Log("Vegetable Stew: " + vegetableStewCount);
                break;

            case EItemType.FruitSalad:
                fruitSaledCount++;
                Debug.Log("Fruit Salad: " + fruitSaledCount);
                break;

            case EItemType.RepairKit:
                repairKitCount++;
                Debug.Log("Repair Kit: " + repairKitCount);
                break;
        }
    }

    public bool RemoveItem(EItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case EItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($" Amount : {amount} Crystal: " + crystalCount);
                    return true;
                }
                break;

            case EItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($" Amount : {amount} Plant: " + plantCount);
                    return true;
                }
                break;

            case EItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;
                    Debug.Log($" Amount : {amount} Bush: " + bushCount);
                    return true;
                }
                break;

            case EItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;
                    Debug.Log($" Amount : {amount} Tree: " + treeCount);
                    return true;
                }
                break;

            case EItemType.VegetableStew:
                if (vegetableStewCount >= amount)
                {
                    vegetableStewCount -= amount;
                    Debug.Log($" Amount : {amount} Vegetable Stew: " + vegetableStewCount);
                    return true;
                }
                break;

            case EItemType.FruitSalad:
                if (fruitSaledCount >= amount)
                {
                    fruitSaledCount -= amount;
                    Debug.Log($" Amount : {amount} Fruit Salad: " + fruitSaledCount);
                    return true;
                }
                break;

            case EItemType.RepairKit:
                if (repairKitCount >= amount)
                {
                    repairKitCount -= amount;
                    Debug.Log($" Amount : {amount} Repair Kit: " + repairKitCount);
                    return true;
                }
                break;
        }
        return false;
    }

    public int GetItemCount(EItemType itemType)
    {
        switch (itemType)
        {
            case EItemType.Crystal:
                return crystalCount;
            case EItemType.Plant:
                return plantCount;
            case EItemType.Bush:
                return bushCount;
            case EItemType.Tree:
                return treeCount;

            case EItemType.VegetableStew:
                return vegetableStewCount;
            case EItemType.FruitSalad:
                return fruitSaledCount;
            case EItemType.RepairKit:
                return repairKitCount;

            default:
                return 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("Crystal: " + crystalCount);
        Debug.Log("Plant: " + plantCount);
        Debug.Log("Bush: " + bushCount);
        Debug.Log("Tree: " + treeCount);

        Debug.Log("Vegetable Stew: " + vegetableStewCount);
        Debug.Log("Fruit Salad: " + fruitSaledCount);
        Debug.Log("Repair Kit: " + repairKitCount);
    }
}
