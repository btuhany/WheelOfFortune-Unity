using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace WheelOfFortune.Tools
{
    public abstract class ObjectPoolMonoBehaviour<T> : SingletonMonoBehaviour<ObjectPoolMonoBehaviour<T>> where T : MonoBehaviour
    {
        [Header("Pool Config")]
        [SerializeField] protected T _prefab;
        [SerializeField] private int _size;
        [SerializeField] private int _expandingSize;

        private Queue<T> _pooledObjects;
        protected Transform _transform;
        protected override void Awake()
        {
            base.Awake();
            _transform = GetComponent<Transform>();
            InitializePool();
        }
        private void ExpandPool(int size)
        {
            for (int i = 0; i < size; i++)
            {
                T newObj = Instantiate(_prefab, _transform);
                newObj.gameObject.SetActive(false);
                _pooledObjects.Enqueue(newObj);
            }
        }
        private void InitializePool()
        {
            _pooledObjects = new Queue<T>();
            ExpandPool(_size);
        }
        public T GetObject(bool active = true)
        {
            if (!gameObject.activeSelf)
                Debug.LogError("Pool object is not enabled!");
            else if (_pooledObjects == null)
                InitializePool();

            if (_pooledObjects.Count <= 0)
                ExpandPool(_expandingSize);

            T newObj = _pooledObjects.Dequeue();
            newObj.gameObject.SetActive(active);
            //newObj.transform.parent = null;
            return newObj;
        }
        public virtual void ReturnObject(T obj)
        {
            if (!gameObject.activeSelf)
                Debug.LogError("Pool object is not enabled!");

            obj.gameObject.SetActive(false);
            obj.gameObject.transform.SetParent(_transform);
            _pooledObjects.Enqueue(obj);
        }
    }
}
