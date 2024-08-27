using System;
using UnityEngine;

namespace RacerVsCops
{
    [CreateAssetMenu(fileName = nameof(VehicleConfig), menuName = "Scriptable Objects/" + nameof(VehicleConfig))]
    public class VehicleConfig : ScriptableObject
    {
        [SerializeField] private VehicleSetting _vehicleSetting;
        internal VehicleSetting vehicleSetting => _vehicleSetting;
        [SerializeField] private VehicleDatum _vehicleDatum;
        internal VehicleDatum vehicleDatum => _vehicleDatum;


        [Serializable]
        public class VehicleSetting
        {
            [SerializeField] private float _moveSpeed;
            [SerializeField] private float _turnSpeed;
            [SerializeField] private float _wheelRotSpeed;
            [Range(1, 4)]
            [SerializeField] private int _totalHealth;

            internal float MoveSpeed => _moveSpeed;
            internal float TurnSpeed => _turnSpeed;
            internal float WheelRotSpeed => _wheelRotSpeed;
            internal int TotalHealth => _totalHealth;
        }

        [Serializable]
        public class VehicleDatum
        {
            [SerializeField] private int _id;
            [SerializeField] private VehicleBase _vehiclePrefab;
            [SerializeField] private int _vehiclePrice;
            [SerializeField] private string _vehicleName;
            [SerializeField] private Sprite _vehicleIcon;
            [SerializeField] private VehicleCategory _vehicleCategory;

            internal int ID => _id;
            internal VehicleBase VehiclePrefab => _vehiclePrefab;
            internal int VehiclePrice => _vehiclePrice;
            internal string VehicleName => _vehicleName;
            internal Sprite vehicleIcon => _vehicleIcon;
            internal VehicleCategory VehicleCategory => _vehicleCategory;
        }
    }
}
