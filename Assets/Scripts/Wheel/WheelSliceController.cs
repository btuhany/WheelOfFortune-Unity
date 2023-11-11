using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;

namespace WheelOfFortune.Wheel
{
    public class WheelSliceController : MonoBehaviour
    {
        private int _sliceIndex;
        private WheelItem _content;
        private int _contentCount;

        [SerializeField] private Image _itemImage;
        [SerializeField] private RectTransform _itemImageRect;
        [SerializeField] private TextMeshProUGUI _itemCountText;

        public int SliceIndex { get => _sliceIndex; }
        public WheelItem Content { get => _content; }
        public int ContentCount { get => _contentCount; }

        private void OnValidate()
        {
            if (_itemImage == null)
                _itemImage = GetComponentInChildren<Image>();

            if (_itemCountText == null)
                _itemCountText = GetComponentInChildren<TextMeshProUGUI>();

            if(_itemImageRect == null)
                _itemImageRect = _itemImage.GetComponent<RectTransform>();
        }
        private void UpdateUIElements(WheelItem item)
        {
            _itemImage.sprite = item.SpriteWheel;

            if (item.Type == WheelItem.ItemType.Reward)
                _itemCountText.text = "x" + item.Count.ToString();
            else
                _itemCountText.text = "";
        }
        public void TryRandomizeContentCount()
        {
            if (!_content.IsRandomCount) return;
            _content.SetRandomCount();
            UpdateUIElements(_content);
        }
        public void SetContent(WheelItem item, bool isRandomCount = true)
        {
            _content = item;

            if (_content.IsRandomCount && isRandomCount)
                _content.SetRandomCount();

            UpdateUIElements(_content);
        }
        public void SetSliceIndex(int index)
        {
            _sliceIndex = index;
        }
    }
}
