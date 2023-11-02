using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Setting")]
    public class WheelSettings : ScriptableObject
    {
        [Header("Spin Start Animation")]
        [SerializeField] private float _animSpinStartDegree;
        [SerializeField] private float _animSpinStartTime;
        [SerializeField] private Ease _animSpinStartEase;
        [Header("Spin Animation")]
        [SerializeField] private float _animSpinDegree;
        [SerializeField] private float _animSpinTime;
        [SerializeField] private Ease _animSpinEase;
        [SerializeField] private int _animSpinLoopCount;
        [Header("Spin End Animation")]
        [SerializeField] private float _animSpinEndTime;
        [SerializeField] private Ease _animSpinEndEase;
        [Header("Content Info Animation")]
        [SerializeField] private float _animContentInfoDuration;
        [SerializeField] private float _animContentInfoStartTime;
        [SerializeField] private float _animContentShowDelay;
        public float AnimSpinStartDegree { get => _animSpinStartDegree; }
        public float AnimSpinStartTime { get => _animSpinStartTime; }
        public Ease AnimSpinStartEase { get => _animSpinStartEase; }

        public float AnimSpinDegree { get => _animSpinDegree; }
        public float AnimSpinTime { get => _animSpinTime; }
        public Ease AnimSpinEase { get => _animSpinEase; }

        public float AnimSpinEndTime { get => _animSpinEndTime; }
        public Ease AnimSpinEndEase { get => _animSpinEndEase; }

        public int AnimSpinLoopCount { get => _animSpinLoopCount; }
        public float AnimContentInfoStartTime { get => _animContentInfoStartTime; }
        public float AnimContentInfoDuration { get => _animContentInfoDuration; }
        public float AnimContentShowDelay { get => _animContentShowDelay; }
    }
}
