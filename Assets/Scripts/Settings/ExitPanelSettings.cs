using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Exit Panel Settings")]
    public class ExitPanelSettings : ScriptableObject
    {
        [Header("Background")]
        [SerializeField] private float _backgroundColorAlpha = 0.8f;
        [SerializeField] private float _backgroundFadeAnimTime = 0.3f;
        [SerializeField] private Ease _backgroundFadeAnimEase;
        [Header("Text")]
        [SerializeField] private float _textFadeAnimTime = 0.3f;
        [Header("Buttons")]
        [SerializeField] private float _buttonsScaleAnimTime = 0.3f;
        [Header("Text & Button")]
        [SerializeField] private Ease _spawnAnimEase = Ease.Flash;
        public float BackgroundColorAlpha { get => _backgroundColorAlpha; }
        public float BackgroundFadeAnimTime { get => _backgroundFadeAnimTime; }
        public Ease BackgroundFadeAnimEase { get => _backgroundFadeAnimEase; }
        public float TextFadeAnimTime { get => _textFadeAnimTime; }
        public float ButtonsScaleAnimTime { get => _buttonsScaleAnimTime; }
        public Ease SpawnAnimEase { get => _spawnAnimEase; }
    }
}
