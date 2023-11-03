using UnityEngine;
using WheelOfFortune.Panels;

namespace WheelOfFortune.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ExitPanelController _exitPanelController;
        [SerializeField] private RewardsPanelController _rewardsPanelController;
        [SerializeField] private ZonesPanelController _zonesPanelController;
        private void OnEnable()
        {
            _exitPanelController.OnGiveUpButtonClick += HandleOnGiveUpBtnClk;
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
    }
}
