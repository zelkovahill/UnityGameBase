using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestCondition
{
    bool IsMet();
    void Initialize();
    float GetProgress();
    string GetDescription();
}