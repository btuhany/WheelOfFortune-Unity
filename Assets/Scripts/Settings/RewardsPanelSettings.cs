using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Panels;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Rewards Panel Settings")]
    public class RewardsPanelSettings : ScriptableObject
    {
        [Header("Rewards Panel")]
        [SerializeField] private RewardController _rewardsPanelContentPrefab;
        [SerializeField] private Image _rewardContentImage;  //Used in animation.

        [Header("Reward Part Gather Animation")]
        [Tooltip("Milliseconds")][SerializeField] private int _gatherAnimStartDelay = 200; 
        [SerializeField] private int _gatherMaxRewPart = 10;
        [SerializeField] private int _gatherRewPartsDelayFrame = 1;
        [Tooltip("Milliseconds")][SerializeField] private int _moveRewPartsMinDelay = 10;
        [Tooltip("Milliseconds")][SerializeField] private int _moveRewPartsMaxDelay = 400;
        [SerializeField] private float _moveRewPartTime = 1.0f;
        [SerializeField] private Ease _moveRewPartEase = Ease.InOutBounce;
        [SerializeField] private Vector3 _collectionTargetScale;
        [SerializeField] private float _collectionReactTime = 0.2f;
        [SerializeField] private float _collectionReactScaleFactor = 1.2f;
        [SerializeField] private Ease _collectionReactEase = Ease.InOutElastic;
        [SerializeField] private Vector2 _spawnMoveOffsetMinVector;
        [SerializeField] private Vector2 _spawnMoveOffsetMaxVector;
        [SerializeField] private float _spawnMoveOffsetTime = 1f;
        [SerializeField] private Ease _spawnMoveOffsetEase = Ease.InElastic;
        [SerializeField] private float _spawnRewPartScaleFactor = 1.2f;
        [SerializeField] private float _spawnRewPartScaleTime = 0.3f;
        [SerializeField] private Ease _spawnRePartScaleEase = Ease.OutElastic;

        [Header("Exit Button")]
        [SerializeField] private float _btnExitHideAnimTime = 0.2f;
        [SerializeField] private float _btnExitUnhideAnimTime = 0.2f;
        [SerializeField] private Ease _btnExitHideAnimEase = Ease.Flash;
        [SerializeField] private Ease _btnExitUnhideAnimEase = Ease.Flash;

        [Header("At Exit")]
        [SerializeField] private float _exitMoveAnimTime = 0.5f;
        [SerializeField] private Ease _exitMoveAnimEase;


        public RewardController RewardsPanelContentPrefab { get => _rewardsPanelContentPrefab; }
        public Image RewardContentImage { get => _rewardContentImage; }
        public int GatherMaxRewPart { get => _gatherMaxRewPart; }
        public int GatherRewPartsDelayFrame { get => _gatherRewPartsDelayFrame; }
        public float MoveRewPartsMinDelay { get => _moveRewPartsMinDelay; }
        public float MoveRewPartsMaxDelay { get => _moveRewPartsMaxDelay; }
        public float MoveRewPartTime { get => _moveRewPartTime; }
        public float CollectionReactScaleFactor { get => _collectionReactScaleFactor; }
        public float CollectionReactTime { get => _collectionReactTime; }
        public Ease CollectionReactEase { get => _collectionReactEase; }
        public Ease MoveRewPartEase { get => _moveRewPartEase; }
        public float SpawnRewPartScaleFactor { get => _spawnRewPartScaleFactor; }
        public float SpawnRewPartScaleTime { get => _spawnRewPartScaleTime; }
        public Ease SpawnRewPartScaleEase { get => _spawnRePartScaleEase; }
        public float SpawnMoveOffsetTime { get => _spawnMoveOffsetTime; }
        public Ease SpawnMoveOffsetEase { get => _spawnMoveOffsetEase; }
        public Vector2 SpawnMoveOffsetMinVector { get => _spawnMoveOffsetMinVector; }
        public Vector2 SpawnMoveOffsetMaxVector { get => _spawnMoveOffsetMaxVector; }
        public int GatherAnimStartDelay { get => _gatherAnimStartDelay; }
        public float ExitBtnHideAnimTime { get => _btnExitHideAnimTime; }
        public float ExitBtnUnhideAnimTime { get => _btnExitUnhideAnimTime; }
        public Ease ExitBtnHideAnimEase { get => _btnExitHideAnimEase; }
        public Ease ExitBtnUnhideAnimEase { get => _btnExitUnhideAnimEase; }
        public Vector3 CollectionTargetScale { get => _collectionTargetScale; }
        public float ExitMoveAnimTime { get => _exitMoveAnimTime; }
        public Ease ExitMoveAnimEase { get => _exitMoveAnimEase; }
    }
}
