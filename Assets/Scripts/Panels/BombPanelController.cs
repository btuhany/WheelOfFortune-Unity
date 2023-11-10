using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class BombPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private BombPanelSettings _settings;
        [Header("References")]
        [SerializeField] private RectTransform _imgAnimsHolder;
        [SerializeField] private Image _imgFlash;
        [SerializeField] private Image _imgBackground;
        [SerializeField] private TextMeshProUGUI _textInfo;
        [SerializeField] private TextMeshProUGUI _textReviveGold;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _giveUpButton;
        private Sequence _animHeartbeat;
        private Tween _animFlashRotation;

        public event System.Action OnBtnClkGiveUp;
        public event System.Action OnBtnClkRevive;
        public event System.Action OnPanelEnter;

        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_imgBackground == null)
                _imgBackground = GetComponent<Image>();
            if (_reviveButton == null || _giveUpButton == null)
            {
                Button[] buttons = new Button[2];
                buttons = GetComponentsInChildren<Button>();
                if (_giveUpButton == null)
                    _giveUpButton = buttons[0];
                if (_reviveButton == null)
                    _reviveButton = buttons[1];
            }
        }
        private void Awake()
        {
            InitializeAnimHeartbeat();
            InitializeAnimFlashRotation();
            SetUIForAnimStart();
            _giveUpButton.onClick.AddListener(HandleOnGiveUpBtnClk);
            _reviveButton.onClick.AddListener(HandleOnReviveBtnClk);
            this.gameObject.SetActive(false);
        }
        private void InitializeAnimHeartbeat()
        {
            _animHeartbeat = DOTween.Sequence();
            _animHeartbeat.Append(_imgAnimsHolder.
                DOPunchScale(_settings.BombAnimPunchScale, _settings.BombAnimTime))
                .SetEase(_settings.BombAnimEase);
            _animHeartbeat.AppendInterval(_settings.BombAnimPunchInterval);
            _animHeartbeat.Append(_imgAnimsHolder.
                DOPunchScale(_settings.BombAnimPunchScale, _settings.BombAnimTime))
                .SetEase(_settings.BombAnimEase);
            _animHeartbeat.AppendInterval(_settings.BombAnimLoopInterval);
            _animHeartbeat.SetLoops(-1, LoopType.Restart);
            _animHeartbeat.Pause();
        }
        private void InitializeAnimFlashRotation()
        {
            _animFlashRotation = _imgFlash.transform.DOLocalRotate(
                new Vector3(0f, 0f, -360f),
                _settings.FlashRotationTimePeriod, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
            _animFlashRotation.Pause();
        }
        private void PlayFlashAnim()
        {
            _animFlashRotation.Play();
        }
        private void PlayHeartbeatAnim()
        {
            _animHeartbeat.Play();
        }
        private void SetUIForAnimStart()
        {
            Color imgBgColor = _imgBackground.color;
            imgBgColor.a = 0f;
            _imgBackground.color = imgBgColor;

            Color textInfoColor = _textInfo.color;
            textInfoColor.a = 0f;
            _textInfo.color = textInfoColor;

            Color imgFlashColor = _imgFlash.color;
            imgFlashColor.a = 0f;
            _imgFlash.color = imgFlashColor;

            _imgFlash.transform.localScale = Vector3.zero;
            
            _reviveButton.transform.localScale = Vector3.zero;
            _giveUpButton.transform.localScale = Vector3.zero;
        }
        private void HandleOnGiveUpBtnClk()
        {
            OnBtnClkGiveUp?.Invoke();
        }
        private void HandleOnReviveBtnClk()
        {
            OnBtnClkRevive?.Invoke();
        }
        public async UniTask PlayEnter()
        {
            OnPanelEnter?.Invoke();
            this.gameObject.SetActive(true);

            List<UniTask> fadeAnims = new List<UniTask>();

            fadeAnims.Add(_textInfo.DOFade(1f, _settings.TextFadeTime).ToUniTask());
            fadeAnims.Add(_imgBackground.DOFade(1.0f, _settings.BackgroundFadeTime).ToUniTask());
            fadeAnims.Add(_imgFlash.DOFade(_settings.FlashImgAlphaVal, _settings.FlashImgFadeTime).ToUniTask());
            fadeAnims.Add(_imgFlash.transform
                .DOScale(Vector3.one, _settings.FlashImgScaleAnimTime)
                .SetEase(_settings.FlashImgScaleAnimEase)
                .OnComplete(PlayFlashAnim).ToUniTask());

            await UniTask.WhenAll(fadeAnims);

            PlayHeartbeatAnim();

            await _giveUpButton.transform.DOScale(Vector3.one, _settings.ButtonAnimTime);           
            await _reviveButton.transform.DOScale(Vector3.one, _settings.ButtonAnimTime);           
        }
        public void ResetPanel()
        {
            _animHeartbeat.Pause();
            _animFlashRotation.Pause();
            SetUIForAnimStart();
            this.gameObject.SetActive(false);
        }
        public void SetButtonRevive(bool isEnable, int goldCost)
        {
            //Set revive button disabled.
            _reviveButton.gameObject.SetActive(isEnable);
            _textReviveGold.text = goldCost.ToString();
        }
    }
}

