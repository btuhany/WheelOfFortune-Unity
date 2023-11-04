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

        private readonly int _spinPanelDefaultScaleX = 1;
        private readonly int _spinPanelHidedScaleX = 0;

        public int SpinPanelDefaultScaleX => _spinPanelDefaultScaleX;

        public int SpinPanelHidedScaleX => _spinPanelHidedScaleX;

        public float AnimShowSpinPanelTime { get => _animShowSpinPanelTime; }
        public Ease AnimShowSpinPanelEase { get => _animShowSpinPanelEase; }
        public float AnimHideSpinPanelTime { get => _animHideSpinPanelTime; }
        public Ease AnimHideSpinPanelEase { get => _animHideSpinPanelEase; }
    }
}
