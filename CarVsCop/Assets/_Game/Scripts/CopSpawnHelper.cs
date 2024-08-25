using System.Collections.Generic;
using UnityEngine;
using SimpleObjectPoolingSystem;
using System.Linq;

namespace RacerVsCops
{
    public class CopSpawnHelper : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private float distanceFromTarget = 10f;

        private Transform targetToChase;
        private EssentialHelperData _essentialHelperData;
        private GameplayHelper _gameplayHelper;
        private ObjectPooling _objectPooling;

        private WantedLevel _wantedLevel = WantedLevel.NONE;

        private List<Cop> _spawnedCopList = new List<Cop>();

        internal void SetTarget(Transform target)
        {
            targetToChase = target;
        }

        internal void Init()
        {
            GameHelper.Instance.StartListening(GameConstants.UpdateSpawnedCopList, UpdateSpawnedCopList);
            GameHelper.Instance.StartListening(GameConstants.UpdateCopType, UpdateWantedLevel);
        }

        internal void Init(GameplayHelper gameplayHelper, EssentialHelperData essentialHelperData)
        {
            _gameplayHelper = gameplayHelper;
            _essentialHelperData = essentialHelperData;
            _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();
        }

        internal void SpawnCops()
        {
            int upAngle = Random.Range(-30, 30);
            int downAngle = Random.Range(160, 200);
            int randomAngle = Random.value < 0.5f ? upAngle : downAngle;

            Vector3 dir = new Vector3(Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0, Mathf.Cos(randomAngle * Mathf.Deg2Rad));
            Vector3 copPosition = targetToChase.position + dir * distanceFromTarget;

            Cop cop = GetCops();
            Quaternion rotation = Quaternion.Euler(Vector3.up * (randomAngle + 180));
            cop.Init(_gameplayHelper, _essentialHelperData);
            cop.SetCopVehicle(targetToChase, copPosition, rotation, parent);
            _spawnedCopList.Add(cop);

        }

        private Cop GetCops() // TODO :: Will modify later for more cop vehicles
        {
            //switch(_wantedLevel)
            //{
            //    case WantedLevel.FIRST:
            //        return _objectPooling.GetObjectFromPool(PoolObjectType.COP_SEDAN).GetComponent<Cop>();

            //    case WantedLevel.SECOND:
            //        return _objectPooling.GetObjectFromPool(PoolObjectType.COP_MUSCLE).GetComponent<Cop>();

            //    case WantedLevel.THIRD:
            //        int rand = Random.Range(0, 2);
            //        return rand == 0 ? _objectPooling.GetObjectFromPool(PoolObjectType.COP_SEDAN).GetComponent<Cop>() : _objectPooling.GetObjectFromPool(PoolObjectType.COP_MUSCLE).GetComponent<Cop>();

            //}
            //return null;
            return _objectPooling.GetObjectFromPool(PoolObjectType.COP_SEDAN).GetComponent<Cop>();
        }

        private void UpdateWantedLevel(object obj)
        {
            _wantedLevel = (WantedLevel)obj;
        }

        private void UpdateSpawnedCopList(object obj)
        {
            if (!Equals(_spawnedCopList.Count, 0))
            {
                for(int i=0; i<_spawnedCopList.Count; i++)
                {
                    if (_spawnedCopList[i].GetInstanceID() == (int)obj)
                    {
                        _spawnedCopList.Remove(_spawnedCopList[i]);
                    }
                }
            }
        }

        internal void Cleanup()
        {
            if(!Equals(_spawnedCopList.Count, 0))
            {
                for(int i = 0; i<_spawnedCopList.Count; i++)
                {
                    if (_spawnedCopList[i].isActiveAndEnabled)
                    {
                        _spawnedCopList[i].Cleanup();
                    }
                }
                _spawnedCopList.Clear();
            }

            GameHelper.Instance.StopListening(GameConstants.UpdateSpawnedCopList, UpdateSpawnedCopList);
            GameHelper.Instance.StopListening(GameConstants.UpdateCopType, UpdateWantedLevel);
        }

    }
}
