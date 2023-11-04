using System;
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
        private void OnEnable()
        {
            _bombPanelController.OnGiveUpButtonClick += HandleOnGiveUpBtnClk;
            _bombPanelController.OnEnter += HandleOnExitPanelEnter;
            _bombPanelController.OnReviveButtonClick += HandleOnRevBtnClk;
        }
        private void HandleOnExitPanelEnter()
        {
            int reviveGoldCost = _settings.ReviveGoldCost;
            bool enableReviveBtn = _rewardsPanelController.IsGoldEnough(reviveGoldCost);
            _bombPanelController.UpdateReviveBtn(enableReviveBtn, reviveGoldCost);
        }
        private void HandleOnGiveUpBtnClk()
        {
            RestartGame();
        }
        [ContextMenu("Restart Game")]
        private void RestartGame()
        {
            _bombPanelController.ResetPanel();
            _rewardsPanelController.ResetRewards();
            _zonesPanelController.ResetZones();
        }
        private void HandleOnRevBtnClk()
        {
            _bombPanelController.ResetPanel();
            _rewardsPanelController.HandleOnRevived(_settings.ReviveGoldCost);
        }
    }
}
