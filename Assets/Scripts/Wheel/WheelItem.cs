using UnityEngine;

namespace WheelOfFortune.Items
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Items/Wheel Item")]
    public class WheelItem : ScriptableObject
    {
        public enum ItemType
        {
            Reward,
            Bomb
        }

        [Header("Info")]
        [SerializeField] private string _name;
        [SerializeField] private ItemType _type = ItemType.Reward;

        //To track the gold currency.
        //Normally It would be better to use it with an item enum type
        //but there is no need for it in this current situtation.
        [SerializeField] private bool _isGold = false; 

        [Header("Sprites")]
        [SerializeField] private Sprite _spriteWheel;
        [SerializeField] private Sprite _spriteReward;

        [Header("Wheel Config")]
        [SerializeField] private bool _isRandomizeCount = true;
        [SerializeField] private int _count = 0;
        [SerializeField] private int _minCount = 1;
        [SerializeField] private int _maxCount;

        public void SetRandomCount()
        {
            if (!_isRandomizeCount) return;

            if (_type == ItemType.Reward)
                _count = Random.Range(_minCount, _maxCount);
            else
                _count = -1;
        }

        #region Properties
        public string Name { get => _name; }
        public ItemType Type { get => _type; }
        public Sprite SpriteWheel { get => _spriteWheel; }
        public Sprite SpriteReward { get => _spriteReward; }
        public int Count { get => _count; }
        public bool IsGold { get => _isGold; }
        public bool IsRandomizeCount { get => _isRandomizeCount; }
        #endregion
    }
}
