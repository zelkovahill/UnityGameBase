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

    public void Shooting(Vector3 targetPosition, TargetEnemy targetEnemy)
    {
        currentMaxShootDelay += Time.deltaTime;

        if (currentMaxShootDelay < maxShootDelay || currentBullet <= 0)
        {
            return;
        }

        currentBullet--;
        currentMaxShootDelay = 0;
        Vector3 aim = (targetPosition - bulletPoint.position).normalized;




        // Instantiate(weaponFlashFX, bulletPoint);
        GameObject flashFX = PoolManager.instance.ActiveObject(1);
        SetObjectActive(flashFX, bulletPoint);
        flashFX.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);

        // Instantiate(bulletCaseFX, bulletCasePoint);
        GameObject caseFX = PoolManager.instance.ActiveObject(2);
        SetObjectActive(caseFX, bulletCasePoint);


        // Instantiate(bulletPrefab, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up));
        GameObject prefabToSpawn = PoolManager.instance.ActiveObject(0);
        SetObjectActive(prefabToSpawn, bulletPoint);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);


        // Raycast을 이용하여 데미지주기 (Raycast를 쏴서 적을 맞추는 방식)
        // if (targetEnemy != null && targetEnemy.currentHP > 0)
        // {
        //     targetEnemy.currentHP -= 1;
        //     Debug.Log("Enemy HP : " + targetEnemy.currentHP);
        // }

    }

    public void ReloadClip()
    {
        // Instantiate(weaponClipFX, weaponClipPoint);
        GameObject clipFX = PoolManager.instance.ActiveObject(3);
        SetObjectActive(clipFX, weaponClipPoint);
        InitBullet();
    }

    private void InitBullet()
    {
        currentBullet = maxBullet;
    }

    private void SetObjectActive(GameObject obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }
}
