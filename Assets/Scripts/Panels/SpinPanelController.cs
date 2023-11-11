using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Settings;
using WheelOfFortune.Wheel;

namespace WheelOfFortune.Panels
{
    public class SpinPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpinPanelSettings _settings;

        [Header("References")]
        [SerializeField] private Image _imageWheelSpin;
        [SerializeField] private Image _imageWheelIndicator;
        [SerializeField] private TextMeshProUGUI _textHeaderSpin;
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _buttonSpin;

        private ZonesPanelController.ZoneType _currentZoneType;

        public WheelController WheelController { get => _wheelController; }

        public event System.Action OnButtonClickedSpin;

        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_buttonSpin == null)
                _buttonSpin = GetComponentInChildren<Button>();
        }
        private void Awake()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_buttonSpin == null)
                _buttonSpin = GetComponentInChildren<Button>();
            _buttonSpin.onClick.AddListener(HandleOnButtonSpin);
        }
        private void HandleOnButtonSpin()
        {
            SpinButtonSet(false);
            OnButtonClickedSpin?.Invoke();
        }
        private void SetWheelPanelZoneType(SpinPanelSettings.SpinPanelAppearance spinPanel)
        {
            _imageWheelSpin.sprite = spinPanel.spriteSpin;
            _imageWheelIndicator.sprite = spinPanel.spriteIndicator;
            _textHeaderSpin.text = spinPanel.textHeader;
            _textHeaderSpin.color = spinPanel.textHeaderColor;
        }
        public void SpinButtonSet(bool isActive)
        {
            _buttonSpin.gameObject.SetActive(isActive);
        }
        public async UniTask HideSpinPanelAnimation()
        {
            await UniTask.Delay(_settings.HidePanelMillisecondsDelay);
            await _rectTransform.DOScaleX(
                _settings.HidePanelAnim.value,
                _settings.HidePanelAnim.time)
                .SetEase(_settings.HidePanelAnim.ease).ToUniTask();
        }
        public async UniTask ShowSpinPanelAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.ShowPanelAnim.value,
                _settings.ShowPanelAnim.time)
                .SetEase(_settings.ShowPanelAnim.ease).ToUniTask();
            SpinButtonSet(true);
        }
        public void HandleOnZoneChanged(ZonesPanelController.ZoneType zoneType)
        {
            if (_currentZoneType == zoneType) return;
            _currentZoneType = zoneType;
            _wheelController.HandleOnZoneChanged(zoneType);
            switch (zoneType)
            {
                case ZonesPanelController.ZoneType.Normal:
                    SetWheelPanelZoneType(_settings.NormalZoneAppear);
                    break;
                case ZonesPanelController.ZoneType.Safe:
                    SetWheelPanelZoneType(_settings.SafeZoneAppear);
                    break;
                case ZonesPanelController.ZoneType.Super:
                    SetWheelPanelZoneType(_settings.SuperZoneAppear);
                    break;
                default:
                    break;
            }
        }
    }
}
