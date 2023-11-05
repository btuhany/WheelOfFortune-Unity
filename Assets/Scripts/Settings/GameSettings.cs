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

        public int ReviveGoldCost { get => _reviveGoldCost; }
    }
}
