using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Rewards Panel Setting")]
    public class RewardsPanelSettings : ScriptableObject
    {
        [Header("Rewards Panel")]
        [SerializeField] private RewardsPanelContent _rewardsPanelContentPrefab;

        public RewardsPanelContent RewardsPanelContentPrefab { get => _rewardsPanelContentPrefab; }
    }
}
