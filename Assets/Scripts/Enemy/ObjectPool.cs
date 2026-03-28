using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ObjectPool : MonoBehaviour
    {
        #region Properties
        [System.Serializable]
        public class PoolItem
        {
            public string tag;          // e.g., "Bee"
            public Color color;        // Enemy color for visual distinction
            public GameObject prefab;   // The prefab to spawn
            public int amount;          // How many to pre-instantiate
        }    

        public GameObject enemyPrefab;
        public List<PoolItem> poolItems;
        private Dictionary<string, List<GameObject>> _pool;
        #endregion

        #region UnityEngine
        private void Awake()
        {
            _pool = new Dictionary<string, List<GameObject>>();
            foreach(PoolItem item in poolItems)
            {
                List<GameObject> objectList = new List<GameObject>();
                for (int i = 0; i < item.amount; i++)
                {
                    GameObject obj = Instantiate(item.prefab);
                    obj.transform.Find("Triangle").GetComponent<SpriteRenderer>().color = item.color; // Set color for visual distinction
                    obj.SetActive(false);
                    if (!_pool.ContainsKey(item.tag))
                    {
                        _pool.Add(item.tag, new List<GameObject>());
                    }
                    _pool[item.tag].Add(obj);

                }
            }
        }
        #endregion

        #region Custom
        public GameObject GetPooledObject(string tag)
        {
            if(!_pool.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            foreach (GameObject obj in _pool[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }

            return null; // No available object
        }
        #endregion
    }
}