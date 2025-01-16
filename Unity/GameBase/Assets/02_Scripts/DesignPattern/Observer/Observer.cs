using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Subject subject;

    public void OnEnable()
    {
        subject = GetComponent<Subject>();
        subject.OnClicked += OnSubjectClicked;
    }

    private void OnDisable()
    {
        subject.OnClicked -= OnSubjectClicked;
    }

    public void OnSubjectClicked(GameObject clickedObject)
    {
        Debug.Log("Observer OnSubjectClicked");
    }
}
