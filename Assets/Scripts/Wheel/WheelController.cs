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
        private const float _wheelRadius = 140f;
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
            Sequence sequence = DOTween.Sequence();
            int loop = (int)(_settings.AnimSpinStartTime / (_settings.AnimIndicatorStartSwingTime * 2));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorStartSwingAngle), _settings.AnimIndicatorStartSwingTime));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorStartSwingAngle), _settings.AnimIndicatorStartSwingTime));
            sequence.SetLoops(loop);
        }
        private void PlayIndicatorAnim()
        {
            float totalTime = _settings.AnimSpinTime * _settings.AnimSpinLoopCount;
            int loopCount = (int)(totalTime / _settings.AnimIndicatorSwingTimePerLoop);
            _rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorSwingAngle), _settings.AnimIndicatorSwingTimePerLoop).SetLoops(loopCount);
        }
        private void PlayIndicatorStopAnim()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorEndSwingAngle), _settings.AnimIndicatorEndSwingTime));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorEndSwingAngle), _settings.AnimIndicatorEndSwingTime));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(Vector3.zero, _settings.AnimIndicatorEndSwingTime)
                .SetEase(_settings.AnimIndicatorStopEase));
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

            //Change with bombs
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
        public Sequence SpinToTargetSlice(int targetAngle)
        {
            float angleDifference = targetAngle - _rectTransform.rotation.eulerAngles.z;
            Sequence spinSequence = DOTween.Sequence();

            spinSequence.AppendCallback(PlayIndicatorStartAnim);

            //Spin start animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, _settings.AnimSpinStartDegree),
            _settings.AnimSpinStartTime, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(_settings.AnimSpinStartEase));

            spinSequence.AppendCallback(PlayIndicatorAnim);

            //Spin animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, _settings.AnimSpinDegree),
            _settings.AnimSpinTime, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(_settings.AnimSpinEase)
            .SetLoops(_settings.AnimSpinLoopCount));


            float spinEndTime = _settings.AnimSpinEndTime * ((_fullRotationDegree - angleDifference) / _fullRotationDegree);
            spinSequence.AppendCallback(PlayIndicatorStopAnim);

            //Spin end animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, targetAngle - _fullRotationDegree),
            spinEndTime, RotateMode.FastBeyond360)
            .SetRelative(false)
            .SetEase(_settings.AnimSpinEndEase));

            return spinSequence;
        }
    }
}
