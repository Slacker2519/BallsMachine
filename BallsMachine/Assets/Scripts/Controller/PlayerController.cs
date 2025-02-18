using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BaseGun _gun;
    [SerializeField] private EBullet _bulletType;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _gun.Fire(_bulletType);
        }
    }
}
