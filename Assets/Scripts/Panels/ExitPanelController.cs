using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class ExitPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ExitPanelSettings _settings;
        [Header("References")]
        [SerializeField] private Image _backgroundImg;
        [SerializeField] private TextMeshProUGUI _textQuestion;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _holderEndRewards;
        
        public RectTransform RectTransform { get => _rectTransform; }
        public RectTransform HolderEndRewards { get => _holderEndRewards; }

        public event System.Action OnBtnClkExitNo;
        public event System.Action OnBtnClkExitYes;

        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_backgroundImg == null)
                _backgroundImg = GetComponent<Image>();
            if (_yesButton == null || _noButton == null)
            {
                Button[] buttons = new Button[2];
                buttons = GetComponentsInChildren<Button>();
                if (_yesButton == null)
                    _yesButton = buttons[0];
                if (_noButton == null)
                    _noButton = buttons[1];
            }
            if (_textQuestion == null)
                _textQuestion = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Awake()
        {
            _yesButton.onClick.AddListener(HandleOnYesBtnClk);
            _noButton.onClick.AddListener(HandleOnNoBtnClk);

            SetButtons(false, true, _yesButton, _noButton);
            
            Color startColor = _backgroundImg.color;
            _backgroundImg.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            startColor = _textQuestion.color;
            _textQuestion.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
                
            this.gameObject.SetActive(false);
        }
        private void HandleOnYesBtnClk()
        {
            OnBtnClkExitYes?.Invoke();
        }
        private void HandleOnNoBtnClk()
        {
            SetButtons(false, false, _yesButton, _noButton);
            OnBtnClkExitNo?.Invoke();
        }
        private void SetButtons(bool active, bool resetScale, params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.enabled = active;
                button.interactable = active;
                if (resetScale)
                    button.transform.localScale = Vector3.zero;
            }
        }
        public async UniTask PlayEnterBackground()
        {
            this.gameObject.SetActive(true);
            await _backgroundImg.DOFade(
                _settings.BackgroundColorAlpha, _settings.BackgroundFadeAnimTime)
                .SetEase(_settings.BackgroundFadeAnimEase).ToUniTask();
            SetButtons(true, false, _yesButton, _noButton);
        }
        public List<UniTask> PlayEnterQuestionButtons()
        {
            List<UniTask> fadeAnims = new List<UniTask>();
            fadeAnims.Add(_textQuestion.DOFade(1.0f, _settings.TextFadeAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_yesButton.transform.DOScale(1.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_noButton.transform.DOScale(1.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            return fadeAnims;
        }
        public async UniTask ResetQuestionButtons()
        {
            List<UniTask> fadeAnims = new List<UniTask>();
            fadeAnims.Add(_textQuestion.DOFade(0.0f, _settings.TextFadeAnimTime)
                .SetEase(_settings.SpawnAnimEase)
                .ToUniTask());
            fadeAnims.Add(_yesButton.transform.DOScale(0.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_noButton.transform.DOScale(0.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());

            await UniTask.WhenAll(fadeAnims);
        }
        public async UniTask ResetBackground()
        {
            await _backgroundImg.DOFade(
                0.0f, _settings.BackgroundFadeAnimTime)
                .SetEase(_settings.BackgroundFadeAnimEase).ToUniTask();
            this.gameObject.SetActive(false);
        }

    }
}
