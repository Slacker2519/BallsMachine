using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "Scriptable Objects/BulletSO")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private List<BulletData> bulletDatas;
    public List<BulletData> BulletDatas => bulletDatas;
}

[Serializable]
public class BulletData
{
    [SerializeField] private EBullet _type;
    [SerializeField] private float _lifeSpan = Constants.BULLET_LIFE_SPAN;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _explodeRadius = Constants.BULLET_EXPLODE_RADIUS;
    [SerializeField] private GameObject _prefab;

    #region Getters & Setters
    public EBullet Type => _type;
    public float LifeSpan => _lifeSpan;
    public float Speed => _speed;
    public float ExplodeRadius => _explodeRadius;
    public GameObject Prefab => _prefab;
    #endregion
}
