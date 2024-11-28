using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("몬스터 HP")]
    private int Monster_HP = 5;

    public void Damaged(int damage)
    {
        Monster_HP -= damage;

        if (Monster_HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
