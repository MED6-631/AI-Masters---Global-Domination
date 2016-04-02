namespace AI.Master
{
    using UnityEngine;
    using System.Collections.Generic;

    public sealed class UnitPool
    {
        private static int _nextId;
        private Queue<UnitBase> _pool;
        private GameObject _prefab;
        private GameObject _host;
        private int _count;



        public UnitPool(GameObject prefab, GameObject host, int initialInstanceCount)
        {
            _pool = new Queue<UnitBase>(initialInstanceCount);
            _prefab = prefab;
            _host = host;

            for (int i = 0; i < initialInstanceCount; i++)
            {
                _pool.Enqueue(CreateInstance());

            }
        }

        public int count
        {
            get { return _count; }
        }

        public UnitBase Get(Vector3 position, Quaternion rotation)
        {
            UnitBase unit;
            if(_pool.Count > 0)
            {
                unit = _pool.Dequeue();
            }
            else
            {
                unit = CreateInstance();
            }

            var t = unit.gameObject.transform;

            t.position = position;
            t.rotation = rotation;

            _count++;
            unit.id = _nextId++;
            unit.gameObject.SetActive(true);
            return unit;
        }


        public void Return(UnitBase item)
        {
            _count--;
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }

        private UnitBase CreateInstance()
        {
            var go = GameObject.Instantiate(_prefab);
            if(_host != null)
            {
                go.transform.SetParent(_host.transform);
            }

            go.SetActive(false);
            return go.GetComponent<UnitBase>();
        }
    }
}

