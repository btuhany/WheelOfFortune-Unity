using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;
using WheelOfFortune.Settings;
using WheelOfFortune.Wheel;

namespace WheelOfFortune.Panels
{
    public class ContentPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ContentPanelSettings _settings;
        [Header("References")]
        [SerializeField] private Image _contentImage;
        [SerializeField] private TextMeshProUGUI _contentHeaderText;
        [SerializeField] private TextMeshProUGUI _contentCountText;
        [SerializeField] private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform.localScale = new Vector3(0f, 1f, 1f);
            this.gameObject.SetActive(false); //UI optimization
        }
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        private void HandleOnAnimStart(WheelItem content)
        {
            SetContent(content);
            this.gameObject.SetActive(true);
        }
        private void HandleOnAnimEnd()
        {
            this.gameObject.SetActive(false); //UI optimization
        }
        private void SetContent(WheelItem item)
        {
            _contentImage.sprite = item.SpriteWheel;
            _contentHeaderText.text = item.ItemName;
            _contentCountText.text = "x" + item.Count.ToString();
        }
        public async UniTask ShowContentAnimation(WheelItem content)
        {
            HandleOnAnimStart(content);

            await _rectTransform.DOScaleX(
                _settings.ContentPanelDefaultScaleX,
                _settings.AnimShowSliceContentTime)
                .SetEase(_settings.AnimShowSliceEase).ToUniTask();
        }
        public async UniTask HideContentAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.ContentPanelHidedScaleX,
                _settings.AnimHideSliceContentTime)
                .SetEase(_settings.AnimHideSliceContentEase).ToUniTask();
            
            HandleOnAnimEnd();
        }
    }
}

