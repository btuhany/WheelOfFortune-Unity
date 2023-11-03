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
            WheelSliceController randomSlice = _wheelController.SelectRandomSlice();
            WheelItem randomItem = randomSlice.Content;
            await _wheelController.SpinToTargetSlice(randomSlice.SliceIndex * _anglePerSlice);

            await _spinPanelController.HideSpinPanelAnimation();
            await _contentPanelController.ShowContentAnimation(randomItem);

            await _rewardsPanelController.GetReward(randomItem, _contentPanelController.transform);

            _zonesPanelController.ScrollZone(1);

            await _contentPanelController.HideContentAnimation();
            await _spinPanelController.ShowSpinPanelAnimation();

        }
    }
}
