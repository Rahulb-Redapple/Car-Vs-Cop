using SimpleObjectPoolingSystem;
using UnityEngine;

namespace RacerVsCops
{
    public class ParticleHandler : ObjectPoolBase
    {
        [SerializeField] private ParticleSystem explosionPS;

        internal void Init()
        {
            SetVisibility(true);
            Invoke(nameof(Cleanup), (explosionPS.main.duration + explosionPS.startLifetime));
        }

        private void Cleanup()
        {
            _objectPooling.ReturnObjectToPool(this, poolObjectType);
        }
    }
}
