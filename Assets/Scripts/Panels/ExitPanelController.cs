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
        [SerializeField] private Button[] _buttons = new Button[2];
        [SerializeField] private RectTransform _rectTransform;
        
        public RectTransform RectTransform { get => _rectTransform; }
        public event System.Action OnBtnClkExitNo;
        public event System.Action OnBtnClkExitYes;

        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            if (_backgroundImg == null)
                _backgroundImg = GetComponent<Image>();
            if (_buttons[0] == null || _buttons[1] == null)
                _buttons = GetComponentsInChildren<Button>();
            if (_textQuestion == null)
                _textQuestion = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Awake()
        {
            _buttons[0].onClick.AddListener(HandleOnYesBtnClk);
            _buttons[1].onClick.AddListener(HandleOnNoBtnClk);

            foreach (Button button in _buttons)
            {
                button.interactable = false;
                button.enabled = false;
                button.transform.localScale = Vector3.zero;
            }

            Color startColor = _backgroundImg.color;
            _backgroundImg.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            startColor = _textQuestion.color;
            _textQuestion.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
                
            this.gameObject.SetActive(false);
        }
        public async UniTask PlayEnterBackground()
        {
            this.gameObject.SetActive(true);
            await _backgroundImg.DOFade(
                _settings.BackgroundColorAlpha, _settings.BackgroundFadeAnimTime)
                .SetEase(_settings.BackgroundFadeAnimEase).ToUniTask();
            foreach (Button button in _buttons)
            {
                button.interactable = true;
                button.enabled = true;
            }
        }
        public List<UniTask> PlayEnterQuestionButtons()
        {
            List<UniTask> fadeAnims = new List<UniTask>();
            fadeAnims.Add(_textQuestion.DOFade(1.0f, _settings.TextFadeAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_buttons[0].transform.DOScale(1.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_buttons[1].transform.DOScale(1.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            return fadeAnims;
        }
        public async UniTask ResetQuestionButtons()
        {
            List<UniTask> fadeAnims = new List<UniTask>();
            fadeAnims.Add(_textQuestion.DOFade(0.0f, _settings.TextFadeAnimTime)
                .SetEase(_settings.SpawnAnimEase)
                .ToUniTask());
            fadeAnims.Add(_buttons[0].transform.DOScale(0.0f, _settings.ButtonsScaleAnimTime)
                .SetEase(_settings.SpawnAnimEase).ToUniTask());
            fadeAnims.Add(_buttons[1].transform.DOScale(0.0f, _settings.ButtonsScaleAnimTime)
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
        private void HandleOnYesBtnClk()
        {
            OnBtnClkExitYes?.Invoke();
        }
        private void HandleOnNoBtnClk()
        {
            foreach (Button button in _buttons)
            {
                button.interactable = false;
                button.enabled = false;
            }
            OnBtnClkExitNo?.Invoke();
        }
    }
}
