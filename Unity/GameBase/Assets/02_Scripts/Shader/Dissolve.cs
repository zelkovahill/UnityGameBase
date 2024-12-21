using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material material;
    public float amount = -1f;
    public bool isDissolving = false;

    private void Start()
    {
        isDissolving = true;
        amount = -1f;
        material.SetFloat("_Amount", amount);
    }

    private void Update()
    {
        if (isDissolving)
        {
            amount += Time.deltaTime;
            material.SetFloat("_Amount", amount);
            if (amount >= 1f)
            {
                isDissolving = false;
            }
        }
    }
}
