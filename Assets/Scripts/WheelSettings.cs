using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Wheel
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Settings")]
    public class WheelSettings : ScriptableObject
    {
        [Header("Temp Input")]
        [SerializeField] public int targetAngle;
        [Header("Spin Start Animation")]
        [SerializeField] private float _spinStartAnimDegree;
        [SerializeField] private float _spinStartAnimTime;
        [SerializeField] private Ease _spinStartAnimEase;
        [Header("Spin Animation")]
        [SerializeField] private float _spinAnimDegree;
        [SerializeField] private float _spinAnimTime;
        [SerializeField] private Ease _spinAnimEase;
        [SerializeField] private int _spinAnimLoopCount;
        [Header("Spin End Animation")]
        [SerializeField] private float _spinEndAnimTime;
        [SerializeField] private Ease _spinEndAnimEase;

        public float SpinStartAnimDegree { get => _spinStartAnimDegree; }
        public float SpinStartAnimTime { get => _spinStartAnimTime; }
        public Ease SpinStartAnimEase { get => _spinStartAnimEase; }

        public float SpinAnimDegree { get => _spinAnimDegree; }
        public float SpinAnimTime { get => _spinAnimTime; }
        public Ease SpinAnimEase { get => _spinAnimEase; }

        public float SpinEndAnimTime { get => _spinEndAnimTime; }
        public Ease SpinEndAnimEase { get => _spinEndAnimEase; }

        public int SpinAnimLoopCount { get => _spinAnimLoopCount; }
    }
}
