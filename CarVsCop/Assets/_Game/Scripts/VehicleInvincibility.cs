using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class VehicleInvincibility : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private float _invincibleDuration = 3f;

        private Coroutine _blinkingEffectCoroutine;

        private bool _canInvincible = false;

        private float _currInvincibleDuration;

        internal void Init()
        {
            _canInvincible = true;
            _currInvincibleDuration = _invincibleDuration;
            _blinkingEffectCoroutine = StartCoroutine(Invincibility());
        }

        private IEnumerator Invincibility()
        {
            while(_canInvincible)
            {
                if (_currInvincibleDuration > 0)
                {
                    _currInvincibleDuration -= Time.deltaTime;
                    _collider.isTrigger = true;
                }
                else
                {  
                    _collider.isTrigger = false;                    
                    Cleanup();
                }
                yield return null;
            }           
        }

        internal void Cleanup()
        {
            if(!Equals(_blinkingEffectCoroutine, null))
            {
                StopCoroutine(_blinkingEffectCoroutine);
            }
            _canInvincible = false;
            _currInvincibleDuration = _invincibleDuration;
        }
    }
}
