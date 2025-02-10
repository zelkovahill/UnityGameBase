using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTPS : MonoBehaviour
{
    public static GameManagerTPS instance;

    [Header("Bullet")]
    [SerializeField]
    private Transform bulletPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float maxShootDelay = 0.2f;
    [SerializeField]
    private float currentMaxShootDelay = 0.2f;

    [SerializeField]
    private Text bulletText;
    private int maxBullet = 30;
    private int currentBullet = 0;

    [Header("Weapon FX")]
    [SerializeField]
    private GameObject weaponFlashFX;
    [SerializeField]
    private Transform bulletCasePoint;
    [SerializeField]
    private GameObject bulletCaseFX;
    [SerializeField]
    private Transform weaponClipPoint;
    [SerializeField]
    private GameObject weaponClipFX;

    private void Start()
    {
        instance = this;

        currentMaxShootDelay = 0;
        InitBullet();
    }

    private void Update()
    {
        bulletText.text = currentBullet + " / " + maxBullet;
    }

    public void Shooting(Vector3 targetPosition)
    {
        currentMaxShootDelay += Time.deltaTime;

        if (currentMaxShootDelay < maxShootDelay || currentBullet <= 0)
        {
            return;
        }

        currentBullet--;
        currentMaxShootDelay = 0;

        Instantiate(weaponFlashFX, bulletPoint);
        Instantiate(bulletCaseFX, bulletCasePoint);

        Vector3 aim = (targetPosition - bulletPoint.position).normalized;
        Instantiate(bulletPrefab, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up));
    }

    public void ReloadClip()
    {
        Instantiate(weaponClipFX, weaponClipPoint);
        InitBullet();
    }

    private void InitBullet()
    {
        currentBullet = maxBullet;
    }
}
