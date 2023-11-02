using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Settings;
using WheelOfFortune.Wheel;

namespace WheelOfFortune.Panels
{
    public class RewardsPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RewardsPanelSettings _settings;

        [Header("References")]
        [SerializeField] private RectTransform _rewardsContentHolder;

        private List<RewardsPanelContent> _rewards;
        public void AddReward(WheelSliceController slice)
        {
            RewardsPanelContent newReward = Instantiate(_settings.RewardsPanelContentPrefab, _rewardsContentHolder);
            newReward.SetReward(slice.Content.Sprite, slice.ContentCount);
        }
        
    }
}

