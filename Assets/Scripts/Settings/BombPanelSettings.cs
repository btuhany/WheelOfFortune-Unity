using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Bomb Panel Settings")]
    public class BombPanelSettings : ScriptableObject
    {
        [Header("Background Image Config")]
        [SerializeField] private float _backgroundFadeTime = 1.0f;

        [Header("Bomb Image Config")]
        [SerializeField] private Vector3 _bombAnimPunchScale;
        [SerializeField] private float _bombAnimTime;
        [SerializeField] private Ease _bombAnimEase;
        [SerializeField] private float _bombAnimLoopInterval = 3f;
        [SerializeField] private float _bombAnimPunchInterval = 0.1f;

        [Header("Flash Image Config")]
        [SerializeField] private float _flashRotationTimePeriod = 2f;
        [SerializeField] private float _flashImgFadeTime = 0.7f;
        [SerializeField] private float _flashImgScaleAnimTime = 0.6f;
        [SerializeField] private Ease _flashImgScaleAnimEase = Ease.Linear;
        [SerializeField] private float _flashImgAlphaVal = 0.65f;

        [Header("Info Text Config")]
        [SerializeField] private float _textFadeTime = 0.6f;

        [Header("Buttons Config")]
        [SerializeField] private float _buttonAnimTime = 0.3f;
        public Vector3 BombAnimPunchScale { get => _bombAnimPunchScale; }
        public float BombAnimTime { get => _bombAnimTime; }
        public Ease BombAnimEase { get => _bombAnimEase; }
        public float BombAnimLoopInterval { get => _bombAnimLoopInterval; }
        public float BombAnimPunchInterval { get => _bombAnimPunchInterval; }
        public float FlashRotationTimePeriod { get => _flashRotationTimePeriod; }
        public float BackgroundFadeTime { get => _backgroundFadeTime; }
        public float TextFadeTime { get => _textFadeTime; }
        public float FlashImgFadeTime { get => _flashImgFadeTime; }
        public float FlashImgScaleAnimTime { get => _flashImgScaleAnimTime; }
        public Ease FlashImgScaleAnimEase { get => _flashImgScaleAnimEase; }
        public float FlashImgAlphaVal { get => _flashImgAlphaVal; }
        public float ButtonAnimTime { get => _buttonAnimTime; }
    }
}
