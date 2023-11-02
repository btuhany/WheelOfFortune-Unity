using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WheelOfFortune.Items;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class RewardsPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RewardsPanelSettings _settings;

        [Header("References")]
        [SerializeField] private RectTransform _rewardsContentHolder;

        private Dictionary<WheelItem, RewardsPanelContentController> _rewardsDictionary = new Dictionary<WheelItem, RewardsPanelContentController>();
        public void AddReward(WheelItem item)
        {
            if (_rewardsDictionary.ContainsKey(item))
            {
                RewardsPanelContentController rewardsContent = _rewardsDictionary[item];
                rewardsContent.IncreaseCount(item.Count);
                
            }
            else
            {
                RewardsPanelContentController newReward = Instantiate(_settings.RewardsPanelContentPrefab, _rewardsContentHolder);
                newReward.SetReward(item);
                _rewardsDictionary.Add(item, newReward);
            }
        }        
    }
}

