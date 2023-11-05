using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Spin Panel Settings")]
    public class SpinPanelSettings : ScriptableObject
    {
        [Header("Show Spin Panel Animation")]
        [SerializeField] private float _animShowSpinPanelTime = 1f;
        [SerializeField] private Ease _animShowSpinPanelEase = Ease.Linear;

        [Header("Hide Spin Panel Animation")]
        [SerializeField] private float _animHideSpinPanelTime = 1f;
        [SerializeField] private Ease _animHideSpinPanelEase = Ease.Linear;

        [Header("Spin & Indicator Sprites")]
        [Header("Safe Zone")]
        [SerializeField] private Sprite _spriteSafeZoneSpin;
        [SerializeField] private Sprite _spriteSafeZoneIndicator;
        [SerializeField] private string _stringHeaderSafeZone = "SILVER SPIN";
        [SerializeField] private Color _colorTextSafeZone;
        [Header("Super Zone")]
        [SerializeField] private Sprite _spriteSuperZoneIndicator;
        [SerializeField] private Sprite _spriteSuperZoneSpin;
        [SerializeField] private string _stringHeaderSuperZone = "GOLDEN SPIN";
        [SerializeField] private Color _colorTextSuperZone;
        [Header("Normal Zone")]
        [SerializeField] private Sprite _spriteNormalZoneSpin;
        [SerializeField] private Sprite _spriteNormalZoneIndicator;
        [SerializeField] private string _stringHeaderNormalZone = "BRONZE SPIN";
        [SerializeField] private Color _colorTextNormalZone;

        private readonly int _spinPanelDefaultScaleX = 1;
        private readonly int _spinPanelHidedScaleX = 0;

        public int SpinPanelDefaultScaleX => _spinPanelDefaultScaleX;
        public int SpinPanelHidedScaleX => _spinPanelHidedScaleX;
        public float AnimShowSpinPanelTime { get => _animShowSpinPanelTime; }
        public Ease AnimShowSpinPanelEase { get => _animShowSpinPanelEase; }
        public float AnimHideSpinPanelTime { get => _animHideSpinPanelTime; }
        public Ease AnimHideSpinPanelEase { get => _animHideSpinPanelEase; }
        public Sprite SpriteNormalZoneSpin { get => _spriteNormalZoneSpin; }
        public Sprite SpriteSafeZoneSpin { get => _spriteSafeZoneSpin; }
        public Sprite SpriteSuperZoneSpin { get => _spriteSuperZoneSpin; }
        public Sprite SpriteNormalZoneIndicator { get => _spriteNormalZoneIndicator; }
        public Sprite SpriteSafeZoneIndicator { get => _spriteSafeZoneIndicator; }
        public Sprite SpriteSuperZoneIndicator { get => _spriteSuperZoneIndicator; }
        public string StringHeaderSafeZone { get => _stringHeaderSafeZone; }
        public string StringHeaderSuperZone { get => _stringHeaderSuperZone; }
        public string StringHeaderNormalZone { get => _stringHeaderNormalZone; }
        public Color ColorTextSafeZone { get => _colorTextSafeZone; }
        public Color ColorTextSuperZone { get => _colorTextSuperZone; }
        public Color ColorTextNormalZone { get => _colorTextNormalZone; }
    }
}
