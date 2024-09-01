using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace RacerVsCops
{
    [CreateAssetMenu(fileName = nameof(VehicleColorConfig), menuName = "Scriptable Objects/" + nameof(VehicleColorConfig))]
    public class VehicleColorConfig : EssentialConfigScriptableObject
    {
        [SerializeField] private List<ColorConfig> _colorConfigList = new List<ColorConfig>();

        internal ReadOnlyCollection<ColorConfig> ColorConfigsList => _colorConfigList.AsReadOnly();

        internal List<ColorConfig.ColorData> GetColorData(VehicleCategory vehicleCategory)
        {
            foreach (ColorConfig colorConfig in _colorConfigList)
            {
                if(colorConfig.VehicleCategory == vehicleCategory)
                {
                    return colorConfig.ColorDataList.ToList();
                }
            }
            return null;
        }
    }

    [Serializable]
    public class ColorConfig
    {
        [SerializeField] private VehicleCategory _vehicleCategory;
        [SerializeField] private List<ColorData> _materialsList = new List<ColorData>();

        internal VehicleCategory VehicleCategory => _vehicleCategory;
        internal ReadOnlyCollection<ColorData> ColorDataList => _materialsList.AsReadOnly();

        [Serializable]
        public class ColorData
        {
            [SerializeField] private Material _material;
            [SerializeField] private Color _color;
            [SerializeField] private string _materialCode;
            [SerializeField] private int _price;

            internal Material Material => _material;
            internal Color Color => _color;
            internal string MaterialCode => _materialCode;  
            internal int Price => _price;   
        }
    }
}
