using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuestCondition : IQuestCondition
{
    private string enemyType;
    private int requiredKills;
    private int currentKills;

    public KillQuestCondition(string enemyType, int requiredKills)
    {
        this.enemyType = enemyType;
        this.requiredKills = requiredKills;
        this.currentKills = 0;
    }

    public bool IsMet() => currentKills >= requiredKills;
    public void Initialize() => currentKills = 0;
    public float GetProgress() => (float)currentKills / requiredKills;
    public string GetDescription() => $"Defeat {requiredKills} {enemyType} ({currentKills}/{requiredKills})";

    public void EnemyKilled(string enemyType)
    {
        if (this.enemyType == enemyType)
        {
            currentKills++;
        }
    }
}
