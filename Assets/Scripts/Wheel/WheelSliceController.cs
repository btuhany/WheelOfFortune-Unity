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

        //For showing the bomb ui image properly.
        //Bomb ui image has an 512x512 image dimensions.
        //It isn't compatible with the image dimensions of rewards. 
        private Vector2 _initialItemImageSizeDelta;
        private readonly Vector2 _bombPosOffset = new Vector2(0, 5);

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
        private void Awake()
        {
            _initialItemImageSizeDelta = _itemImageRect.sizeDelta;
        }
        private void UpdateUIElements(WheelItem item)
        {
            _itemImage.sprite = item.SpriteWheel;

            if (item.Type == WheelItem.ItemType.Reward)
            {
                _itemCountText.text = "x" + item.Count.ToString();
                _itemImageRect.sizeDelta = _initialItemImageSizeDelta;
                _itemImageRect.anchoredPosition = Vector2.zero;
            }
            else
            {
                _itemCountText.text = "";
                _itemImageRect.sizeDelta = new Vector2(
                    _itemImageRect.rect.width, _itemImageRect.rect.width);
                _itemImageRect.anchoredPosition = _bombPosOffset;
            }
        }
        public void TryRandomizeContentCount()
        {
            if (!_content.DoRandomizeCount) return;
            _content.SetRandomCount();
            UpdateUIElements(_content);
        }
        public void SetContent(WheelItem item)
        {
            _content = item;

            if (_content.DoRandomizeCount)
                _content.SetRandomCount();

            UpdateUIElements(_content);
        }
        public void SetSliceIndex(int index)
        {
            _sliceIndex = index;
        }
    }
}
