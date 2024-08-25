using System;
using UnityEngine;

namespace RacerVsCops
{
    public class GameController : MonoBehaviour, IController
    {
        [SerializeField] private CameraHelper _cameraHelper;
        [SerializeField] private PlayerHelper _playerHelper;
        [SerializeField] private CopSpawnHelper _copSpawnHelper;
        [SerializeField] private GameplayHelper _gameplayHelper;
        [SerializeField] private GroundGridHelper _groundGridHelper;
        [SerializeField] private GlobalVolumeHelper _globalVolumeHelper;
        [SerializeField] private AudioManager _audioManager;

        private PopupHandler _popupHandler;
        private EssentialHelperData _essentialHelperData;
        private EssentialConfigData _essentialConfigData;

        private Action<GameStates, object> _stateChanged;

        public void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData, Action<GameStates, object> stateChanged)
        {
            _popupHandler = popupHandler;
            _essentialHelperData = essentialHelperData;
            _essentialConfigData = essentialConfigData;
            _stateChanged = stateChanged;
            _audioManager.InitAudio();
            _globalVolumeHelper.Init();
            _gameplayHelper.Init(_popupHandler, _copSpawnHelper);
            _copSpawnHelper.Init(_gameplayHelper, _essentialHelperData);
            _cameraHelper.Init(_essentialConfigData);
            _groundGridHelper.Init(_essentialHelperData);
        }

        public void ChangeGameState(GameStates newGameState, object data = null)
        {
            switch(newGameState)
            {
                case GameStates.MENU:
                    _cameraHelper.Cleanup();
                    _copSpawnHelper.Cleanup();
                    _groundGridHelper.Cleanup();
                    break;

                case GameStates.GAMEPLAY:
                    _playerHelper.Init(_gameplayHelper, _copSpawnHelper, _cameraHelper, _groundGridHelper, _essentialHelperData, _essentialConfigData);
                    _copSpawnHelper.Init();
                    _playerHelper.ReadyToPlay();
                    _cameraHelper.ReadyToFollow(true);
                    _gameplayHelper.InitiateGameplay();
                    _globalVolumeHelper.ReadyToPlayEffect();
                    _groundGridHelper.ReadyToGenerateGrid(true);
                    break;

                case GameStates.SHOP:
                    break;

                case GameStates.RESULT:
                    _cameraHelper.ReadyToFollow(false);
                    _groundGridHelper.ReadyToGenerateGrid(false);
                    _gameplayHelper.DetermineResult(data as string);
                    _playerHelper.Cleanup();
                    _globalVolumeHelper.Cleanup();
                    _gameplayHelper.Cleanup();
                    break;
            }
        }      
        
        private void CleanupGameplayData()
        {

        }

        public void RegisterInputs()
        {
            
        }

        public void UnregisterInputs()
        {
            
        }

        public void Cleanup()
        {

        }
    }
}
