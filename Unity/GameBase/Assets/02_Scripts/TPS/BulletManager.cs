using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private float destroyTime = 3f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            DestroyBullet();
        }

    }

    private void FixedUpdate()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        _rigidbody.velocity = transform.forward * moveSpeed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
        destroyTime = 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyBullet();
    }
}
