using System;
using UnityEngine;
using WheelOfFortune.Panels;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSettings _settings;
        [SerializeField] private BombPanelController _exitPanelController;
        [SerializeField] private RewardsPanelController _rewardsPanelController;
        [SerializeField] private ZonesPanelController _zonesPanelController;
        private void OnEnable()
        {
            _exitPanelController.OnGiveUpButtonClick += HandleOnGiveUpBtnClk;
            _exitPanelController.OnEnter += HandleOnExitPanelEnter;
            _exitPanelController.OnReviveButtonClick += HandleOnRevBtnClk;
        }
        private void HandleOnExitPanelEnter()
        {
            int reviveGoldCost = _settings.ReviveGoldCost;
            bool enableReviveBtn = _rewardsPanelController.IsGoldEnough(reviveGoldCost);
            _exitPanelController.UpdateReviveBtn(enableReviveBtn, reviveGoldCost);
        }
        private void HandleOnGiveUpBtnClk()
        {
            RestartGame();
        }
        [ContextMenu("Restart Game")]
        private void RestartGame()
        {
            _exitPanelController.ResetPanel();
            _rewardsPanelController.ResetRewards();
            _zonesPanelController.ResetZones();
        }
        private void HandleOnRevBtnClk()
        {
            _exitPanelController.ResetPanel();
            _rewardsPanelController.HandleOnRevived(_settings.ReviveGoldCost);
        }
    }
}
