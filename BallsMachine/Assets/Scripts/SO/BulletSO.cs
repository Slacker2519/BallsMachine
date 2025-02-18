using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "Scriptable Objects/BulletSO")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private EBullet _type;
    [SerializeField] private float _lifeSpan = Constants.BULLET_LIFE_SPAN;
    [SerializeField] private float _speed = 10f;

    #region Getters & Setters
    public EBullet Type => _type;
    public float LifeSpan => _lifeSpan;
    public float Speed => _speed;
    #endregion
}
