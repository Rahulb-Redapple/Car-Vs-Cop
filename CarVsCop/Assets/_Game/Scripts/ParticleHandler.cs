using SimpleObjectPoolingSystem;
using UnityEngine;

namespace RacerVsCops
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private PoolObjectType objectType;
        [SerializeField] private ParticleSystem explosionPS;

        private EssentialHelperData _essentialHelperData;

        internal void Init(EssentialHelperData essentialHelperData)
        {
            _essentialHelperData = essentialHelperData;
            gameObject.SetActive(true);
            Invoke(nameof(DeActivate), (explosionPS.main.duration + explosionPS.startLifetime));
        }

        private void DeActivate()
        {
            _essentialHelperData.AccessData<ObjectPooling>().ReturnObjectToPool(gameObject, PoolObjectType.EXPLOSION);
        }
    }
}
