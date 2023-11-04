using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class SpinPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpinPanelSettings _settings;

        private RectTransform _rectTransform;
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        public async UniTask HideSpinPanelAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.SpinPanelHidedScaleX,
                _settings.AnimHideSpinPanelTime)
                .SetEase(_settings.AnimHideSpinPanelEase).ToUniTask();
        }
        public async UniTask ShowSpinPanelAnimation()
        {
            await _rectTransform.DOScaleX(
                _settings.SpinPanelDefaultScaleX,
                _settings.AnimShowSpinPanelTime)
                .SetEase(_settings.AnimShowSpinPanelEase).ToUniTask();
        }
    }
}
