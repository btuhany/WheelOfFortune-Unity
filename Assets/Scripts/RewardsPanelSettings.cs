using UnityEngine;
using WheelOfFortune.Panels;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Rewards Panel Setting")]
    public class RewardsPanelSettings : ScriptableObject
    {
        [Header("Rewards Panel")]
        [SerializeField] private RewardsPanelContentController _rewardsPanelContentPrefab;

        public RewardsPanelContentController RewardsPanelContentPrefab { get => _rewardsPanelContentPrefab; }
    }
}
