using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    public string PoolName;
    public GameObject Prefab;
    public List<GameObject> PoolList;

    public ObjectPool(string poolName, GameObject prefab)
    {
        PoolName = poolName;
        Prefab = prefab;
        PoolList = new List<GameObject>();
    }
}

public class PoolManager : SingletonMono<PoolManager>
{
    [SerializeField] private GunSO _gunSO;
    [SerializeField] private BulletSO _bulletSO;
    [SerializeField] private List<ObjectPool> _pools = new List<ObjectPool>();
    
    private List<Transform> _parentList = new List<Transform>();

    private void Awake()
    {
        LoadDataAssets();
    }

    private void LoadDataAssets()
    {
        foreach (var item in _bulletSO.BulletDatas)
        {
            var pool = new ObjectPool(item.Type.ToString(), item.Prefab);
            _pools.Add(pool);

            GameObject parent = new GameObject(pool.PoolName);
            parent.transform.SetParent(transform);
            _parentList.Add(parent.transform);
        }

        foreach (var item in _gunSO.GunDatas)
        {
            var pool = new ObjectPool(item.Type.ToString(), item.Prefab);
            _pools.Add(pool);

            GameObject parent = new GameObject(pool.PoolName);
            parent.transform.SetParent(transform);
            _parentList.Add(parent.transform);
        }
    }

    public void ClearPoolsData()
    {
        _pools.Clear();
    }

    public GameObject SpawnGun(EGun gunType, Transform parent = null)
    {
        return GetObjectFromPool(gunType.ToString(), parent);
    }

    public GameObject SpawnBullet(EBullet bulletType, Transform parent = null)
    {
        return GetObjectFromPool(bulletType.ToString(), parent);
    }

    private GameObject GetObjectFromPool(string poolName, Transform parent = null)
    {
        var pool = _pools.Find(x => x.PoolName.Equals(poolName));
        GameObject obj = null;
        if (pool == null)
        {
            Debug.LogError("Pool name invalid " + poolName.ToString());
        }
        else
        {
            if (parent == null)
            {
                parent = _parentList.Find(x => x.name.Equals(poolName));
            }
            obj = pool.PoolList.Find(x => !x.activeSelf);
            if (obj == null)
            {
                obj = Instantiate(pool.Prefab, parent);
                obj.name = poolName;
                pool.PoolList.Add(obj);
            }
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
        }
        return obj;
    }

    public void ReturnObject(GameObject obj, string poolName = "")
    {
        if (string.IsNullOrEmpty(poolName))
        {
            poolName = obj.name;
        }
        obj.SetActive(false);
        Transform parent = _parentList.Find(x => x.name.Equals(poolName));
        if (parent != null)
        {
            obj.transform.SetParent(parent.transform);
        }
    }

    public void ReturnAllObjects()
    {
        foreach (var pool in _pools)
        {
            foreach (var obj in pool.PoolList)
            {
                ReturnObject(obj, pool.PoolName);
            }
        }
    }
}
