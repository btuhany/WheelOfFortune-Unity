using Cysharp.Threading.Tasks;
using UnityEngine;
using WheelOfFortune.Items;
using WheelOfFortune.Panels;
using WheelOfFortune.Wheel;

namespace WheelOfFortune.Managers
{
    public class SpinManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpinPanelController _spinPanelController;
        [SerializeField] private ContentPanelController _contentPanelController;
        [SerializeField] private RewardsPanelController _rewardsPanelController;
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private ZonesPanelController _zonesPanelController;
        [SerializeField] private BombPanelController _exitPanelController;

        #region Constants
        private const int _anglePerSlice = 45;
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleSpinProcess();
            }
        }

        //TODO: Try UniTaskVoid (fire and forget) instead of void.
        private async void HandleSpinProcess()
        {
            _rewardsPanelController.HideExitButton();
            WheelSliceController randomSlice = _wheelController.SelectRandomSlice();
            WheelItem randomItem = randomSlice.Content;
            await _wheelController.SpinToTargetSlice(randomSlice.SliceIndex * _anglePerSlice);

            if (randomItem.Type == WheelItem.ItemType.Reward)
                await HandleAtReward(randomItem);
            else if (randomItem.Type == WheelItem.ItemType.Bomb)
                await HandleAtBomb();
        }
        private async UniTask HandleAtReward(WheelItem randomItem)
        {
            await _spinPanelController.HideSpinPanelAnimation();
            await _contentPanelController.ShowContentAnimation(randomItem);

            await _rewardsPanelController.GetReward(randomItem, _contentPanelController.transform);

            _rewardsPanelController.UnhideExitButton();
            _zonesPanelController.ScrollZone(1);

            await _contentPanelController.HideContentAnimation();
            await _spinPanelController.ShowSpinPanelAnimation();
        }
        private async UniTask HandleAtBomb()
        {
            _rewardsPanelController.HideExitButton(true);
            await _exitPanelController.StartEnterAnim();
        }

    }
}
