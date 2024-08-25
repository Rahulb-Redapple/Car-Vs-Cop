using SimpleObjectPoolingSystem;
using UnityEngine;

namespace RacerVsCops
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private PoolObjectType objectType;
        [SerializeField] private ParticleSystem explosionPS;

        private EssentialHelperData _essentialHelperData;
        private ObjectPooling _objectPooling;

        internal void Init(EssentialHelperData essentialHelperData)
        {
            _essentialHelperData = essentialHelperData;
            _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();
            gameObject.SetActive(true);
            Invoke(nameof(Cleanup), (explosionPS.main.duration + explosionPS.startLifetime));
        }

        private void Cleanup()
        {
            _objectPooling.ReturnObjectToPool(gameObject, PoolObjectType.EXPLOSION);
        }
    }
}
