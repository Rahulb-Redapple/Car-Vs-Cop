using SimpleObjectPoolingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class ColorItem : ObjectPoolBase
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;

        private ColorConfig.ColorData _colorData;

        private Action<Material> _onClickAction;

        internal void SetColorData(ColorConfig.ColorData colorData, Action<Material> onClickAction)
        {
            _colorData = colorData;
            _onClickAction = onClickAction;
            SetButtonData(_colorData.Color);
            SetVisibility(true);
        }  

        private void SetButtonData(Color color)
        {
            _buttonImage.color = color;
        }

        public void OnClick()
        {
            _onClickAction?.Invoke(_colorData.Material);
        }

        internal override void Cleanup()
        {
            _onClickAction = null;
            _objectPooling.ReturnObjectToPool(this, PoolObjectType.COLOR_ITEM);
        }
    }
}
