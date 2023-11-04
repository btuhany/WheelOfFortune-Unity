using Cysharp.Threading.Tasks;
using UnityEngine;
using WheelOfFortune.Panels;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private BombPanelController _bombPanelController;
        [SerializeField] private RewardsPanelController _rewardsPanelController;
        [SerializeField] private ZonesPanelController _zonesPanelController;
        [SerializeField] private ExitPanelController _exitPanelController;
        [SerializeField] private SpinPanelController _spinPanelController;
        private void OnEnable()
        {
            _bombPanelController.OnBtnClkGiveUp += HandleOnGiveUp;
            _bombPanelController.OnPanelEnter += HandleOnPanelEnterBomb;
            _bombPanelController.OnBtnClkRevive += HandleOnRevive;
            _rewardsPanelController.OnButtonClickExit += HandleOnBtnClkExit;
            _exitPanelController.OnBtnClkExitYes += HandleOnExitYes;
            _exitPanelController.OnBtnClkExitNo += HandleOnExitNo;
            _zonesPanelController.OnZoneChangedEvent += HandleOnZoneChanged;
        }
        private void OnDisable()
        {
            _bombPanelController.OnBtnClkGiveUp -= HandleOnGiveUp;
            _bombPanelController.OnPanelEnter -= HandleOnPanelEnterBomb;
            _bombPanelController.OnBtnClkRevive -= HandleOnRevive;
            _rewardsPanelController.OnButtonClickExit -= HandleOnBtnClkExit;
            _exitPanelController.OnBtnClkExitYes -= HandleOnExitYes;
            _exitPanelController.OnBtnClkExitNo -= HandleOnExitNo;
            _zonesPanelController.OnZoneChangedEvent -= HandleOnZoneChanged;
        }
        private void Start()
        {
            _zonesPanelController.InvokeZoneChangeEvent();
        }
        private void RestartGame()
        {
            _rewardsPanelController.ResetRewards();
            _zonesPanelController.ResetZones();
            _spinPanelController.SetSpinButton(true);
        }
        private async void HandleOnBtnClkExit()
        {
            await _exitPanelController.PlayEnterBackground();
            await _rewardsPanelController.ShowEndRewards(_exitPanelController.RectTransform);
            await UniTask.WhenAll(_exitPanelController.PlayEnterQuestionButtons());
        }
        private void HandleOnPanelEnterBomb()
        {
            int reviveGoldCost = _settings.ReviveGoldCost;
            bool enableReviveBtn = _rewardsPanelController.IsGoldEnough(reviveGoldCost);
            _bombPanelController.SetButtonRevive(enableReviveBtn, reviveGoldCost);
        }
        private void HandleOnGiveUp()
        {
            _bombPanelController.ResetPanel();
            RestartGame();
        }
        private void HandleOnRevive()
        {
            _bombPanelController.ResetPanel();
            _rewardsPanelController.HandleOnRevived(_settings.ReviveGoldCost);
        }
        private async void HandleOnExitNo()
        {
            await _exitPanelController.ResetQuestionButtons();
            await _rewardsPanelController.UnshowEndRewards(_exitPanelController.RectTransform);
            await _exitPanelController.ResetBackground();
        }
        private void HandleOnExitYes()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
        private void HandleOnZoneChanged(ZonesPanelController.ZoneType newZone)
        {
            _spinPanelController.SetSprites(newZone);
        }
    }
}
