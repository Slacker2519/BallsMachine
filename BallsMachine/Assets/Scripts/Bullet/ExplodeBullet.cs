using DG.Tweening;
using UnityEngine;

public class ExplodeBullet : BaseBullet
{
    [SerializeField] private LayerMask _bulletLayer;
    [SerializeField] private ParticleSystem _explodeEffect;
    private float _currentLifeSpan = 0f;
    private bool _isHittingWall = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        _isHittingWall = false;
    }

    protected override void Update()
    {
        if (_isHittingWall)
        {
            CountDownSelfDestroy();
        }
    }

    protected void CountDownSelfDestroy()
    {
        var data = _BulletSO.BulletDatas.Find(x => x.Type == _Type);

        if (_currentLifeSpan < data.LifeSpan)
        {
            _currentLifeSpan += Time.deltaTime;
        }
        else
        {
            _currentLifeSpan = 0f;
            _isHittingWall = false;
            ResetBulletVelocity();
            Explode();

            DOVirtual.DelayedCall(0.2f, () =>
            {
                PoolManager.Instance.ReturnObject(gameObject);
            });
        }
    }

    private void Explode()
    {
        BulletData data = _BulletSO.BulletDatas.Find(x => x.Type == _Type);
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, data.ExplodeRadius, _bulletLayer);

        if (rangeCheck.Length > 0)
        {
            foreach (Collider2D hit in rangeCheck)
            {
                if (hit.TryGetComponent<BaseBullet>(out BaseBullet bullet))
                {
                    _explodeEffect.Play();
                    bullet.ResetBulletVelocity();
                    Vector2 direction = (Vector2)bullet.transform.localPosition - (Vector2)transform.localPosition;
                    bullet.Shoot(direction.normalized);
                }
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.WALLTAG))
        {
            _isHittingWall = true;
        }
    }
}
