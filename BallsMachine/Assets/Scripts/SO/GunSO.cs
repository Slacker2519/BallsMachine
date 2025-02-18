using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/GunSO")]
public class GunSO : ScriptableObject
{
    [SerializeField] private float _fireRate = Constants.GUN_FIRE_RATE;

    public float FireRate => _fireRate;
}
