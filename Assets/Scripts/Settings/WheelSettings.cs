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
        [SerializeField] private float _animIndicatorStartSwingTime = 0.2f;
        [SerializeField] private float _animIndicatorSwingTimePerLoop = 0.1f;
        [SerializeField] private float _animIndicatorEndSwingTime = 0.4f;
        [SerializeField] private float _animIndicatorDegree = 60f;
        [SerializeField] private float _animIndicatorEndSwingAngle = 30f;
        [SerializeField] private float _animIndicatorStartSwingAngle = 30f;
        [SerializeField] private Ease _animIndicatorStopEase = Ease.OutElastic;

        [Header("Tier Bombs Config (Max Exclusive)")]
        [SerializeField] private int _bombCountTierOneMin = 1;
        [SerializeField] private int _bombCountTierOneMax = 3;
        [SerializeField] private int _bombCountTierTwoMin = 1;
        [SerializeField] private int _bombCountTierTwoMax = 4;
        [SerializeField] private int _bombCountTierThreeMin = 2;
        [SerializeField] private int _bombCountTierThreeMax = 5;

        [Header("Prefabs")]
        [SerializeField] private WheelItem _bombItem;
        public float AnimSpinStartDegree { get => _animSpinStartDegree; }
        public float AnimSpinStartTime { get => _animSpinStartTime; }
        public Ease AnimSpinStartEase { get => _animSpinStartEase; }

        public float AnimSpinDegree { get => _animSpinDegree; }
        public float AnimSpinTime { get => _animSpinTime; }
        public Ease AnimSpinEase { get => _animSpinEase; }

        public float AnimSpinEndTime { get => _animSpinEndTime; }
        public Ease AnimSpinEndEase { get => _animSpinEndEase; }

        public int AnimSpinLoopCount { get => _animSpinLoopCount; }
        public float AnimIndicatorStartSwingTime { get => _animIndicatorStartSwingTime; }
        public float AnimIndicatorSwingTimePerLoop { get => _animIndicatorSwingTimePerLoop; }
        public float AnimIndicatorEndSwingTime { get => _animIndicatorEndSwingTime; }
        public float AnimIndicatorEndSwingAngle { get => _animIndicatorEndSwingAngle; }
        public float AnimIndicatorSwingAngle { get => _animIndicatorDegree; }
        public float AnimIndicatorStartSwingAngle { get => _animIndicatorStartSwingAngle; }
        public Ease AnimIndicatorStopEase { get => _animIndicatorStopEase; }
        public int BombCountTierOneMin { get => _bombCountTierOneMin; }
        public int BombCountTierOneMax { get => _bombCountTierOneMax; }
        public int BombCountTierTwoMin { get => _bombCountTierTwoMin; }
        public int BombCountTierTwoMax { get => _bombCountTierTwoMax; }
        public int BombCountTierThreeMin { get => _bombCountTierThreeMin; }
        public int BombCountTierThreeMax { get => _bombCountTierThreeMax; }
        public WheelItem BombItem { get => _bombItem; }
    }
}
