using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using WheelOfFortune.Items;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Settings")]
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

        [Header("Indicator Animation")]
        [SerializeField] private float _animIndicatorStartTime = 0.2f;
        [SerializeField] private float _animIndicatorRotateTimePerLoop = 0.1f;
        [SerializeField] private float _animIndicatorEndTime = 0.4f;
        [SerializeField] private float _animIndicatorEndWaitTime = 0.5f;
        [SerializeField] private float _animIndicatorDegreeZ = 60f;
        [SerializeField] private float _animIndicatorEndDegreeZ = 30f;
        [SerializeField] private float _animIndicatorStartDegreeZ = 30f;

        public float AnimSpinStartDegree { get => _animSpinStartDegree; }
        public float AnimSpinStartTime { get => _animSpinStartTime; }
        public Ease AnimSpinStartEase { get => _animSpinStartEase; }

        public float AnimSpinDegree { get => _animSpinDegree; }
        public float AnimSpinTime { get => _animSpinTime; }
        public Ease AnimSpinEase { get => _animSpinEase; }

        public float AnimSpinEndTime { get => _animSpinEndTime; }
        public Ease AnimSpinEndEase { get => _animSpinEndEase; }

        public int AnimSpinLoopCount { get => _animSpinLoopCount; }
        public float AnimIndicatorStartTime { get => _animIndicatorStartTime; }
        public float AnimIndicatorRotateTimePerLoop { get => _animIndicatorRotateTimePerLoop; }
        public float AnimIndicatorEndTime { get => _animIndicatorEndTime; }
        public float AnimIndicatorEndWaitTime { get => _animIndicatorEndWaitTime; }
        public float AnimIndicatorEndDegreeZ { get => _animIndicatorEndDegreeZ; }
        public float AnimIndicatorDegreeZ { get => _animIndicatorDegreeZ; }
        public float AnimIndicatorStartDegreeZ { get => _animIndicatorStartDegreeZ; }
    }
}
