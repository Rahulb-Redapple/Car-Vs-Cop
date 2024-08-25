using UnityEngine;

namespace RacerVsCops
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private ApplicationHandler _applicationHandler;
        [SerializeField] private PopupHandler _popupHandler;
        [SerializeField] private EssentialHelperData _essentialHelperData;
        [SerializeField] private EssentialConfigData _essentialConfigData;
        //[SerializeField] private InputHandler _inputHandler;
        //[SerializeField] private AudioHandler _audioHandler;

        private PlayerSaveData _playerSaveData;

        private void Awake()
        {
            _playerSaveData = PlayerDataHandler.LoadPlayerData();
            Application.targetFrameRate = 60;
            _essentialConfigData.Init();
            _essentialHelperData.Init();
            //_inputHandler.Init();
            _popupHandler.Init();
            //_audioHandler.Init();
            _applicationHandler.Init(_popupHandler, _essentialHelperData, _essentialConfigData);
        }

        private void SaveGame()
        {
            if (_playerSaveData.IsFirstTime)
            {
                _playerSaveData.SetIsFirstTime(false);
            }
            PlayerDataHandler.SavePlayerData();
        }

        private void OnDisable()
        {
            SaveGame();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGame();
            }
        }
    }
}
