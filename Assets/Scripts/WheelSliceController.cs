using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Wheel
{
    public class WheelSliceController : MonoBehaviour
    {
        private int _sliceIndex;
        private WheelSliceContent _content;
        private int _contentCount;

        #region Components
        private Image _uiContentImage;
        private TextMeshProUGUI _uiContentText;

        public int SliceIndex { get => _sliceIndex; }
        #endregion
        private void OnValidate()
        {
            _uiContentImage = GetComponentInChildren<Image>();
            _uiContentText = GetComponentInChildren<TextMeshProUGUI>();
        }
        public void SetContent(WheelSliceContent content)
        {
            _content = content;
            _contentCount = Random.Range(_content.MinCount, _content.MaxCount);

            UpdateUIElements(_content.Sprite, _contentCount);
        }
        public void SetSliceIndex(int index)
        {
            _sliceIndex = index;
        }
        private void UpdateUIElements(Sprite sprite, int contentCount)
        {
            _uiContentImage.sprite = sprite;
            _uiContentText.text = "x" + contentCount.ToString();
            
        }
    }
}
