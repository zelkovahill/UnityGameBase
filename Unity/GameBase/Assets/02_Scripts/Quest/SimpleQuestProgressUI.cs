using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleQuestProgressUI : MonoBehaviour
{
    [Header("Quest List")]
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questPrefabs;

    [Header("Pregress Test")]
    [SerializeField] private Button killEnemyButton;

    [SerializeField] private QuestManager questManager;

    private void Start()
    {
        questManager = QuestManager.Instance;


    }

    private void CreateQuestUI(Quest quest)
    {
        GameObject questObj = Instantiate(questPrefabs, questListParent);

        TextMeshProUGUI titleText = questObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI progressText = questObj.transform.Find("ProgressText").GetComponent<TextMeshProUGUI>();

        titleText.text = quest.Title;
        progressText.text = $"Progress : {quest.GetProgress():P0}";
    }

    private void UpdateQuestUI(Quest quest)
    {
        RefreshQuestList();
    }

    private void RefreshQuestList()
    {
        foreach (Transform child in questListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in questManager.GetActiveQuest())
        {
            CreateQuestUI(quest);
        }
    }

    private void OnKillEnemy()
    {
        questManager.OnEnemyKilled("Rat");
        RefreshQuestList();
    }

    private void OnCollectItem()
    {
        questManager.OnItemCollected("Herb");
        RefreshQuestList();
    }
}
