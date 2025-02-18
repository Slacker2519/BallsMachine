using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [SerializeField] protected Canvas _Canvas;
    [SerializeField] private GunSO _gunSO;

    private Vector2 _direction;
    protected float _OffSet = 270f;

    protected GunSO _GunSO => _gunSO;

    protected virtual void Update()
    {
        FacingMouse();
    }

    private void FacingMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, mousePos, _Canvas.worldCamera, out _direction);
        Vector2 direction = (_direction - (Vector2)transform.localPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + _OffSet));
    }

    public virtual void Fire()
    {

    }
}
