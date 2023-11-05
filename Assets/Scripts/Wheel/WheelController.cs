using DG.Tweening;
using UnityEngine;
using WheelOfFortune.Items;
using WheelOfFortune.Settings;
using WheelOfFortune.Panels;
using UnityEngine.UI;

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
                    _sliceControllers[i].SetContent(itemGroup.Items[i]);
            }
        }
        private void PlayIndicatorStartAnim()
        {
            Sequence sequence = DOTween.Sequence();
            int loop = (int)(_settings.AnimSpinStartTime / _settings.AnimIndicatorStartTime);
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorStartDegreeZ), _settings.AnimIndicatorStartTime / 2));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorStartDegreeZ), _settings.AnimIndicatorStartTime / 2));
            sequence.SetLoops(loop);
        }
        private void PlayIndicatorAnim()
        {
            Sequence sequence = DOTween.Sequence();
            float totalTime = _settings.AnimSpinTime * _settings.AnimSpinLoopCount;
            int loopCount = (int)(totalTime / _settings.AnimIndicatorRotateTimePerLoop);
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorDegreeZ), _settings.AnimIndicatorRotateTimePerLoop).SetLoops(loopCount));
        }
        private void PlayIndicatorStopAnim()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorEndDegreeZ), _settings.AnimIndicatorEndTime / 2));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorEndDegreeZ), _settings.AnimIndicatorEndTime / 2));
            sequence.Append(_rectSpinIndicator.DOLocalRotate(Vector3.zero, _settings.AnimIndicatorEndTime)
                .SetEase(_settings.AnimIndicatorStopEase));
        }
        public void RandomizeSliceContents()
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                WheelItem randomItem = _tierOneSliceContents.Items[Random.Range(0, _tierOneSliceContents.Items.Length)];
                _sliceControllers[i].SetContent(randomItem);
            }
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

            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < itemGroup.Items.Length && itemGroup.Items[i] != null)
                    _sliceControllers[i].SetContent(itemGroup.Items[i]);
            }
        }
        public void TryRandomizeItemsCounts()
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].TryRandomizeContentCount();
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
