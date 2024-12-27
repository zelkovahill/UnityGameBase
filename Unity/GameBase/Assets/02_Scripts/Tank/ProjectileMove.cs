using System.Collections;
using System.Collections.Generic;
using Tank;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ProjectileMove : MonoBehaviour
{
    public enum EProjectileType
    {
        Player,     // 플레이어
        Enemy,      // 적
    }

    [Tooltip("발사 방향")]
    public Vector3 launchDirection;

    [SerializeField]
    [Tooltip("발사체 타입")]
    public EProjectileType projectileType = EProjectileType.Player;


    private void FixedUpdate()
    {
        float moveAmount = 3 * Time.fixedDeltaTime;

        transform.Translate(launchDirection * moveAmount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 벽이라면 발사체를 파괴
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

        // 충돌한 오브젝트가 몬스터라면 몬스터의 체력을 1 감소시키고 발사체를 파괴
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<MonsterController>().Damaged(1);
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 벽이라면 발사체를 파괴
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

        // 충돌한 오브젝트가 플레이어라면 플레이어의 체력을 1 감소시키고 발사체를 파괴
        if (other.gameObject.CompareTag("Player") && projectileType == EProjectileType.Player)
        {
            other.gameObject.GetComponent<PlayerController>().Damaged(1);
            Destroy(this.gameObject);
        }
    }
}
