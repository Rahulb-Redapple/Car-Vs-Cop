using SimpleObjectPoolingSystem;
using UnityEngine;

namespace RacerVsCops
{
    public class Cop : VehicleBase, IHealth
    {
        [SerializeField] private CopsLight _copsLight;

        private Transform target;

        internal void SetCopVehicle(Transform player, Vector3 position, Quaternion rotation, Transform parent)
        {
            transform.SetParent(parent);
            transform.position = position;
            transform.rotation = rotation;
            IsVehicleReady = true;
            target = player;
            gameObject.SetActive(true);
            _copsLight.Init();
        }

        internal override void LookAtTarget()
        {
            if (!target)
                return;

            Vector3 dir = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, VehicleConfig.vehicleSetting.TurnSpeed * Time.deltaTime);
        }

        internal override void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out VehicleIdentifier vehicleIdentifier))
            {
                switch(vehicleIdentifier.vehicleType)
                {
                    case VehicleType.COP:
                        InitiateVehicleHealth(OnHitHealthDecrease);
                        break;
                }               
            }
        }

        public void InitiateVehicleHealth(int damageCount)
        {
            if (CurrentHealth >= 1)
            {
                if (CurrentHealth > 1)
                    ParticleTrail.HandleTrails(TotalHealth - CurrentHealth);

                CurrentHealth -= damageCount;
                if (CurrentHealth <= 0)
                {
                    GameHelper.Instance.InvokeAction(GameConstants.UpdateSpawnedCopList, GetInstanceID());
                    GameplayHelper.GetCopCount(1);
                    ExplodeVehicle();
#if UNITY_ANDROID || UNITY_IOS
                    Vibration.Vibrate(100); // TODO :: Change later from here
#endif
                    Cleanup();
                }
            }
        }

        internal override void Cleanup()
        {
            _copsLight.Cleanup();
            EssentialHelperData.AccessData<ObjectPooling>().ReturnObjectToPool(gameObject, ObjectPoolType);
        }
    }
}
