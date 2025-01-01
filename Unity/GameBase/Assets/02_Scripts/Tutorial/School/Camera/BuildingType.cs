using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBuildingType
{
    CraftingTable,
    Funace,
    Kitchen,
    Storage,
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;
    public EItemType resultItem;
    public int resultAmount = 1;

    public float hungerRestoreAmount;
    public float repairAmount;

    public EItemType[] requiredItems;
    public int[] requiredAmounts;
}
