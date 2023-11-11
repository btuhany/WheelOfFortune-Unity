using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Panels;

namespace WheelOfFortune.Settings
{
    [System.Serializable]
    public struct Tween
    {
        public float time;
        public Ease ease;
    }
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Rewards Panel Settings")]
    public class RewardsPanelSettings : ScriptableObject
    {
        [Header("Rewards Panel Config")]
        [SerializeField] private RewardController _rewardsPanelContentPrefab;
        [SerializeField] private Image _rewardContentImage;  //Used in animation.
        [SerializeField] private int _rewPartCountMaxPartLimit = 1;

        [Header("Reward Config")]
        [SerializeField] private TweenVector3 _rewardCollectReactionAnim;

        [Header("Reward Parts Config")]
        [SerializeField] private int _gatherMaxRewPart = 10;
        [SerializeField] private int _gatherRewPartsDelayFrame = 1;
        [Tooltip("Milliseconds")][SerializeField] private int _spawnRewPartsMillisecondsDelay = 200; 
        [SerializeField] private int _moveRewPartsMinMillisecondsDelay;
        [SerializeField] private int _moveRewPartsMaxMillisecondsDelay;
        [SerializeField] private Tween _rewPartMoveAnim;
        [SerializeField] private TweenVector3 _rewPartCollectScaleAnim;
        [SerializeField] private Vector3 _rewPartsMoveOffsetMinVector;
        [SerializeField] private Vector3 _rewPartsMoveOffsetMaxVector;
        [SerializeField] private Tween _rewPartsMoveOffsetAnim;
        [SerializeField] private TweenVector3 _rewPartSpawnScaleAnim;

        [Header("Exit Button")]
        [SerializeField] private TweenVector3 _btnExitHideAnim;
        [SerializeField] private TweenVector3 _btnExitUnhideAnim;

        [Header("At Exit")]
        [SerializeField] private Tween _exitPanelMoveAnim;
        [SerializeField] private float _exitShowRewsSizeDelta = 0.2f;
        [SerializeField] private int _exitShowRewsGridConstraint = 2;
        [SerializeField] private int _exitUnshowRewsGridConstraint = 1;
        [SerializeField] private float _heightFactorSizeDeltaY = 0.5f;
        [SerializeField] private float _heightFactorAnchorPosY = 0.125f;

        public int MoveRewPartsRandomMillisecondsDelay => Random.Range(_moveRewPartsMinMillisecondsDelay, _moveRewPartsMaxMillisecondsDelay);
        public Vector3 RewPartsRandomOffsetMove => new Vector3(
            Random.Range(_rewPartsMoveOffsetMinVector.x, _rewPartsMoveOffsetMaxVector.x),
            Random.Range(_rewPartsMoveOffsetMinVector.y, _rewPartsMoveOffsetMaxVector.y),
            Random.Range(_rewPartsMoveOffsetMinVector.z, _rewPartsMoveOffsetMaxVector.z));
        public RewardController RewardsPanelContentPrefab { get => _rewardsPanelContentPrefab; }
        public Image RewardContentImage { get => _rewardContentImage; }
        public int GatherMaxRewPart { get => _gatherMaxRewPart; }
        public int GatherRewPartsDelayFrame { get => _gatherRewPartsDelayFrame; }
        public int SpawnRewPartsMillisecondsDelay { get => _spawnRewPartsMillisecondsDelay; }
        public float ExitShowRewsSizeDelta { get => _exitShowRewsSizeDelta; }
        public int ExitShowRewsGridConstraint { get => _exitShowRewsGridConstraint; }
        public float HeightDividerSizeDeltaY { get => _heightFactorSizeDeltaY; }
        public float HeightDividerAnchorPosY { get => _heightFactorAnchorPosY; }
        public int ExitUnshowRewsGridConstraint { get => _exitUnshowRewsGridConstraint; }
        public int RewPartCountMaxPartLimit { get => _rewPartCountMaxPartLimit; }
        public TweenVector3 RewardCollectReactionAnim { get => _rewardCollectReactionAnim; }
        public TweenVector3 RewPartSpawnScaleAnim { get => _rewPartSpawnScaleAnim; }
        public Tween RewPartMoveAnim { get => _rewPartMoveAnim; }
        public TweenVector3 RewPartCollectScaleAnim { get => _rewPartCollectScaleAnim; }
        public Tween RewPartsMoveOffsetAnim { get => _rewPartsMoveOffsetAnim; }
        public Tween ExitPanelMoveAnim { get => _exitPanelMoveAnim; }
        public TweenVector3 BtnExitHideAnim { get => _btnExitHideAnim; }
        public TweenVector3 BtnExitUnhideAnim { get => _btnExitUnhideAnim; }
    }
}
