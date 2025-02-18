using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/GunSO")]
public class GunSO : ScriptableObject
{
    [SerializeField] private List<GunData> gunDatas;
    public List<GunData> GunDatas => gunDatas;
}

[Serializable]
public class GunData
{
    [SerializeField] private EGun _type;
    [SerializeField] private float _fireRate = Constants.GUN_FIRE_RATE;
    [SerializeField] private GameObject _prefab;

    #region Getters & Setters
    public EGun Type => _type;
    public float FireRate => _fireRate;
    public GameObject Prefab => _prefab;
    #endregion
}
