using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Wheel
{
    public class WheelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WheelSettings _settings;

        #region Components
        private RectTransform _rectTransform;
        #endregion

        private const int _fullRotationDegree = 360;
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Spin(_settings.targetAngle);
            }
        }

        private void Spin(int targetAngle)
        {
            float angleDifference = targetAngle - _rectTransform.rotation.eulerAngles.z;
            Sequence spinSequence = DOTween.Sequence();

            //Spin start animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, _settings.SpinStartAnimDegree),
            _settings.SpinStartAnimTime, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(_settings.SpinStartAnimEase));

            //Spin animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, _settings.SpinAnimDegree),
            _settings.SpinAnimTime, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(_settings.SpinAnimEase)
            .SetLoops(_settings.SpinAnimLoopCount));

            //Spin end animation append
            spinSequence.Append(
            _rectTransform.DOLocalRotate
            (new Vector3(0f, 0f, targetAngle - _fullRotationDegree),
            _settings.SpinEndAnimTime * ((_fullRotationDegree - angleDifference) / _fullRotationDegree), RotateMode.FastBeyond360)
            .SetRelative(false)
            .SetEase(_settings.SpinEndAnimEase));
        }
        //
    }
}
