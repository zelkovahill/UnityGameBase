using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class RecipeList
{
    public static CraftingRecipe[] KitchenRecipes = new CraftingRecipe[]
    {
        new CraftingRecipe
        {
            itemName = "Vegetable Stew",
            resultItem = EItemType.VegetableStew,
            resultAmount = 1,
            hungerRestoreAmount = 40.0f,
            requiredItems = new EItemType[] { EItemType.Plant, EItemType.Bush  },
            requiredAmounts = new int[] {2,1}
        },
        new CraftingRecipe
        {
            itemName = "Fruit Salad",
            resultItem = EItemType.FruitSalad,
            resultAmount = 1,
            hungerRestoreAmount = 60.0f,
            requiredItems = new EItemType[] { EItemType.Plant, EItemType.Bush },
            requiredAmounts = new int[] {3,3}
        },
    };

    public static CraftingRecipe[] WorkbenchRecipes = new CraftingRecipe[]
    {
        new CraftingRecipe
        {
            itemName = "Repair Kit",
            resultItem = EItemType.RepairKit,
            resultAmount = 1,
            repairAmount = 25.0f,
            requiredItems = new EItemType[] { EItemType.Crystal },
            requiredAmounts = new int[] {3}
        },
    };
}
