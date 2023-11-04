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

        //Give-up and revive buttons
        [SerializeField] private Button[] _buttons = new Button[2];
        
        [SerializeField] private RectTransform _rectTransform;
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
            if (_buttons[0] == null || _buttons[1] == null)
                _buttons = GetComponentsInChildren<Button>();
        }
        private void Awake()
        {
            InitializeAnimHeartbeat();
            InitializeAnimFlashRotation();
            SetUIForAnimStart();
            _buttons[0].onClick.AddListener(HandleOnGiveUpBtnClk);
            _buttons[1].onClick.AddListener(HandleOnReviveBtnClk);
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
            _imgBackground.color = new Color(
                _imgBackground.color.r,
                _imgBackground.color.g,
                _imgBackground.color.b,
                0.0f);

            _textInfo.color = new Color(
                _textInfo.color.r,
                _textInfo.color.g,
                _textInfo.color.b,
                0.0f);

            _imgFlash.color = new Color(
                _imgFlash.color.r,
                _imgFlash.color.g,
                _imgFlash.color.b,
                0.0f);

            _imgFlash.transform.localScale = Vector3.zero;
            
            foreach (Button button in _buttons)
                button.transform.localScale = Vector3.zero;
            
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

            for (int i = 0; i < _buttons.Length; i++)
                await _buttons[i].transform.DOScale(Vector3.one, _settings.ButtonAnimTime);
            
        }
        public void ResetPanel()
        {
            SetUIForAnimStart();
            this.gameObject.SetActive(false);
        }
        public void SetButtonRevive(bool isEnable, int goldCost)
        {
            //Set revive button disabled.
            _buttons[1].gameObject.SetActive(isEnable);
            _textReviveGold.text = goldCost.ToString();
        }
    }
}
