using System;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public abstract class EssentialHelper : MonoBehaviour { }
    public class EssentialHelperData : MonoBehaviour
    {
        [SerializeField] private List<EssentialHelper> _essentialDataList = new List<EssentialHelper>();

        private Dictionary<Type, EssentialHelper> _essentialDataCollection = new Dictionary<Type, EssentialHelper>();

        internal void Init()
        {
            for (int i = 0; i < _essentialDataList.Count; i++)
            {
                _essentialDataCollection.Add(_essentialDataList[i].GetType(), _essentialDataList[i]);
            }
        }

        internal T AccessData<T>() where T : EssentialHelper
        {
            if (_essentialDataCollection.ContainsKey(typeof(T)))
            {
                return (T)_essentialDataCollection[typeof(T)];
            }
            return default;
        }

        internal void Cleanup()
        {
            _essentialDataCollection?.Clear();
        }
    }
}
