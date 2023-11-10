using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Pools;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class ZonesPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ZonesPanelSettings _settings;
        [Header("References")]
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private RectTransform _gridHolderRect;
        [SerializeField] private GridLayoutGroup _zonesGridLayout;
        [SerializeField] private RectTransform _zoneBackground;
        [SerializeField] private RectTransform _prewZonesFilter;
        [SerializeField] private Image _zoneBackgroundImg;

        private List<TextMeshProUGUI> _zonesList = new List<TextMeshProUGUI>();
        private Vector3 _gridHolderInitialPos;
        private int _counterZone = 1;
        private int _counterZoneGroupRtrnPool = 0;
        private float _zoneRectWidth;

        public enum ZoneType
        {
            None,
            Normal,
            Safe,
            Super
        }
        public int ZoneSafeValue => _settings.ZoneSafeValue;
        public int ZoneSuperValue => _settings.ZoneSuperValue;
        public int CurrentZone => _counterZone;
        public ZoneType CurrentZoneType => GetZoneType(_counterZone);

        public event System.Action<ZoneType> OnZoneChangedEvent;

        private void OnValidate()
        {
            if (_panelRect == null)
                _panelRect = GetComponent<RectTransform>();

            if (_zoneBackgroundImg == null && _zoneBackground != null)
                _zoneBackgroundImg = _zoneBackground.GetComponent<Image>();
        }
        private void Awake()
        {
            InitializeScrollGrid();
            AddZones(_settings.GroupMaxActiveSize * _settings.GroupsAtStart);
            _gridHolderInitialPos = _gridHolderRect.anchoredPosition;
            UpdateZoneTextColor();
        }

        //It's better to control positions of ui elements via script,
        //because max zone count in zone panel is a value.
        private void InitializeScrollGrid()
        {
            _zoneRectWidth = _panelRect.rect.width / _settings.GroupMaxActiveSize;
            _zonesGridLayout.cellSize = new Vector2(_zoneRectWidth, _settings.GroupCellHeight);
            _zoneBackground.sizeDelta = new Vector2(_zoneRectWidth, _zoneBackground.sizeDelta.y);
            _zonesGridLayout.transform.localPosition = new Vector3(
                -_zonesGridLayout.cellSize.x * _settings.StartLocalPosFactor,
                _zonesGridLayout.transform.localPosition.y,
                _zonesGridLayout.transform.localPosition.z);
            _prewZonesFilter.sizeDelta = new Vector2(
                _zoneRectWidth * (_settings.GroupMaxActiveSize - _settings.PrewFilterMaxGroupSizeDif) * _settings.StartLocalPosFactor,
                _prewZonesFilter.sizeDelta.y);
        }
        private void AddZones(int value)
        {
            for (int i = 1; i <= value; i++)
            {
                //TextMeshProUGUI zoneText =  Instantiate(_settings.ZonePrefab, _zonesGridLayout.transform);
                TextMeshProUGUI zoneText = UITextZonePool.Instance.GetObject(true);
                zoneText.transform.SetParent(_zonesGridLayout.transform);

                zoneText.text = i.ToString();

                _zonesList.Add(zoneText);

                ZoneType type = GetZoneType(i);
                if (type == ZoneType.Safe)
                    zoneText.color = _settings.ZoneSafeColor;
                else if (type == ZoneType.Super)
                    zoneText.color = _settings.ZoneSuperColor;
            }
        }
        private void CurrentZoneBgChangeAnim()
        {
            Sequence colorSequence = DOTween.Sequence();

            colorSequence.Append(
                _zoneBackgroundImg.DOColor(
                    _settings.ZoneBgColorFadeAnim,
                    _settings.ZoneBgColorFadeAnimTime)
                .SetEase(_settings.ZoneBgClrFadeStartEase));

            colorSequence.AppendCallback(
                () => ChangeZoneBgImg(GetZoneType(_counterZone)));

            colorSequence.Append(
                _zoneBackgroundImg.DOColor(
                    Color.white,
                    _settings.ZoneBgColorFadeAnimTime)
                .SetEase(_settings.ZoneBgClrFadeEndEase));
        }
        private ZoneType GetZoneType(int zoneValue)
        {
            if (zoneValue % _settings.ZoneSafeValue == 0 && zoneValue % _settings.ZoneSuperValue != 0)
                return ZoneType.Safe;
            else if (zoneValue % _settings.ZoneSuperValue == 0)
                return ZoneType.Super;
            else
                return ZoneType.Normal;
        }
        private void ChangeZoneBgImg(ZoneType currentZone)
        {
            if (currentZone == ZoneType.Safe)
                _zoneBackgroundImg.sprite = _settings.ZoneSpriteSafe;
            else if (currentZone == ZoneType.Super)
                _zoneBackgroundImg.sprite = _settings.ZoneSpriteSuper;
            else
                _zoneBackgroundImg.sprite = _settings.ZoneSpriteNormal;
        }

        //Zone counter starts from 1.
        //To reach the zones from the zoneList,
        //zone counter must decreased by 1.
        private void UpdateZoneTextColor()
        {
            ZoneType currentType = GetZoneType(_counterZone);

            //Prewious zone (decrease counter by two)
            if (_counterZone > 1 && GetZoneType(_counterZone - 1) == ZoneType.Normal)
                _zonesList[_counterZone - 2].color = Color.white;
            

            //Current zone (decrease counter by one)
            if (currentType == ZoneType.Normal)
                _zonesList[_counterZone - 1].color = Color.black;
        }
        private void CheckHandleZoneTypeChange()
        {
            ZoneType prewZoneType = GetZoneType(_counterZone - 1);
            ZoneType currentZoneType = GetZoneType(_counterZone);

            if (prewZoneType != currentZoneType)
                OnZoneChangedEvent?.Invoke(currentZoneType);
        }
        private void HandleScrollOnZonesReturnPool()
        {
            int returnObjectCount = _settings.GroupMaxActiveSize / _settings.ReturnObjectsCountMaxCountDivider;
            for (int i = 0; i < returnObjectCount; i++)
                UITextZonePool.Instance.ReturnObject(_zonesList[_counterZone - returnObjectCount - i]);
            _gridHolderRect.DOLocalMove(
            _gridHolderRect.localPosition - _settings.GroupSlideDir * _zoneRectWidth * returnObjectCount,
            _settings.ScrollTime * _settings.GridHolderRectTimeFactor)
            .SetEase(Ease.Linear).ToUniTask();
        }
        public async void ScrollZones(int value)
        {
            await _gridHolderRect.DOLocalMove(
                _gridHolderRect.localPosition + _settings.GroupSlideDir * _zoneRectWidth * value,
                _settings.ScrollTime)
                .SetEase(_settings.ScrollEase).ToUniTask();

            _counterZone += value;
            _counterZoneGroupRtrnPool++;

            if (_counterZoneGroupRtrnPool > _settings.GroupMaxActiveSize * _settings.ReturnPoolMinZoneCountFactor)
            {
                HandleScrollOnZonesReturnPool();
                _counterZoneGroupRtrnPool = 0;
            }

            UpdateZoneTextColor();
            CurrentZoneBgChangeAnim();
            CheckHandleZoneTypeChange();
        }
        public void ResetZones()
        {
            foreach (TextMeshProUGUI text in _zonesList)
            {
                if (text.gameObject != null)
                    Destroy(text.gameObject);
            }
            _zonesList.Clear();
            _counterZone = 1;
            AddZones(_settings.GroupMaxActiveSize * _settings.GroupsAtStart);
            _gridHolderRect.anchoredPosition = _gridHolderInitialPos;
            OnZoneChangedEvent?.Invoke(GetZoneType(_counterZone));
        }
        public void InvokeZoneChangeEvent()
        {
            OnZoneChangedEvent?.Invoke(GetZoneType(_counterZone));
        }
    }
}

