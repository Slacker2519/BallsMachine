using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [SerializeField] protected Canvas _Canvas;
    [SerializeField] private GunSO _gunSO;
    [SerializeField] private Transform _ammoTrans;
    [SerializeField] private EGun _type;

    private float _currentGunCooldown = 0f;
    private Vector2 _direction;
    protected float _OffSet = 270f;

    protected GunSO _GunSO => _gunSO;
    protected Transform _AmmoTrans => _ammoTrans;

    protected virtual void Update()
    {
        FacingMouse();

        if (_currentGunCooldown > 0f)
        {
            _currentGunCooldown -= Time.deltaTime;
        }
    }

    private void FacingMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, mousePos, _Canvas.worldCamera, out _direction);
        Vector2 direction = (_direction - (Vector2)transform.localPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + _OffSet));
    }

    public virtual void Fire(EBullet ammoType)
    {
        if (_currentGunCooldown <= 0)
        {
            _currentGunCooldown = _gunSO.GunDatas.Find(x => x.Type == _type).FireRate;
            Vector2 direction = (_direction - (Vector2)transform.localPosition).normalized;
            GameObject bullet = PoolManager.Instance.SpawnBullet(ammoType, _ammoTrans);
            bullet.GetComponent<BaseBullet>().Shoot(direction);
        }
    }
}
