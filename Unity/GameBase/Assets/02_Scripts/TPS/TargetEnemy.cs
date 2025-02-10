using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetEnemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPBar;

    private float enemyMaxHP = 10;
    public float currentHP = 0;


    private void Start()
    {
        InitEnemyHP();
    }

    private void Update()
    {
        HPBar.value = currentHP / enemyMaxHP;
    }

    private void InitEnemyHP()
    {
        currentHP = enemyMaxHP;
    }
}
