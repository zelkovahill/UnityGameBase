using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] private GameObject[] prefabs;
    private int poolSize = 1;
    private List<GameObject>[] objectPools;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitObjectPool();
    }

    private void InitObjectPool()
    {
        objectPools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            objectPools[i] = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                objectPools[i].Add(obj);
            }
        }
    }

    public GameObject ActiveObject(int index)
    {
        GameObject obj = null;

        for (int i = 0; i < objectPools[index].Count; i++)
        {
            if (!objectPools[index][i].activeInHierarchy)
            {
                obj = objectPools[index][i];
                obj.SetActive(true);
                return obj;
            }
        }

        obj = Instantiate(prefabs[index]);
        objectPools[index].Add(obj);
        obj.SetActive(true);

        return obj;
    }
}
