using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class LoadingScreen : UiScreenBase
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text fillValueText;

        private float counter = 3;

        bool canCountdown = true;
        float count = 0;

        internal override void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            base.Init(popupHandler, essentialHelperData, essentialConfigData);
            _slider.value = 0;
            _slider.maxValue = counter;
        }

        IEnumerator StartLoader()
        {
            while (canCountdown)
            {
                yield return null;
                count += Time.deltaTime;

                _slider.value = count;
                fillValueText.text = ((int)_slider.value * 100).ToString();

                if (count >= (counter - (10f / 100f * counter)))
                {
                    _slider.value = counter;
                    canCountdown = false;
                }
            }
            yield return new WaitForSeconds(0.5f);
            GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.MENU, null});
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            StartCoroutine(StartLoader());
        }

        internal override void Cleanup()
        {
            _slider.value = 0;
        }
    }
}
