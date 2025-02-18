using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BulletSO _bulletSO;
    [SerializeField] private EBullet _type;

    protected Rigidbody2D _Rb => _rb;
    protected BulletSO _BulletSO => _bulletSO;
    protected EBullet _Type => _type;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        ResetBulletVelocity();
    }

    protected virtual void Update()
    {

    }

    public virtual void Shoot(Vector2 direction)
    {
        if (!_bulletSO) return;

        ResetBulletVelocity();
        _rb.linearVelocity = direction * _bulletSO.BulletDatas.Find(x => x.Type == _type).Speed;
    }

    public void ResetBulletVelocity()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected virtual void OnDisable()
    {
        ResetBulletVelocity();
    }
}
