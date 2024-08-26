using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private RectTransform _toggleHandleRectTransform;
        [SerializeField] private TMP_Text _OnText;
        [SerializeField] private TMP_Text _OffText;

        [Space]

        [SerializeField] private Vector2 _onPos;
        [SerializeField] private Vector2 _offPos = Vector2.zero;

        [SerializeField] private float _easeTime;
        [SerializeField] private Ease _easeType;

        internal void OnToggleValueChanged(bool state)
        {
            _toggle.isOn = state;
            //_toggleHandleRectTransform.DOAnchorPos(state ? _onPos : _offPos, _easeTime).SetEase(_easeType);
            //if (state)
            //{
            //    _OnText.gameObject.SetActive(true);
            //    _OffText.gameObject.SetActive(false);
            //}
            //else
            //{
            //    _OnText.gameObject.SetActive(false);
            //    _OffText.gameObject.SetActive(true);
            //}
        }
    }
}
