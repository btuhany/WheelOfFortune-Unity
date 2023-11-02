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

        #region Components
        private Image _uiContentImage;
        private TextMeshProUGUI _uiContentText;

        public int SliceIndex { get => _sliceIndex; }
        public WheelItem Content { get => _content; }
        public int ContentCount { get => _contentCount; }
        #endregion

        private void OnValidate()
        {
            if (_uiContentImage == null)
                _uiContentImage = GetComponentInChildren<Image>();

            if (_uiContentText == null)
                _uiContentText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void UpdateUIElements(WheelItem item)
        {
            _uiContentImage.sprite = item.SpriteWheel;
            _uiContentText.text = "x" + item.Count.ToString();
        }
        public void SetContent(WheelItem item)
        {
            _content = item;
            _content.SetRandomCount();

            UpdateUIElements(_content);
        }
        public void SetSliceIndex(int index)
        {
            _sliceIndex = index;
        }
    }
}
