using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private RectTransform _rectTransform;
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
        private void HandleOnAnimStart(WheelSliceController slice)
        {
            SetContent(slice.Content, slice.ContentCount);
            this.gameObject.SetActive(true);
        }
        private void HandleOnAnimEnd()
        {
            this.gameObject.SetActive(false); //UI optimization
        }
        private void SetContent(WheelSliceContent content, int count)
        {
            _contentImage.sprite = content.Sprite;
            _contentHeaderText.text = content.Name;
            _contentCountText.text = "x" + count.ToString();
        }
        public async UniTask ShowContentAnimation(WheelSliceController slice)
        {
            HandleOnAnimStart(slice);

            await _rectTransform.DOScaleX(
                _settings.ContentPanelDefaultScaleX,
                _settings.AnimShowSliceContentTime)
                .SetEase(_settings.AnimShowSliceEase);
        }
        //TODO: Try transform instead of rect transform.
        //public async UniTask SpawnAndAnimateRewards(RectTransform target)
        //{
        //    RectTransform rewardTransform = Instantiate(_rewardImg, _rectTransform);
        //    await rewardTransform.DOMove(target.position, 1f);
        //}
        public async UniTask HideContentAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.ContentPanelHidedScaleX,
                _settings.AnimHideSliceContentTime)
                .SetEase(_settings.AnimHideSliceContentEase);
            
            HandleOnAnimEnd();
        }


    }
}

