using DG.Tweening;
using UnityEngine;
using WheelOfFortune.Items;
using WheelOfFortune.Settings;
using WheelOfFortune.Panels;
using UnityEngine.UI;
using static WheelOfFortune.Panels.ZonesPanelController;

namespace WheelOfFortune.Wheel
{
    public class WheelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WheelSettings _settings;

        [Header("References")]
        [SerializeField] private RectTransform _rectSpinIndicator;

        [Header("Wheel Slice Contents")]
        [SerializeField] private WheelItemGroup _normalZoneSliceContents;
        [SerializeField] private WheelItemGroup _safeZoneSliceContents;
        [SerializeField] private WheelItemGroup _superZoneSliceContents;
        [SerializeField] private WheelItemGroup _tierOneSliceContents;
        [SerializeField] private WheelItemGroup _tierTwoSliceContents;
        [SerializeField] private WheelItemGroup _tierThreeSliceContents;

        #region Component References
        [SerializeField] private RectTransform _rectTransform;
        #endregion

        #region Constants
        private const int _fullRotationDegree = 360;
        #endregion

        private WheelSliceController[] _sliceControllers = new WheelSliceController[8];

        [ContextMenu("Update Slice Normal Zone Contents")]
        private void UpdateSliceContentsNormalZone()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
            SetWheelSliceContents(_normalZoneSliceContents);
        }
        [ContextMenu("Update Slice Safe Zone Contents")]
        private void UpdateSliceContentsSafeZone()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
            SetWheelSliceContents(_safeZoneSliceContents);
        }
        [ContextMenu("Update Slice Super Zone Contents")]
        private void UpdateSliceContentsSuperZone()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
            SetWheelSliceContents(_superZoneSliceContents);
        }
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        private void Awake()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
        }
        private void SetWheelSliceContents(WheelItemGroup itemGroup)
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < itemGroup.Items.Length && itemGroup.Items[i] != null)
                {
                    _sliceControllers[i].SetContent(itemGroup.Items[i]);
                    SetSameContentCountsEqual(itemGroup.Items[i]);
                }
            }
        }
        private void SetWheelSliceContents(WheelItem[] items)
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < items.Length && items[i] != null)
                {
                    _sliceControllers[i].SetContent(items[i]);
                    SetSameContentCountsEqual(items[i]);
                }
            }
        }
        private void SetSameContentCountsEqual(WheelItem item)
        {
            for (int j = 0; j < 8; j++)
            {
                if (item == _sliceControllers[j].Content)
                {
                    _sliceControllers[j].SetContent(item, false);
                }
            }
        }
        private void PlayIndicatorStartAnim()
        {
            WheelSettings.RotateAnim indicatorAnim = _settings.indicatorStartAnim;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -indicatorAnim.angle), indicatorAnim.tween.time));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, indicatorAnim.angle), indicatorAnim.tween.time));
            sequence.SetEase(indicatorAnim.tween.ease);
            int loop = (int)(_settings.spinStartAnim.tween.time / (indicatorAnim.tween.time * 2));
            sequence.SetLoops(loop);
        }
        private void PlayIndicatorAnim()
        {
            float totalTime = _settings.spinLoopAnim.tween.time * _settings.SpinAnimLoop;
            int loopCount = (int)(totalTime / _settings.IndicatorAnimTimePerLoop);
            _rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.indicatorLoopAnim.angle),
                _settings.indicatorLoopAnim.tween.time)
                .SetEase(_settings.indicatorLoopAnim.tween.ease).SetLoops(loopCount);
        }
        private void PlayIndicatorStopAnim()
        {
            WheelSettings.RotateAnim indicatorAnim = _settings.indicatorEndAnim;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -indicatorAnim.angle), indicatorAnim.tween.time));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, indicatorAnim.angle), indicatorAnim.tween.time));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(Vector3.zero, indicatorAnim.tween.time)
                .SetEase(indicatorAnim.tween.ease));
        }
        public void HandleOnZoneChanged(ZonesPanelController.ZoneType zoneType)
        {
            WheelItemGroup itemGroup;
            if (zoneType == ZonesPanelController.ZoneType.Normal)
                itemGroup = _normalZoneSliceContents;
            else if (zoneType == ZonesPanelController.ZoneType.Safe)
                itemGroup = _safeZoneSliceContents;
            else
                itemGroup = _superZoneSliceContents;

            SetWheelSliceContents(itemGroup);
        }
        public void RandomizeItemsWithTiers(ItemTier tier)
        {
            int bombCount = 0;
            WheelItemGroup itemGroup;
            if (tier == ItemTier.One)
            {
                bombCount = Random.Range(_settings.BombCountTierOneMin, _settings.BombCountTierOneMax);
                itemGroup = _tierOneSliceContents;
            }
            else if (tier == ItemTier.Two)
            {
                bombCount = Random.Range(_settings.BombCountTierTwoMin, _settings.BombCountTierTwoMax);
                itemGroup = _tierTwoSliceContents;
            }
            else
            {
                bombCount = Random.Range(_settings.BombCountTierThreeMin, _settings.BombCountTierThreeMax);
                itemGroup = _tierThreeSliceContents;
            }


            WheelItem[] randomItemGroup = new WheelItem[8];
            for (int i = 0; i < 8; i++)
            {
                randomItemGroup[i] = itemGroup.Items[Random.Range(0, itemGroup.Items.Length)];
            }
            SetWheelSliceContents(randomItemGroup);

            //Add&Change with bombs
            for (int i = 0; i < bombCount; i++)
            {
                WheelSliceController randomSlice = _sliceControllers[Random.Range(0, _sliceControllers.Length)];
                randomSlice.SetContent(_settings.BombItem);
            }
        }
        public WheelSliceController SelectRandomSlice()
        {
            WheelSliceController randomSlice = _sliceControllers[Random.Range(0, _sliceControllers.Length)];
            return randomSlice;
        }
        public Sequence SpinToTargetSlice(int targetSliceIndex)
        {
            float targetAngle = targetSliceIndex * _settings.AnglePerSlice;
            float angleDifference = targetAngle - _rectTransform.rotation.eulerAngles.z;
            Sequence spinSequence = DOTween.Sequence();

            spinSequence.AppendCallback(PlayIndicatorStartAnim);

            WheelSettings.RotateAnim startSpin = _settings.spinStartAnim;
            WheelSettings.RotateAnim loopSpin = _settings.spinLoopAnim;
            WheelSettings.RotateAnim endSpin = _settings.spinEndAnim;

            //Spin start animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate(new Vector3(0f, 0f, startSpin.angle),
            startSpin.tween.time, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(startSpin.tween.ease));

            spinSequence.AppendCallback(PlayIndicatorAnim);

            //Spin animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate(new Vector3(0f, 0f, loopSpin.angle),
            loopSpin.tween.time, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(loopSpin.tween.ease)
            .SetLoops(_settings.SpinAnimLoop));


            float spinEndTime = endSpin.tween.time * ((_fullRotationDegree - angleDifference) / _fullRotationDegree);
            spinSequence.AppendCallback(PlayIndicatorStopAnim);

            //Spin end animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate(new Vector3(0f, 0f, targetAngle - _fullRotationDegree),
            spinEndTime, RotateMode.FastBeyond360)
            .SetRelative(false)
            .SetEase(endSpin.tween.ease));

            return spinSequence;
        }
    }
}
