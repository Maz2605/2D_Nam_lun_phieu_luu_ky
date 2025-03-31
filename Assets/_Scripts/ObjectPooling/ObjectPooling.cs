using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace _Scripts.ObjectPooling
{
    public static class PoolingManager
    {
        private static Dictionary<int, Pool> _listPools;

        private static void Init(GameObject prefab = null)
        {
            if (_listPools == null)
            {
                _listPools = new Dictionary<int, Pool>();
            }

            if (prefab != null && !_listPools.ContainsKey(prefab.GetInstanceID()))
            {
                _listPools[prefab.GetInstanceID()] = new Pool(prefab);
            }
        }
        public static GameObject Spawn(GameObject prefab)
        {
            Init(prefab);
            return _listPools[prefab.GetInstanceID()].Spawn(Vector3.zero, Quaternion.identity);
        }
        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent = null)
        {
            Init(prefab);
            return _listPools[prefab.GetInstanceID()].Spawn(position, quaternion, parent);
        }
        public static T Spawn<T>(T prefab) where T : Component
        {
            Init(prefab.gameObject);
            return _listPools[prefab.GetInstanceID()].Spawn<T>(Vector3.zero, Quaternion.identity);
        }

        public static T Spawn<T>(T prefab, Vector3 position, Quaternion quaternion, Transform parent = null) where T : Component
        {
            Init(prefab.gameObject);
            return _listPools[prefab.gameObject.GetInstanceID()].Spawn<T>(position, quaternion, parent);
        }
        public static void Despawn(GameObject prefab, Action action = null)
        {
            Pool p = null;
            foreach (var pool in _listPools.Values)
            {
                if (pool.IDObject.Contains(prefab.GetInstanceID()))
                {
                    p = pool;
                    break;
                }
            }

            if (p == null)
            {
                Object.Destroy(prefab);
            }
            else
            {
                p.Despawn(prefab);
            }
        }

        public static void ClearPool()
        {
            if (_listPools.Count > 0)
            {
                _listPools.Clear();
            }
        }
}
    public class Pool
    {
        private readonly Queue<GameObject> _pools;
        public readonly HashSet<int> IDObject;
        private readonly GameObject _prefabObject;
        private int _id = 0;

        public Pool(GameObject gameObject)
        {
            _prefabObject = gameObject;
            _pools = new Queue<GameObject>();
            IDObject = new HashSet<int>();
        }
        public GameObject Spawn(Vector3 position, Quaternion quaternion, Transform parent = null)
        {
            while (true)
            {
                GameObject newObject;
                if (_pools.Count == 0)
                {
                    newObject = Object.Instantiate(_prefabObject,position ,quaternion,parent);
                    _id += 1;
                    IDObject.Add(newObject.GetInstanceID());
                    newObject.name = _prefabObject.name + "_" + _id;
                    return newObject;
                }
                newObject = _pools.Dequeue();
                if (newObject == null)
                {
                    continue;
                }
                newObject.transform.SetPositionAndRotation(position, quaternion);
                newObject.transform.SetParent(parent);
                /*
                newObject.name = prefabObject.name;
                */
                newObject.SetActive(true);
                return newObject;
            }
        }
        public T Spawn<T>(Vector3 position, Quaternion quaternion, Transform parent = null)
        {
            return Spawn(position, quaternion, parent).GetComponent<T>();
        }
        public void Despawn(GameObject gameObject)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            gameObject.SetActive(false);
            _pools.Enqueue(gameObject);
        }
    }
}

