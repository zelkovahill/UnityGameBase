using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private int enemyMaxHP = 100;
    public int currentHP = 0;


    private void Start()
    {
        InitEnemyHP();
    }

    private void InitEnemyHP()
    {
        currentHP = enemyMaxHP;
    }
}
