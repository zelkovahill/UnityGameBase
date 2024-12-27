using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceReward : IQuestReward
{
    private int experienceAmount;

    public ExperienceReward(int amount)
    {
        this.experienceAmount = amount;
    }

    public void Grant(GameObject player)
    {
        Debug.Log($"Granted {experienceAmount} experience");
    }

    public string GetDescription() => $"{experienceAmount} Experience Points";
}
