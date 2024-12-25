using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompassMarker : MonoBehaviour
{
    public Image MainImage;
    public CanvasGroup CanvasGroup;
    public Color DefaultColor;
    public Color AltColor;

    [Header("Direction element")]
    public bool IsDirection;
    public TextMeshProUGUI TextContent;

    // EnemyController m_EnemyController;

    public void Initialize(CompassElement compassElement, string textDirection)
    {
        if (IsDirection && TextContent)
        {
            TextContent.text = textDirection;
        }
        else
        {
            // m_EnemyController = compassElement.transform.GetComponent<EnemyController>();

            // if (m_EnemyController)
            // {
            //     m_EnemyController.onDetectedTarget += DetectTarget;
            //     m_EnemyController.onLostTarget += LostTarget;

            // LostTarget();
            // }
        }
    }

    public void DetectTarget()
    {
        MainImage.color = AltColor;
    }

    public void LostTarget()
    {
        MainImage.color = DefaultColor;
    }
}
