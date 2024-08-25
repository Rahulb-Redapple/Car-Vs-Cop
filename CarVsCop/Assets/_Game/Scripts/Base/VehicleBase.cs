using SimpleObjectPoolingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(VehicleIdentifier))]
    public abstract class VehicleBase : MonoBehaviour
    {
        [SerializeField] private VehicleConfig _vehicleConfig;
        [SerializeField] private ParticleTrail _particleTrail;
        [SerializeField] private List<Transform> wheels = new List<Transform>();
        [SerializeField] private int onHitHealthDecrease = 1;
        [SerializeField] private PoolObjectType poolObjectType;

        private int currentHealth;
        private bool isVehicleReady = false;
        private EssentialHelperData _essentialHelperData;
        private GameplayHelper _gameplayHelper;

        protected List<Transform> Wheels => wheels;
        protected EssentialHelperData EssentialHelperData => _essentialHelperData;
        protected GameplayHelper GameplayHelper => _gameplayHelper;
        internal VehicleConfig VehicleConfig => _vehicleConfig;
        internal ParticleTrail ParticleTrail => _particleTrail;
        internal int TotalHealth => VehicleConfig.vehicleSetting.TotalHealth;
        internal int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
        internal bool IsVehicleReady { get { return isVehicleReady; } set { isVehicleReady = value; } }
        internal int OnHitHealthDecrease => onHitHealthDecrease;
        internal int VehicleID => VehicleConfig.vehicleDatum.ID;
        internal PoolObjectType ObjectPoolType => poolObjectType;

        internal virtual void Init(GameplayHelper gameplayHelper, EssentialHelperData essentialHelperData)
        {
            _gameplayHelper = gameplayHelper;
            _essentialHelperData = essentialHelperData;
            InitializeVehicleData();

        }

        internal virtual void Update()
        {
            if (!isVehicleReady)
                return;

            MoveForward();
            LookAtTarget();
            UpdateWheelsVisualRotation();
        }
        private void MoveForward()
        {
            transform.Translate(Vector3.forward * VehicleConfig.vehicleSetting.MoveSpeed * Time.deltaTime);
        }
        private void UpdateWheelsVisualRotation()
        {
            foreach (Transform wheel in wheels)
                wheel.transform.Rotate(Vector3.right * VehicleConfig.vehicleSetting.WheelRotSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Function only for Cop vehicle
        /// </summary>
        internal virtual void LookAtTarget()
        {

        }
        private void InitializeVehicleData()
        {
            currentHealth = TotalHealth;
        }
        internal void ExplodeVehicle()
        {
            GameObject explosion = EssentialHelperData.AccessData<ObjectPooling>().GetObjectFromPool(PoolObjectType.EXPLOSION);
            explosion.transform.position = transform.position;
            ParticleHandler particleHandler = explosion.GetComponent<ParticleHandler>();
            particleHandler.Init(EssentialHelperData);
        }

        internal abstract void OnCollisionEnter(Collision collision);
        internal abstract void Cleanup();
    }
}
