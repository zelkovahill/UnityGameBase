using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> allQuests = new Dictionary<string, Quest>();
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    private Dictionary<string, Quest> completedQuests = new Dictionary<string, Quest>();

    public event Action<Quest> OnQuestStarted;
    public event Action<Quest> OnQuestCompleted;
    public event Action<Quest> OnQuestFailed;

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
    }
}
