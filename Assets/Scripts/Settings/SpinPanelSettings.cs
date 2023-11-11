using DG.Tweening;
using System;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Spin Panel Settings")]
    public class SpinPanelSettings : ScriptableObject
    {
        [Serializable]
        public struct SpinPanelAppearance
        {
            public Sprite spriteSpin;
            public Sprite spriteIndicator;
            public string textHeader;
            public Color textHeaderColor;
        }

        [Header("Show Panel Config")]
        [SerializeField] private TweenFloat _showPanelAnim;

        [Header("Hide Panel Config")]
        [SerializeField] private TweenFloat _hidePanelAnim;
        [Tooltip("Milliseconds")][SerializeField] private int _hidePanelMillisecondsDelay = 1000;

        [Header("Spin Panel Appearance")]
        [SerializeField] private SpinPanelAppearance _safeZoneAppear;
        [SerializeField] private SpinPanelAppearance _superZoneAppear;
        [SerializeField] private SpinPanelAppearance _normalZoneAppear;

        public int HidePanelMillisecondsDelay { get => _hidePanelMillisecondsDelay; }
        public TweenFloat ShowPanelAnim { get => _showPanelAnim; }
        public TweenFloat HidePanelAnim { get => _hidePanelAnim; }
        public SpinPanelAppearance NormalZoneAppear { get => _normalZoneAppear; }
        public SpinPanelAppearance SuperZoneAppear { get => _superZoneAppear; }
        public SpinPanelAppearance SafeZoneAppear { get => _safeZoneAppear; }
    }
}
