using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;
using WheelOfFortune.Panels;
using WheelOfFortune.Wheel;

namespace WheelOfFortune.Managers
{
    public class SpinManager : MonoBehaviour
    {
        [Header("Temp")]
        [SerializeField] private Image _vfxImage;
        [Header("References")]
        [SerializeField] private SpinPanelController _spinPanelController;
        [SerializeField] private ContentPanelController _contentPanelController;
        [SerializeField] private RewardsPanelController _rewardsPanelController;
        [SerializeField] private WheelController _wheelController;

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

            await _contentPanelController.HideContentAnimation();
            await _spinPanelController.ShowSpinPanelAnimation();

        }
    }
}
