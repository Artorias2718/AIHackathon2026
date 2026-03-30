using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private ObjectPool _pool; // Reference to your pool script
        private List<EnemyBehavior> _activeEnemies = new List<EnemyBehavior>();

        [Header("Formation Settings")]
        public Transform formationRoot;
        public int rows = 4;
        public int cols = 10;
        public Vector2 spacing = new Vector2(1.2f, 1f);

        private void Awake()
        {
            // Find the pool on the "Enemies" root object
            _pool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            SpawnFormation();
        }

        private void SpawnFormation()
        {
            for (int r = 0; r < rows; r++)
            {
                // Assign a tag based on the row to get the right color/prefab
                string rowTag = GetTagForByRow(r);

                for (int c = 0; c < cols; c++)
                {
                    // 1. Get the object from the pool instead of Instantiate
                    GameObject go = _pool.GetPooledObject(rowTag);

                    if (go != null)
                    {
                        // 2. Position it at the spawn point and enable it
                        go.transform.position = new Vector3(0, 10, 0);
                        go.SetActive(true);

                        // 3. Set the home position data
                        EnemyBehavior behavior = go.GetComponent<EnemyBehavior>();
                        behavior.formationTransform = formationRoot;
                        behavior.localHomePosition = new Vector3(
                            (c - (cols / 2f)) * spacing.x,
                            (r - (rows / 2f)) * spacing.y,
                            0
                        );

                        _activeEnemies.Add(behavior);
                    }
                }
            }
        }

        private string GetTagForByRow(int row)
        {
            // Match these to the tags you set in your ObjectPool component!
            if (row == 0) return "Bee";
            if (row == 1) return "Butterfly";
            return "Boss";
        }
    }
}