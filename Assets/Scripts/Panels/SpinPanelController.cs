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
        public event System.Action OnButtonClickedSpin;
        private ZonesPanelController.ZoneType _currentZoneType;

        public WheelController WheelController { get => _wheelController; }

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
            SetSpinButton(false);
            OnButtonClickedSpin?.Invoke();
        }
        public void SetSpinButton(bool isActive)
        {
            _buttonSpin.gameObject.SetActive(isActive);
        }
        public async UniTask HideSpinPanelAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.SpinPanelHidedScaleX,
                _settings.AnimHideSpinPanelTime)
                .SetEase(_settings.AnimHideSpinPanelEase).ToUniTask();
        }
        public async UniTask ShowSpinPanelAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.SpinPanelDefaultScaleX,
                _settings.AnimShowSpinPanelTime)
                .SetEase(_settings.AnimShowSpinPanelEase).ToUniTask();
            SetSpinButton(true);
        }
        public void SetSprites(ZonesPanelController.ZoneType zoneType)
        {
            if (_currentZoneType == zoneType) return;
            _currentZoneType = zoneType;
            _wheelController.HandleOnZoneChanged(zoneType);
            switch (zoneType)
            {
                case ZonesPanelController.ZoneType.Normal:
                    _imageWheelSpin.sprite = _settings.SpriteNormalZoneSpin;
                    _imageWheelIndicator.sprite = _settings.SpriteNormalZoneIndicator;
                    _textHeaderSpin.text = _settings.StringHeaderNormalZone;
                    break;
                case ZonesPanelController.ZoneType.Safe:
                    _imageWheelSpin.sprite = _settings.SpriteSafeZoneSpin;
                    _imageWheelIndicator.sprite = _settings.SpriteSafeZoneIndicator;
                    _textHeaderSpin.text = _settings.StringHeaderSafeZone;
                    break;
                case ZonesPanelController.ZoneType.Super:
                    _imageWheelSpin.sprite = _settings.SpriteSuperZoneSpin;
                    _imageWheelIndicator.sprite = _settings.SpriteSuperZoneIndicator;
                    _textHeaderSpin.text = _settings.StringHeaderSuperZone;
                    break;
                default:
                    break;
            }
        }
    }
}