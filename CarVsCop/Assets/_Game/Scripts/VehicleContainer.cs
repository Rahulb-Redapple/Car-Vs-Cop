using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RacerVsCops
{
    public class VehicleContainer : MonoBehaviour
    {
        private EssentialConfigData _essentialConfigData;
        private VehicleData _vehicleData;

        private List<Player> _populatedCarList = new List<Player>();

        internal void Init(EssentialConfigData essentialConfigData)
        {
            _essentialConfigData = essentialConfigData;
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();    
        }

        internal void AddVehicleToList(Player playerCar)
        {
            _populatedCarList.Add(playerCar);
        }

        internal List<Player> GetVehicleList()
        {
            return _populatedCarList;
        }

        internal List<VehicleConfig> GetAllVehicleConfigs()
        {
            return _vehicleData.VehicleConfigs.ToList();    
        }

        internal void PopulateVehicles(List<VehicleConfig> vehicleConfigsList)
        {
            for (int i = 0; i < vehicleConfigsList.Count; i++)
            {
                Player player = Instantiate(vehicleConfigsList[i].vehicleDatum.VehiclePrefab, Vector3.zero, Quaternion.Euler(0, 135, 0)) as Player;
                player.transform.SetParent(transform);
                player.Rotator.Init(_essentialConfigData);
                player.SetVisibility(false);
                AddVehicleToList(player);
            }
        }

        internal void Cleanup()
        {
            if (!Equals(_populatedCarList.Count, 0))
            {
                foreach (Player player in _populatedCarList)
                {
                    player.DestroyPlayer();
                }
                _populatedCarList.Clear();
            }
        }
    }
}
