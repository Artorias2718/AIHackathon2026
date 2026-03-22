using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    #region Properties
    public float fireRate = 0.5f;
    public GameObject ShotPrefab;
    public Transform FirePoint;

    private InputAction _fireAction;
    private float _nextFireTime = 0f;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        _fireAction = playerInput.actions["Fire"];
    }

    private void Update()
    {
        if(_fireAction.IsPressed() && Time.time >= _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + fireRate;
        }
    }
    #endregion

    #region Custom
    private void Fire()
    {
        GameObject projectile = ProjectilePool.Instance.GetProjectile();
        if (projectile != null)
        {
            projectile.transform.position = FirePoint.position;
            projectile.transform.rotation = FirePoint.rotation;
        }
    }
    #endregion
}
