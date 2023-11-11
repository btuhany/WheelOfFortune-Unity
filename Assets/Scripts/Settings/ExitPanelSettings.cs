using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Exit Panel Settings")]
    public class ExitPanelSettings : ScriptableObject
    {
        [Header("Background")]
        [SerializeField] private TweenFloat _backgroundShowFadeAnim;
        [SerializeField] private TweenFloat _backgroundHideFadeAnim;
        [Header("Question Text")]
        [SerializeField] private TweenFloat _textHideFadeAnim;
        [SerializeField] private TweenFloat _textShowFadeAnim;
        [Header("Buttons")]
        [SerializeField] private TweenVector3 _buttonHideScaleAnim;
        [SerializeField] private TweenVector3 _buttonShowScaleAnim;
        public TweenFloat BackgroundFadeAnim { get => _backgroundShowFadeAnim; }
        public TweenFloat TextHideFadeAnim { get => _textHideFadeAnim; }
        public TweenFloat TextShowFadeAnim { get => _textShowFadeAnim; }
        public TweenVector3 ButtonHideScaleAnim { get => _buttonHideScaleAnim; }
        public TweenVector3 ButtonShowScaleAnim { get => _buttonShowScaleAnim; }
        public TweenFloat BackgroundHideFadeAnim { get => _backgroundHideFadeAnim; }
    }
}
