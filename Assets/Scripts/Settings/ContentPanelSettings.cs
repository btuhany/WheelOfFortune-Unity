using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Content Panel Settings")]
    public class ContentPanelSettings : ScriptableObject
    {
        [Header("Show Slice Content Animation")]
        [SerializeField] private TweenFloat _showContentPanelAnim;

        [Header("Hide Slice Content Animation")]
        [SerializeField] private TweenFloat _hideContentPanelAnim;

        public TweenFloat HideContentPanelAnim { get => _hideContentPanelAnim; }
        public TweenFloat ShowContentPanelAnim { get => _showContentPanelAnim; }
    }
}
