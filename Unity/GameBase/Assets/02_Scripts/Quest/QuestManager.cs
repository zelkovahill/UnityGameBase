using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private Dictionary<string, Quest> allQuests = new Dictionary<string, Quest>();
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    private Dictionary<string, Quest> completedQuests = new Dictionary<string, Quest>();

    public event Action<Quest> OnQuestStarted;
    public event Action<Quest> OnQuestCompleted;
    public event Action<Quest> OnQuestFailed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeQuest();
    }

    private void InitializeQuest()
    {
        var ratHuntQuest = new Quest(
            "Q001",
            "Rat Problem",
            "Clear the basement of rate",
            QuestType.Kill,
            1
        );

        ratHuntQuest.AddCondition(new KillQuestCondition("Rat", 5));
        ratHuntQuest.AddReward(new ExperienceReward(100));
        ratHuntQuest.AddReward(new ItemReward("Gold", 50));

        var herbQuest = new Quest(
                "Q002",
                "Herb Collection",
                "Collect herbs for the healer",
                QuestType.Collection,
                1
        );

        herbQuest.AddCondition(new CollectionQuestCondition("Herb", 3));
        herbQuest.AddReward(new ExperienceReward(50));

        allQuests.Add(ratHuntQuest.Id, ratHuntQuest);
        allQuests.Add(herbQuest.Id, herbQuest);

        StartQuest("Q001");
        StartQuest("Q002");

    }

    public bool CanStartQuest(string questId)
    {
        if (!allQuests.TryGetValue(questId, out Quest quest))
        {
            return false;
        }

        if (activeQuests.ContainsKey(questId))
        {
            return false;
        }

        if (completedQuests.ContainsKey(questId))
        {
            return false;
        }

        foreach (var perrequisite in quest.GetType().GetField("prerequisiteQuestIds")?.GetValue(quest) as List<string> ?? new List<string>())
        {
            if (!completedQuests.ContainsKey(perrequisite))
            {
                return false;
            }
        }

        return true;
    }

    public void StartQuest(string questId)
    {
        if (!CanStartQuest(questId))
        {
            return;
        }

        var quest = allQuests[questId];
        quest.Start();
        activeQuests.Add(questId, quest);
        OnQuestStarted?.Invoke(quest);
    }

    public void UpdateQuestProgress(string questId)
    {
        if (!activeQuests.TryGetValue(questId, out Quest quest))
        {
            return;
        }

        if (quest.CheckCompletion())
        {
            CompleteQuest(questId);
        }
    }

    private void CompleteQuest(string questId)
    {
        if (!activeQuests.TryGetValue(questId, out Quest quest))
        {
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        quest.Complete(player);

        activeQuests.Remove(questId);
        completedQuests.Add(questId, quest);
        OnQuestCompleted?.Invoke(quest);

        Debug.Log($"Quest {quest.Title} completed");
    }

    public List<Quest> GetAcailableQuests()
    {
        return allQuests.Values.Where(q => CanStartQuest(q.Id)).ToList();
    }

    public List<Quest> GetActiveQuest()
    {
        return activeQuests.Values.ToList();
    }

    public List<Quest> GetCompletedQuest()
    {
        return completedQuests.Values.ToList();
    }

    public void OnEnemyKilled(string enemyType)
    {
        var activeQuestList = activeQuests.Values.ToList();

        foreach (var quest in activeQuestList)
        {
            foreach (var condition in quest.GetConditions())
            {
                if (condition is KillQuestCondition killCondition)
                {
                    killCondition.EnemyKilled(enemyType);
                    UpdateQuestProgress(quest.Id);
                }
            }
        }
    }

    public void OnItemCollected(string itemId)
    {
        var activeQuestList = activeQuests.Values.ToList();

        foreach (var quest in activeQuestList)
        {
            foreach (var condition in quest.GetConditions())
            {
                if (condition is CollectionQuestCondition collectionCondition)
                {
                    collectionCondition.ItemCollected(itemId);
                    UpdateQuestProgress(quest.Id);
                }
            }
        }
    }
}