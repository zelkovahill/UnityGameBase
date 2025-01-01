using System.Collections;
using System.Collections.Generic;
using Tutorial.School.Camera;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public EBuildingType buildingType;
    public CraftingRecipe[] Recipes;
    private SurvivalStats survivalStats;
    private ConstructibleBuilding building;

    private void Start()
    {
        survivalStats = FindObjectOfType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)
        {
            case EBuildingType.Kitchen:
                Recipes = RecipeList.KitchenRecipes;
                break;

            case EBuildingType.CraftingTable:
                Recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }

    public void TryCraft(CraftingRecipe recipe, PlayerInventory inventory)
    {
        if (!building.isConstructed)
        {
            FloatingTextManager.instance?.Show("Building is not constructed", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            if (inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextManager.instance?.Show("Not enough materials", transform.position + Vector3.up);
                return;
            }
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            inventory.RemoveItem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
        }

        survivalStats.DamageCrafting();

        inventory.AddItem(recipe.resultItem);
        FloatingTextManager.instance?.Show($"Crafted {recipe.resultItem}", transform.position + Vector3.up);
    }
}
