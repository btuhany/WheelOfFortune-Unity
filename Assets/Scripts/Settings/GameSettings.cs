using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Game/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Game Config")]
        [SerializeField] private int _reviveGoldCost = 25;
        [Header("Tiers Config")]
        [SerializeField] private int _tierOneLimit = 10;
        [SerializeField] private int _tierTwoLimit = 20;
        [SerializeField] private int _tierThreeLimit = 30;

        public int ReviveGoldCost { get => _reviveGoldCost; }
        public int TierOneLimit { get => _tierOneLimit; }
        public int TierTwoLimit { get => _tierTwoLimit; }
        public int TierThreeLimit { get => _tierThreeLimit; }
    }
}
