using UnityEngine;

namespace WheelOfFortune.Items
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Items/Wheel Item Group")]
    public class WheelItemGroup : ScriptableObject
    {
        [SerializeField] private WheelItem[] _items = new WheelItem[8];

        public WheelItem[] Items { get => _items; }
    }
}
