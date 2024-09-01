using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RacerVsCops
{
    [CreateAssetMenu(fileName = nameof(VehicleData), menuName = "Scriptable Objects/" + nameof(VehicleData))]
    public class VehicleData : EssentialConfigScriptableObject
    {
        [SerializeField] private List<VehicleConfig> vehicleConfigs;

        internal ReadOnlyCollection<VehicleConfig> VehicleConfigs => vehicleConfigs.AsReadOnly();

        internal VehicleConfig GetVehicleConfig(int vehicleID)
        {
            foreach (VehicleConfig vehicleConfig in vehicleConfigs)
            {
                if(vehicleConfig.vehicleDatum.ID == vehicleID)
                {
                    return vehicleConfig;
                }
            }
            return default;
        }
    }
}
