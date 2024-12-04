using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class SlowDownTower : MonoBehaviour
    {
        private Tower thisTower;

        private void Start()
        {
            thisTower = GetComponent<Tower>();
        }

        private void Update()
        {
            // 적 리스트가 갱신 되었을 때
            if (thisTower.enemiesUpdate)
            {
                if (thisTower.enemiesInRange.Count > 0)
                {
                    foreach (EnemyController enemy in thisTower.enemiesInRange)
                    {
                        enemy.SetMode(thisTower.fireRate);  // 슬로우 적용 
                    }
                }
            }
        }
    }
}