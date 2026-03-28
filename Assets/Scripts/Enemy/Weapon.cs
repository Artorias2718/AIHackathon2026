using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemy
{
    public class Weapon : MonoBehaviour
    {
        #region Properties
        public float fireRate = 0.05f;
        public GameObject ShotPrefab;
        public Transform FirePoint;

        //private InputAction _fireAction;
        private float _nextFireTime = 0f;
        #endregion

        #region UnityEngine
        private void Awake()
        {
            var playerInput = GetComponent<PlayerInput>();
            //_fireAction = playerInput.actions["Fire"];
        }

        private void Update()
        {
            if(Time.time >= _nextFireTime)
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
}