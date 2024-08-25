using UnityEngine;

namespace RacerVsCops
{
    public class VehicleIdentifier : MonoBehaviour
    {
        [SerializeField] private VehicleType _vehicleType;

        internal VehicleType vehicleType => _vehicleType;
    }
}
