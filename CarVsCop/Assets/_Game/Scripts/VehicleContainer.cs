using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class VehicleContainer : MonoBehaviour
    {
        private EssentialConfigData _essentialConfigData;
        private VehicleData _vehicleData;

        private List<Player> _carList = new List<Player>();

        internal void Init(EssentialConfigData essentialConfigData)
        {
            _essentialConfigData = essentialConfigData;
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();    
        }

        internal void AddVehicleToList(Player playerCar)
        {
            _carList.Add(playerCar);
        }

        internal List<Player> GetVehicleList()
        {
            return _carList;
        }
        internal void PopulateVehicles()
        {
            if (!Equals(_carList.Count, 0))
                return;

            else
            {
                for (int i = 0; i < _vehicleData.VehicleConfigs.Count; i++)
                {
                    Player player = Instantiate(_vehicleData.VehicleConfigs[i].vehicleDatum.VehiclePrefab, Vector3.zero, Quaternion.Euler(0, 135, 0)) as Player;
                    player.transform.SetParent(transform);
                    player.Rotator.Init(_essentialConfigData);
                    player.SetVisibility(false);
                    AddVehicleToList(player);
                }
            }
        }
    }
}
