using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public event Action<GameObject> OnClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }

    public void Click()
    {
        Debug.Log("Subject Clicked");
        OnClicked?.Invoke(this.gameObject);
    }
}
