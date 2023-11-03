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
        [SerializeField] private RectTransform _bombImgsHolder;
        [SerializeField] private Image _bombImage;
        [SerializeField] private Image _flashImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _textInfo;
        [SerializeField] private TextMeshProUGUI _reviveGoldCostTxt;

        //Give-up and revive buttons
        private Button[] _buttons = new Button[2];
        
        private RectTransform _rectTransform;
        private Sequence _animBombHeartbeat;
        private Tween _animFlashRotation;

        public event System.Action OnGiveUpButtonClick;
        public event System.Action OnReviveButtonClick;
        public event System.Action OnEnter;
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_backgroundImage == null)
                _backgroundImage = GetComponent<Image>();
            if (_buttons[0] == null || _buttons[1] == null)
                _buttons = GetComponentsInChildren<Button>();
        }
        private void Awake()
        {
            InitializeBombAnim();
            InitializeFlashAnim();
            SetUIElementsStart();
            _buttons[0].onClick.AddListener(HandleOnGiveUpBtnClk);
            _buttons[1].onClick.AddListener(HandleOnReviveBtnClk);
            this.gameObject.SetActive(false);
        }
        private void InitializeBombAnim()
        {
            _animBombHeartbeat = DOTween.Sequence();
            _animBombHeartbeat.Append(_bombImgsHolder.
                DOPunchScale(_settings.BombAnimPunchScale, _settings.BombAnimTime))
                .SetEase(_settings.BombAnimEase);
            _animBombHeartbeat.AppendInterval(_settings.BombAnimPunchInterval);
            _animBombHeartbeat.Append(_bombImgsHolder.
                DOPunchScale(_settings.BombAnimPunchScale, _settings.BombAnimTime))
                .SetEase(_settings.BombAnimEase);
            _animBombHeartbeat.AppendInterval(_settings.BombAnimLoopInterval);
            _animBombHeartbeat.SetLoops(-1, LoopType.Restart);
            _animBombHeartbeat.Pause();
        }
        private void InitializeFlashAnim()
        {
            _animFlashRotation = _flashImage.transform.DOLocalRotate(
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
            _animBombHeartbeat.Play();
        }
        private void SetUIElementsStart()
        {
            _backgroundImage.color = new Color(
                _backgroundImage.color.r,
                _backgroundImage.color.g,
                _backgroundImage.color.b,
                0.0f);

            _textInfo.color = new Color(
                _textInfo.color.r,
                _textInfo.color.g,
                _textInfo.color.b,
                0.0f);

            _flashImage.color = new Color(
                _flashImage.color.r,
                _flashImage.color.g,
                _flashImage.color.b,
                0.0f);

            _flashImage.transform.localScale = Vector3.zero;
            
            foreach (Button button in _buttons)
                button.transform.localScale = Vector3.zero;
            
        }
        private void HandleOnGiveUpBtnClk()
        {
            OnGiveUpButtonClick?.Invoke();
        }
        private void HandleOnReviveBtnClk()
        {
            OnReviveButtonClick?.Invoke();
        }
        public async UniTask StartEnterAnim()
        {
            OnEnter?.Invoke();
            this.gameObject.SetActive(true);

            List<UniTask> fadeAnims = new List<UniTask>();

            fadeAnims.Add(_textInfo.DOFade(1f, _settings.TextFadeTime).ToUniTask());
            fadeAnims.Add(_backgroundImage.DOFade(1.0f, _settings.BackgroundFadeTime).ToUniTask());
            fadeAnims.Add(_flashImage.DOFade(_settings.FlashImgAlphaVal, _settings.FlashImgFadeTime).ToUniTask());
            fadeAnims.Add(_flashImage.transform
                .DOScale(Vector3.one, _settings.FlashImgScaleAnimTime)
                .SetEase(_settings.FlashImgScaleAnimEase)
                .OnComplete(PlayFlashAnim).ToUniTask());

            await UniTask.WhenAll(fadeAnims);
            PlayHeartbeatAnim();
            for (int i = 0; i < _buttons.Length; i++)
            {
                await _buttons[i].transform.DOScale(Vector3.one, _settings.ButtonAnimTime);
            }
        }
        public void ResetPanel()
        {
            SetUIElementsStart();
            this.gameObject.SetActive(false);
        }
        public void UpdateReviveBtn(bool isEnable, int goldCost)
        {
            //Set revive button disabled.
            _buttons[1].gameObject.SetActive(isEnable);
            _reviveGoldCostTxt.text = goldCost.ToString();
        }
    }
}

