using DG.Tweening;
using System;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Zones Panel Settings")]
    public class ZonesPanelSettings : ScriptableObject
    {
        [Header("Config")]
        [Tooltip("Multiplied with max group size")] [SerializeField] private int _groupsAtStart = 4; 
        [Header("Layout")]
        [SerializeField] private int _groupMaxActiveSize = 13;
        [SerializeField] private float _groupCellHeight = 60f;
        [SerializeField] private float _startLocalPosFactor = 0.5f;
        [SerializeField] private int _prewFilterMaxGroupSizeDif = 1;
        [Header("Scroll")]
        [SerializeField] private TweenVector3 scrollAnim;
        [Header("Zones")]
        [SerializeField] private TweenColor _zoneBgClrFadeAnim;
        [SerializeField] private int _zoneSafeValue = 5;
        [SerializeField] private Color _zoneSafeColor = Color.green;
        [SerializeField] private int _zoneSuperValue = 35;
        [SerializeField] private Color _zoneSuperColor = Color.red;
        [SerializeField] private Sprite _zoneSpriteNormal;
        [SerializeField] private Sprite _zoneSpriteSafe;
        [SerializeField] private Sprite _zoneSpriteSuper;
        [Header("Return Pool Config")]
        [SerializeField] private float _gridHolderRectTimeFactor = 0.3f;
        [SerializeField] private int _returnPoolMinZoneCountFactor = 5;
        [SerializeField] private int _returnObjectsCountMaxCountDivider = 2;

        public int GroupMaxActiveSize { get => _groupMaxActiveSize; }
        public Color ZoneSafeColor { get => _zoneSafeColor; }
        public Color ZoneSuperColor { get => _zoneSuperColor; }
        public int GroupsAtStart { get => _groupsAtStart; }
        public float GroupCellHeight { get => _groupCellHeight; }
        public int ZoneSafeValue { get => _zoneSafeValue; }
        public int ZoneSuperValue { get => _zoneSuperValue; }
        public Sprite ZoneSpriteNormal { get => _zoneSpriteNormal; }
        public Sprite ZoneSpriteSafe { get => _zoneSpriteSafe; }
        public Sprite ZoneSpriteSuper { get => _zoneSpriteSuper; }
        public float StartLocalPosFactor { get => _startLocalPosFactor; }
        public int PrewFilterMaxGroupSizeDif { get => _prewFilterMaxGroupSizeDif; }
        public float GridHolderRectTimeFactor { get => _gridHolderRectTimeFactor; }
        public int ReturnPoolMinZoneCountFactor { get => _returnPoolMinZoneCountFactor; }
        public int ReturnObjectsCountMaxCountDivider { get => _returnObjectsCountMaxCountDivider; }
        public TweenVector3 ScrollAnim { get => scrollAnim; }
        public TweenColor ZoneBgClrFadeAnim { get => _zoneBgClrFadeAnim; }
    }
}

