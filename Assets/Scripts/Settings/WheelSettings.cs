using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using WheelOfFortune.Items;

namespace WheelOfFortune.Settings
{
    [Serializable] 
    public struct TweenVector3
    {
        public Vector3 value;
        public float time;
        public Ease ease;
    }

    [Serializable]
    public struct TweenFloat
    {
        public float value;
        public float time;
        public Ease ease;
    }
    [Serializable]
    public struct TweenColor
    {
        public Color color;
        public float time;
        public Ease ease;
    }

    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Settings")]
    public class WheelSettings : ScriptableObject
    {
        [Header("Spin Animations")]
        public TweenVector3 spinStartRotationAnim;
        public TweenVector3 spinLoopRotationAnim;
        public TweenVector3 spinEndRotationAnim;
        [SerializeField] private int _spinAnimLoop = 3;

        [Header("Indicator Animations")]
        public TweenVector3 indicatorStartRotationAnim;
        public TweenVector3 indicatorLoopRotationAnim;
        public TweenVector3 indicatorEndRotationAnim;
        [SerializeField] private float _indicatorAnimTimePerLoop = 0.1f;

        [Header("Tier Bombs Config (Max Exclusive)")]
        [SerializeField] private int _bombCountTierOneMin = 1;
        [SerializeField] private int _bombCountTierOneMax = 3;
        [SerializeField] private int _bombCountTierTwoMin = 1;
        [SerializeField] private int _bombCountTierTwoMax = 4;
        [SerializeField] private int _bombCountTierThreeMin = 2;
        [SerializeField] private int _bombCountTierThreeMax = 5;

        [Header("Prefabs")]
        [SerializeField] private WheelItem _bombItem;

        [Header("Config")]
        [SerializeField] private int _sliceCount = 8;
        [SerializeField] private float _anglePerSlice = 45f;

        public int BombCountTierOneMin { get => _bombCountTierOneMin; }
        public int BombCountTierOneMax { get => _bombCountTierOneMax; }
        public int BombCountTierTwoMin { get => _bombCountTierTwoMin; }
        public int BombCountTierTwoMax { get => _bombCountTierTwoMax; }
        public int BombCountTierThreeMin { get => _bombCountTierThreeMin; }
        public int BombCountTierThreeMax { get => _bombCountTierThreeMax; }
        public WheelItem BombItem { get => _bombItem; }
        public float AnglePerSlice { get => _anglePerSlice; }
        public float IndicatorAnimTimePerLoop { get => _indicatorAnimTimePerLoop; }
        public int SpinAnimLoop { get => _spinAnimLoop; }
        public int SliceCount { get => _sliceCount; }
    }
}
