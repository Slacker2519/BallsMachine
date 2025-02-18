using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private BulletSO _bulletSO;

    protected BulletSO BulletSO => _bulletSO;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public virtual void Shoot(Vector2 direction)
    {
        if (!_bulletSO) return;

        _rb.linearVelocity = direction * _bulletSO.Speed;
    }
}
