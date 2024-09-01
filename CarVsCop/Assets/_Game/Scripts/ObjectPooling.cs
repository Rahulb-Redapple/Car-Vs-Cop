using RacerVsCops;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleObjectPoolingSystem
{
    public abstract class ObjectPoolBase : MonoBehaviour
    {
        [SerializeField] private PoolObjectType _poolObjectType;

        protected ObjectPooling _objectPooling;
        protected PoolObjectType poolObjectType => _poolObjectType;

        internal virtual void Init(ObjectPooling objectPooling)
        {
            _objectPooling = objectPooling;
        }

        internal void SetVisibility(bool isActive)
        {
            gameObject.SetActive(isActive); 
        }

        internal abstract void Cleanup();
    }

    [System.Serializable]
    public class PoolInfo
    {
        [Tooltip("Select the type of gameobject to obtain from pool")]
        public PoolObjectType type;
        [Tooltip("Enter the maximum amount of gameobject to generate in the pool")]
        public int amount = 0;
        [Tooltip("Select the prefab that will be spawn from pooler")]
        public GameObject ObjectToPool;
        [Tooltip("Select the appropiate gameobject from hierarchy to store the instantiated objects")]
        public GameObject container;

        [HideInInspector]
        public List<GameObject> pool = new List<GameObject>();
    }

    public class ObjectPooling : EssentialHelper
    {
        [SerializeField] List<PoolInfo> listOfPool;

        internal override void Init()
        {
            for (int i = 0; i < listOfPool.Count; i++)
            {
                FillPool(listOfPool[i]);
            }
        }

        #region Instantiating Objects And Adding Them To Pool
        private void FillPool(PoolInfo info)
        {
            for (int i = 0; i < info.amount; i++)
            {
                GameObject obInstance = null;
                obInstance = Instantiate(info.ObjectToPool, info.container.transform);
                obInstance.gameObject.SetActive(false);
                obInstance.transform.position = Vector3.zero;
                
                if(obInstance.TryGetComponent(out ObjectPoolBase objectPoolBase))
                {
                    objectPoolBase.Init(this);
                    info.pool.Add(obInstance);
                }
                else
                {
                    Debug.Log($"{info.ObjectToPool.name} is not inherited from ObjectPoolBase class");
                }
            }
        }
        #endregion


            #region Fetch Objects From Pool Code
        public GameObject GetObjectFromPool(PoolObjectType type)
        {
            PoolInfo selected = GetPoolByType(type);
            List<GameObject> pool = selected.pool;

            GameObject obInstance = null;
            if (pool.Count > 0)
            {
                obInstance = pool[pool.Count - 1];
                pool.Remove(obInstance);
            }
            else
                obInstance = Instantiate(selected.ObjectToPool, selected.container.transform);

            return obInstance;
        }

        private PoolInfo GetPoolByType(PoolObjectType type)
        {
            for (int i = 0; i < listOfPool.Count; i++)
            {
                if (type == listOfPool[i].type)
                    return listOfPool[i];
            }
            return null;
        }
        #endregion


        #region Return Objects To Pool Code
        public void ReturnObjectToPool(ObjectPoolBase ob, PoolObjectType type)
        {
            ob.SetVisibility(false);
            ob.transform.position = Vector3.zero;

            PoolInfo selected = GetPoolByType(type);
            List<GameObject> pool = selected.pool;
            ob.transform.SetParent(selected.container.transform);

            if (!pool.Contains(ob.gameObject))
                pool.Add(ob.gameObject);
        }
        #endregion
    }
}

