using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class GameplayScreen : UiScreenBase
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text copKilledText;

        private int currentCopsKilled = 0;
        private int playerTotalHealth = 0;
        private int playerCurrentHealth = 0;

        [SerializeField] private List<Image> healthList = new List<Image>();

        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _emptyHeart;

        [SerializeField] private List<WantedLevelItem> wantedLevelItems = new List<WantedLevelItem>();

        private int wantedLevel = 0;

        internal override void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            base.Init(popupHandler, essentialHelperData, essentialConfigData);
            GameHelper.Instance.StartListening(GameConstants.CurrentCopKilledCount, HandleCurrentCopsKilled);
            GameHelper.Instance.StartListening(GameConstants.CurrentScore, HandleCurrentScore);           
            GameHelper.Instance.StartListening(GameConstants.CurrentCoin, HandleCurrentCoin);
            GameHelper.Instance.StartListening(GameConstants.UpdatePlayerHealth, HandlePlayerHealth);
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            ResetData();
            HandleCurrentCoin(0);
            HandleCurrentScore(0);
            HandleCurrentCopsKilled(0);
            SetupHealthAtInitial();
        }

        private void HandleCurrentCopsKilled(object obj)
        {
            currentCopsKilled = (int)obj;
            copKilledText.text = currentCopsKilled.ToString();        
            
            if(currentCopsKilled == 0)
            {
                wantedLevelItems[wantedLevel].Init();
                wantedLevel++;
                GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.FIRST);
            }
            else if(currentCopsKilled == 10)
            {
                wantedLevelItems[wantedLevel].Init();
                wantedLevel++;
                GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.SECOND);
            }
            else if(currentCopsKilled == 20)
            {
                wantedLevelItems[wantedLevel].Init();
                wantedLevel++;
                GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.THIRD);
            }
            else if(currentCopsKilled == 30)
            {
                wantedLevelItems[wantedLevel].Init();
            }
        }

        private void SetupHealthAtInitial()
        {
            playerTotalHealth = GameConstants.CurrentVehicleConfig.vehicleSetting.TotalHealth;
            playerCurrentHealth = playerTotalHealth;

            for (int i=0; i< healthList.Count; i++)
            {
                if(i < playerTotalHealth)
                {
                    healthList[i].gameObject.SetActive(true);
                }
                else
                {
                    healthList[i].gameObject.SetActive(false);
                }
            }
        }

        private void HandleCurrentScore(object obj)
        {
            scoreText.text = ((int)obj).ToString();
        }
        
        private void HandleCurrentCoin(object obj)
        {
            coinText.text = ((int)obj).ToString();
        }

        private void HandlePlayerHealth(object obj)
        {
            playerCurrentHealth = ((int)obj);

            for (int i = 0; i < healthList.Count; i++)
            {
                if (i < playerCurrentHealth)
                {
                    healthList[i].sprite = _fullHeart;
                }
                else
                {
                    healthList[i].sprite = _emptyHeart;
                }
            }
        }

        private void ClearAllWantedLevels()
        {
            foreach(WantedLevelItem wantedLevelItem in wantedLevelItems)
            {
                wantedLevelItem.Cleanup();
            }
            wantedLevel = 0;
        }

        internal void ResetData()
        {
            currentCopsKilled = 0;
            ClearAllWantedLevels();
        }

        internal override void Cleanup()
        {
            GameHelper.Instance.StopListening(GameConstants.CurrentCoin, HandleCurrentCoin);
            GameHelper.Instance.StopListening(GameConstants.CurrentScore, HandleCurrentScore);
            GameHelper.Instance.StopListening(GameConstants.UpdatePlayerHealth, HandlePlayerHealth);
            GameHelper.Instance.StopListening(GameConstants.CurrentCopKilledCount, HandleCurrentCopsKilled);
        }
    }
}