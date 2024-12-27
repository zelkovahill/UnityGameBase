using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public QuestType Type { get; set; }
    public QuestStatus Status { get; set; }
    public int Level { get; set; }

    private List<IQuestCondition> conditions = new List<IQuestCondition>();
    private List<IQuestReward> rewards;
    private List<string> prerequisiteQuestIds;

    public Quest(string id, string title, string description, QuestType type, int level)
    {
        Id = id;
        Title = title;
        Description = description;
        Type = type;
        Status = QuestStatus.NotStarted;
        Level = level;

        this.conditions = new List<IQuestCondition>();
        this.rewards = new List<IQuestReward>();
        this.prerequisiteQuestIds = new List<string>();
    }

    public List<IQuestCondition> GetConditions()
    {
        return conditions;
    }

    public void AddCondition(IQuestCondition condition)
    {
        conditions.Add(condition);
    }

    public void AddReward(IQuestReward reward)
    {
        rewards.Add(reward);
    }

    public void Start()
    {
        if (Status == QuestStatus.NotStarted)
        {
            Status = QuestStatus.InProgress;

            foreach (var conditions in conditions)
            {
                conditions.Initialize();
            }
        }
    }

    public bool CheckCompletion()
    {
        if (Status != QuestStatus.InProgress)
        {
            return false;
        }

        return conditions.All(c => c.IsMet());
    }

    public void Complete(GameObject player)
    {
        if (Status != QuestStatus.InProgress)
        {
            return;
        }

        if (!CheckCompletion())
        {
            return;
        }

        foreach (var reward in rewards)
        {
            reward.Grant(player);
        }

        Status = QuestStatus.Comleted;
    }

    public float GetProgress()
    {
        if (conditions.Count == 0)
        {
            return 0;
        }

        return conditions.Average(c => c.GetProgress());
    }

    public List<string> GetConditionDescriptions()
    {
        return conditions.Select(c => c.GetDescription()).ToList();
    }

    public List<string> GetRewardDescriptions()
    {
        return rewards.Select(r => r.GetDescription()).ToList();
    }
}
