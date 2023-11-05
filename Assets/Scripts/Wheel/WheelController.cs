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
        [SerializeField] private WheelItem[] _normalZoneSliceContents;
        [SerializeField] private WheelItem[] _safeZoneSliceContents;
        [SerializeField] private WheelItem[] _superZoneSliceContents;

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
        private void SetWheelSliceContents(WheelItem[] items)
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < _normalZoneSliceContents.Length && _normalZoneSliceContents[i] != null)
                    _sliceControllers[i].SetContent(_normalZoneSliceContents[i]);
            }
        }
        private void PlayIndicatorStartAnim()
        {
            Sequence indicatorStartSequence = DOTween.Sequence();
            int loop = (int)(_settings.AnimSpinStartTime / _settings.AnimIndicatorStartTime);
            indicatorStartSequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorStartDegreeZ), _settings.AnimIndicatorStartTime / 2));
            indicatorStartSequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorStartDegreeZ), _settings.AnimIndicatorStartTime / 2));
            indicatorStartSequence.SetLoops(loop);
        }
        private void PlayIndicatorAnim()
        {
            Sequence indicatorSequence = DOTween.Sequence();
            float totalTime = _settings.AnimSpinTime * _settings.AnimSpinLoopCount;
            int loop = (int)(totalTime / _settings.AnimIndicatorRotateTimePerLoop);
            indicatorSequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorDegreeZ), _settings.AnimIndicatorRotateTimePerLoop).SetLoops(loop));
        }
        private void PlayIndicatorStopAnim(float stopTime)
        {
            Sequence indicatorStartSequence = DOTween.Sequence();
            int loop = (int)((stopTime - _settings.AnimIndicatorEndWaitTime) / _settings.AnimIndicatorEndTime);
            indicatorStartSequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, -_settings.AnimIndicatorEndDegreeZ), _settings.AnimIndicatorEndTime / 2));
            indicatorStartSequence.Append(_rectSpinIndicator.DOLocalRotate(new Vector3(0, 0, _settings.AnimIndicatorEndDegreeZ), _settings.AnimIndicatorEndTime / 2));
            indicatorStartSequence.SetLoops(loop);
            indicatorStartSequence.OnComplete(() => _rectSpinIndicator.DOLocalRotate(Vector3.zero, _settings.AnimIndicatorEndTime / 4));
        }
        public void HandleOnZoneChanged(ZonesPanelController.ZoneType zoneType)
        {
            WheelItem[] items;
            if (zoneType == ZonesPanelController.ZoneType.Normal)
                items = _normalZoneSliceContents;
            else if (zoneType == ZonesPanelController.ZoneType.Safe)
                items = _safeZoneSliceContents;
            else
                items = _superZoneSliceContents;

            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < items.Length && items[i] != null)
                    _sliceControllers[i].SetContent(items[i]);
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
            spinSequence.AppendCallback(() => PlayIndicatorStopAnim(spinEndTime));

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
