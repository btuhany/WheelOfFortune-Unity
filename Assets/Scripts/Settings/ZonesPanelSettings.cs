using DG.Tweening;
using TMPro;
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
        [SerializeField] private Vector3 _groupSlideDir = Vector2.left;
        [SerializeField] private float _groupCellHeight = 60f;
        [SerializeField] private float _startLocalPosFactor = 0.5f;
        [SerializeField] private int _prewFilterMaxGroupSizeDif = 1;
        [Header("Scroll")]
        [SerializeField] private float _scrollTime = 1f;
        [SerializeField] private Ease _scrollEase = Ease.Linear;
        [Header("Zones")]
        [SerializeField] private int _zoneSafeValue = 5;
        [SerializeField] private Color _zoneSafeColor = Color.green;
        [SerializeField] private int _zoneSuperValue = 35;
        [SerializeField] private Color _zoneSuperColor = Color.red;
        [SerializeField] private Color _zoneBgColorFadeAnim = Color.black;
        [SerializeField] private float _zoneBgColorFadeAnimTime = 0.2f;
        [SerializeField] private Ease _zoneBgClrFadeStartEase = Ease.InFlash;
        [SerializeField] private Ease _zoneBgClrFadeEndEase = Ease.OutFlash;
        [SerializeField] private Sprite _zoneSpriteNormal;
        [SerializeField] private Sprite _zoneSpriteSafe;
        [SerializeField] private Sprite _zoneSpriteSuper;
        [Header("Return Pool Config")]
        [SerializeField] private float _gridHolderRectTimeFactor = 0.3f;
        [SerializeField] private int _returnPoolMinZoneCountFactor = 5;
        [SerializeField] private int _returnObjectsCountMaxCountDivider = 2;

        public int GroupMaxActiveSize { get => _groupMaxActiveSize; }
        public Vector3 GroupSlideDir { get => _groupSlideDir; }
        public float ScrollTime { get => _scrollTime; }
        public Ease ScrollEase { get => _scrollEase; }
        public Color ZoneSafeColor { get => _zoneSafeColor; }
        public Color ZoneSuperColor { get => _zoneSuperColor; }
        public int GroupsAtStart { get => _groupsAtStart; }
        public Color ZoneBgColorFadeAnim { get => _zoneBgColorFadeAnim; }
        public float ZoneBgColorFadeAnimTime { get => _zoneBgColorFadeAnimTime; }
        public Ease ZoneBgClrFadeEndEase { get => _zoneBgClrFadeEndEase; }
        public Ease ZoneBgClrFadeStartEase { get => _zoneBgClrFadeStartEase; }
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
    }
}

