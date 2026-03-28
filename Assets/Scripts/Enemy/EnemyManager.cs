using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Properties
        [Header("Formation Settings")]
        public int rows = 4;
        public int columns = 8;
        public float spacing = 1.5f;
        // Empty object that moves/sways
        public Transform formationParent;

        [Header("Spawning")]
        public ObjectPool objectPool;
        public float spawnInterval = 0.5f;
        #endregion

        #region UnityEngine
        private void Start()
        {
            StartCoroutine(CreateFormation());
        }

        private void Update()
        {
        
        }
        #endregion

        #region Custom
        private IEnumerator CreateFormation()
        {
            for(int r = 0; r < rows; ++r)
            {
                for(int c = 0; c < columns; ++c)
                {
                    // Determine which "tag or type of enemy" to spawn based on row or column
                    string enemyType = "";
                    switch(r)
                    {
                        case 0:
                        case 1:
                            enemyType = "Bee";
                            break;
                        case 2: 
                            enemyType = "Butterfly";
                            break;
                        default:
                            enemyType = "Boss";
                            break;

                    }

                    GameObject enemy = objectPool.GetPooledObject(enemyType);

                    if(enemy != null)
                    {
                        Vector3 homePos = new Vector3(c * spacing, r * spacing, 0);

                        enemy.transform.position = new Vector3(0, 10, 0);
                        enemy.SetActive(true);

                        EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
                        behavior.SetHome(formationParent, homePos);
                    
                        yield return new WaitForSeconds(spawnInterval);
                    }
                }
            }
        }
        #endregion
    }
}