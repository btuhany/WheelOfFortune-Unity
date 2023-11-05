using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;

namespace WheelOfFortune.Panels
{
    public class RewardController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _countText;
        
        private int _count;

        public Image ItemImage { get => _itemImage; }

        private void OnValidate()
        {
            if (_itemImage == null)
                _itemImage = GetComponentInChildren<Image>();
            if (_countText == null)
                _countText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void UpdateCountText()
        {
            _countText.text = _count.ToString();
        }
        public void SetReward(WheelItem item, bool setCount = false)
        {
            _itemImage.sprite = item.SpriteReward;
            _count = setCount ? item.Count : 0;
            UpdateCountText();
        }
        public void IncreaseCount(int value)
        {
            _count += value;
            UpdateCountText();
        }
        public void SetCount(int value)
        {
            _count = value;
            UpdateCountText();
        }
        
    }
}

