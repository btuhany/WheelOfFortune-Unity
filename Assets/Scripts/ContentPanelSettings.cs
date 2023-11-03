using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Content Panel Setting")]
    public class ContentPanelSettings : ScriptableObject
    {
        [Header("Show Slice Content Animation")]
        [SerializeField] private float _animShowSliceContentTime = 1f;
        [SerializeField] private Ease _animShowSliceEase = Ease.OutBounce;

        [Header("Hide Slice Content Animation")]
        [SerializeField] private float _animHideSliceContentTime = 1f;
        [SerializeField] private Ease _animHideSliceContentEase = Ease.OutBounce;

        private readonly int _contentPanelDefaultScaleX = 1;
        private readonly int _contentPanelHidedScaleX = 0;

        public float AnimShowSliceContentTime { get => _animShowSliceContentTime; }
        public Ease AnimShowSliceEase { get => _animShowSliceEase; }
        public float AnimHideSliceContentTime { get => _animHideSliceContentTime; }
        public Ease AnimHideSliceContentEase { get => _animHideSliceContentEase; }
        public int ContentPanelDefaultScaleX => _contentPanelDefaultScaleX;
        public int ContentPanelHidedScaleX => _contentPanelHidedScaleX;
    }
}
