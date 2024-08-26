using SimpleObjectPoolingSystem;
using UnityEngine;

namespace RacerVsCops
{
    public class Ground : ObjectPoolBase
    {
        private EssentialHelperData _essentialHelperData;

        internal void Init(EssentialHelperData essentialHelperData, Vector3 position, Quaternion rotation, Transform parent)
        {
            _essentialHelperData = essentialHelperData;
            transform.position = position;
            transform.rotation = rotation;
            transform.SetParent(parent);
            gameObject.SetActive(true);
        }

        internal void Cleanup()
        {
            _essentialHelperData.AccessData<ObjectPooling>().ReturnObjectToPool(this, poolObjectType);
        }
    }
}
