using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class MaterialHandler : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshRenderersList = new List<MeshRenderer>();

        private Material _material; 

        internal void SetMaterial(Material material)
        {
            _meshRenderersList.ForEach(r => r.material = material); 
        }
    }
}
