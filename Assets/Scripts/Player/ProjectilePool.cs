using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    #region Properties
    public static ProjectilePool Instance { get; private set; }
    public GameObject ProjectilePrefab;
    public int PoolSize = 20;
    private List<GameObject> _pool = new List<GameObject>();
    #endregion

    #region UnityEngine
    private void Awake()
    {
        Instance = this;
        
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(ProjectilePrefab);
            obj.SetActive(false);
            _pool.Add(obj);
        }
    }
    #endregion

    #region Custom
    public GameObject GetProjectile()
    {
        foreach (var projectile in _pool)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }

        // Optionally, you could expand the pool here if needed
        return null; // No available projectiles
    }
    #endregion
}
