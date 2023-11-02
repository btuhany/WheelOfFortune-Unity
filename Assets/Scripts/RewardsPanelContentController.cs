using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;

namespace WheelOfFortune.Panels
{
    public class RewardsPanelContentController : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _countText;
        private int _count;
        private void Awake()
        {
            if (_image == null)
                _image = GetComponentInChildren<Image>();
            if (_countText == null)
                _countText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void UpdateCountText()
        {
            _countText.text = _count.ToString();
        }
        public void SetReward(WheelItem item)
        {
            _image.sprite = item.SpriteReward;
            _count = item.Count;
            UpdateCountText();
        }
        public void IncreaseCount(int value)
        {
            _count += value;
            UpdateCountText();
        }
        
    }
}

