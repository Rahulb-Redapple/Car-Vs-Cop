using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RacerVsCops
{
    [CreateAssetMenu(fileName = nameof(VehicleColorConfig), menuName = "Scriptable Objects/" + nameof(VehicleColorConfig))]
    public class VehicleColorConfig : EssentialConfigScriptableObject
    {
        [SerializeField] private List<ColorConfig> _colorConfigList = new List<ColorConfig>();

        internal ReadOnlyCollection<ColorConfig> ColorConfigsList => _colorConfigList.AsReadOnly();
    }

    [Serializable]
    public class ColorConfig
    {
        [SerializeField] private VehicleCategory _vehicleCategory;
        [SerializeField] private List<Material> _materialsList = new List<Material>();

        internal VehicleCategory VehicleCategory => _vehicleCategory;
        internal ReadOnlyCollection<Material> MaterialsList => _materialsList.AsReadOnly();
    }
}
