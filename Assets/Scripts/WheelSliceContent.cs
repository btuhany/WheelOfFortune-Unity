using UnityEngine;

namespace WheelOfFortune.Wheel
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Wheel/Wheel Slice Content")]
    public class WheelSliceContent : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;

        [SerializeField] private int _minCount;
        [SerializeField] private int _maxCount;

        [SerializeField] private string _name;

        public Sprite Sprite { get => _sprite; }
        public int MinCount { get => _minCount; }
        public int MaxCount { get => _maxCount; }
        public string Name { get => _name; }
    }
}
