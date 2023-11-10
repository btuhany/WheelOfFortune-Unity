using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using WheelOfFortune.Items;

namespace WheelOfFortune.Settings
{
    [Serializable] 
    public struct Tween
    {
        public float time;
        public Ease ease;
    }

    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Settings")]
    public class WheelSettings : ScriptableObject
    {
        [Serializable]
        public struct RotateAnim
        {
            public Tween tween;
            public float angle;
        }

        [Header("Spin Animations")]
        public RotateAnim spinStartAnim;
        public RotateAnim spinLoopAnim;
        public RotateAnim spinEndAnim;
        [SerializeField] private int _spinAnimLoop = 3;

        [Header("Indicator Animations")]
        public RotateAnim indicatorStartAnim;
        public RotateAnim indicatorLoopAnim;
        public RotateAnim indicatorEndAnim;
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
    }
}
