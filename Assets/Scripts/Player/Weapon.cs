using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    #region Properties
    public GameObject ShotPrefab;
    public Transform FirePoint;

    private InputAction _fireAction;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        _fireAction = playerInput.actions["Fire"];
    }

    private void OnEnable()
        => _fireAction.performed += HandleFireAction;

    private void OnDisable()
        => _fireAction.performed -= HandleFireAction;
    #endregion

    #region Custom
    private void HandleFireAction(InputAction.CallbackContext context)
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
