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
        private void SetWheelPanelZoneType(Sprite spinSprite, Sprite indicatorSprite, string zoneHeader, Color zoneHeaderCol)
        {
            _imageWheelSpin.sprite = spinSprite;
            _imageWheelIndicator.sprite = indicatorSprite;
            _textHeaderSpin.text = zoneHeader;
            _textHeaderSpin.color = zoneHeaderCol;
        }
        public void SpinButtonSet(bool isActive)
        {
            _buttonSpin.gameObject.SetActive(isActive);
        }
        public async UniTask HideSpinPanelAnimation()
        {
            await UniTask.Delay(_settings.DelayHidePanel);
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
                    SetWheelPanelZoneType(_settings.SpriteNormalZoneSpin, _settings.SpriteNormalZoneIndicator,
                        _settings.StringHeaderNormalZone, _settings.ColorTextNormalZone);
                    break;
                case ZonesPanelController.ZoneType.Safe:
                    SetWheelPanelZoneType(_settings.SpriteSafeZoneSpin, _settings.SpriteSafeZoneIndicator,
                        _settings.StringHeaderSafeZone, _settings.ColorTextSafeZone);
                    break;
                case ZonesPanelController.ZoneType.Super:
                    SetWheelPanelZoneType(_settings.SpriteSuperZoneSpin, _settings.SpriteSuperZoneIndicator,
                        _settings.StringHeaderSuperZone, _settings.ColorTextSuperZone);
                    break;
                default:
                    break;
            }
        }
    }
}
