using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임을 시작하면 특정 위치에 몬스터를 스폰
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    private const int COUNT_TO_SPAWN = 5;
    private readonly Vector3 SpawnOriginPosition = new Vector3(-2, 0, 0);
    private readonly Vector3 SpawnDistance = new Vector3(0, 1, 0);

    // 유니티 에디터 상에서 보이는 헤더
    [Tooltip("몬스터 프리팹")]
    // 유니티 에디터 상에 보이도록 혹은 저장 등을 위해 직렬화할 때 사용
    [SerializeField]
    private GameObject _monster;

    // 오브젝트가 최초로 깨어났을 때 호출
    private void Start()
    {
        for (int i = 0; i < COUNT_TO_SPAWN; i++)
        {
            // 최초 스폰 위치부터 미리 정해둔 간격만큼 점점 멀어진다.
            var position = SpawnOriginPosition + SpawnDistance * i;

            // 몬스터를 position에 Quaternion.identity(회전 없음)각도로 스폰
            var monsterObject = Instantiate(_monster, position, Quaternion.identity);

            // 소환된 몬스터의 이름을 지정
            monsterObject.name = $"Monster_{i}";
        }
    }
}
