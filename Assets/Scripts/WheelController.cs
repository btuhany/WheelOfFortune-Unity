using DG.Tweening;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Wheel
{
    public class WheelController : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Image _uiSliceImagePrefab;

        [Header("Settings")]
        [SerializeField] private WheelSettings _settings;

        [Header("Wheel Slice Contents")]
        [SerializeField] private WheelSliceContent[] _sliceContents;

        #region Components
        private RectTransform _rectTransform;
        #endregion

        #region Constants
        private const int _fullRotationDegree = 360;
        private const float _wheelRadius = 140f;
        #endregion

        private WheelSliceController[] _sliceControllers = new WheelSliceController[8];


        [ContextMenu("Update Contents")]
        private void UpdateSliceContents()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
            SetWheelSliceContents();
        }
        private void OnValidate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        private void Awake()
        {
            _sliceControllers = GetComponentsInChildren<WheelSliceController>();
        }
        private void Start()
        {
            SetWheelSliceContents();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                WheelSliceController randomSlice = _sliceControllers[Random.Range(0, _sliceControllers.Length)];
                Debug.Log(randomSlice.SliceIndex);
                Spin(randomSlice.SliceIndex * 45);
            }
        }
        private void SetWheelSliceContents()
        {
            for (int i = 0; i < _sliceControllers.Length; i++)
            {
                _sliceControllers[i].SetSliceIndex(i);

                if (i < _sliceContents.Length && _sliceContents[i] != null)
                    _sliceControllers[i].SetContent(_sliceContents[i]);
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
        
        //private void SetSlicePositions()
        //{
        //    float anglePerSlice = Mathf.Deg2Rad * _fullRotationDegree / _slices.Length ;
        //    for (int i = 0; i < _slices.Length; i++)
        //    {
        //        Image uiSliceImage = Instantiate(_uiSliceImagePrefab, _rectTransform.position, _rectTransform.rotation, this.transform);
        //        RectTransform uiSliceRectTransform = uiSliceImage.GetComponent<RectTransform>();
        //        float x = _wheelRadius * Mathf.Sin(anglePerSlice * i);
        //        float y = _wheelRadius * Mathf.Cos(anglePerSlice * i);
        //        uiSliceRectTransform.anchoredPosition = new Vector2(x, y); 
        //        uiSliceImage.transform.Rotate(new Vector3(0f, 0f, -i * anglePerSlice * Mathf.Rad2Deg));
        //        _slices[i].WheelIndex = i;
        //        _slices[i].SetSlice(uiSliceImage);
        //        _sliceList.Add(uiSliceImage.gameObject);
        //    }
        //}
    }
}
