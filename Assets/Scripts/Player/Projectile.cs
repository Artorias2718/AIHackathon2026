using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Properties
    public float launchForce = 15f;
    public float lifetime = 5f;

    private Rigidbody2D _rigidbody;
    #endregion

    #region UnityEngine
    private void Awake()
        => _rigidbody = GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        _rigidbody.linearVelocity = Vector2.zero; // Reset velocity to ensure consistent behavior
        _rigidbody.AddForce(transform.up * launchForce, ForceMode2D.Impulse);
        Invoke("DisableSelf", 5f);
    }

    private void OnDisable()
        => CancelInvoke();
    #endregion

    #region Custom
    private void DisableSelf()
        => gameObject.SetActive(false);

    #endregion
}
