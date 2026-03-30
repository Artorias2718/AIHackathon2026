using UnityEngine;

namespace Enemy
{
    public class Weapon : MonoBehaviour
    {
        #region Properties
        public float fireRate = 0.05f;
        public GameObject ShotPrefab;
        public Transform FirePoint;
        #endregion

        #region Custom
        public void Fire()
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