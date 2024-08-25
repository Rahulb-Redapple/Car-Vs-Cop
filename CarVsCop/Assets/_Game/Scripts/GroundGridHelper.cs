using SimpleObjectPoolingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class GroundGridHelper : MonoBehaviour
    {
        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] int _groundSize;
        private Transform _target;
        private EssentialHelperData _essentialHelperData;

        private int _leftId = 0;
        private int _topId = 0;
        private Vector3 _midPosition;

        private GameObject[,] _groundList;

        private bool isReadyToGenerateGrid = false;

        private List<Ground> _instantiatedGroundsList = new List<Ground>();

        internal void Init(EssentialHelperData essentialHelperData)
        {
            _essentialHelperData = essentialHelperData;
        }

        internal void ReadyToGenerateGrid(bool isReady)
        {
            isReadyToGenerateGrid = isReady;
            ResetData();
        }

        internal void ReadyToPlay(Transform target)
        {
            _groundList = new GameObject[_width, _height];
            GenerateGround();
            _target = target;
        }

        private void Update()
        {
            if (!isReadyToGenerateGrid)
                return;

            CalculateGroundPosition();
        }

        private void CalculateGroundPosition()
        {
            int LIndex = (int)Mathf.Repeat(_leftId, _width);
            int RIndex = (int)Mathf.Repeat(_leftId + (_width - 1), _width);

            int TIndex = (int)Mathf.Repeat(_topId, _height);
            int BIndex = (int)Mathf.Repeat(_topId + (_height - 1), _height);

            _midPosition.x = (_groundList[LIndex, TIndex].transform.position.x + _groundList[RIndex, BIndex].transform.position.x) / 2;
            _midPosition.z = (_groundList[LIndex, TIndex].transform.position.z + _groundList[RIndex, BIndex].transform.position.z) / 2;

            MoveGrid(LIndex, RIndex, TIndex, BIndex);
        }

        private void MoveGrid(int LIndex, int RIndex, int TIndex, int BIndex)
        {
            if (_target.position.x > (_midPosition.x + _groundSize / 2))
            {
                for (int z = 0; z < _height; z++)
                {
                    Vector3 tmpPosition = _groundList[LIndex, z].transform.position;
                    tmpPosition.x += (_groundSize * _width);
                    _groundList[LIndex, z].transform.position = tmpPosition;
                }

                _leftId++;
            }
            else if ((_target.position.x < (_midPosition.x - _groundSize / 2)))
            {
                for (int z = 0; z < _height; z++)
                {
                    Vector3 tmpPosition = _groundList[RIndex, z].transform.position;
                    tmpPosition.x -= (_groundSize * _width);
                    _groundList[RIndex, z].transform.position = tmpPosition;
                }

                _leftId--;
            }

            if (_target.position.z > (_midPosition.z + _groundSize / 2))
            {
                for (int x = 0; x < _width; x++)
                {
                    Vector3 tmpPosition = _groundList[x, TIndex].transform.position;
                    tmpPosition.z += (_groundSize * _height);
                    _groundList[x, TIndex].transform.position = tmpPosition;
                }

                _topId++;
            }
            else if ((_target.position.z < (_midPosition.z - _groundSize / 2)))
            {
                for (int x = 0; x < _width; x++)
                {
                    Vector3 tmpPosition = _groundList[x, BIndex].transform.position;
                    tmpPosition.z -= (_groundSize * _height);
                    _groundList[x, BIndex].transform.position = tmpPosition;
                }

                _topId--;
            }
        }

        private void GenerateGround()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    Vector3 pos = new Vector3(x * _groundSize, 0, z * _groundSize);
                    _groundList[x, z] = _essentialHelperData.AccessData<ObjectPooling>().GetObjectFromPool(PoolObjectType.GROUND);
                    Ground ground = _groundList[x, z].GetComponent<Ground>();
                    ground.Init(_essentialHelperData, pos, Quaternion.identity, this.transform);
                    //_groundList[x, z].name = $"Ground [{x}, {z}]";
                    _instantiatedGroundsList.Add(ground);
                }
            }
        }

        internal void Cleanup()
        {
            if (!Equals(_instantiatedGroundsList.Count, 0))
            {
                foreach (Ground ground in _instantiatedGroundsList)
                {
                    ground.Cleanup();
                }
                _instantiatedGroundsList.Clear();
                ResetData();
            }
        }

        internal void ResetData()
        {
            _leftId = 0;
            _topId = 0;
            _midPosition = Vector3.zero;
        }
    }
}
