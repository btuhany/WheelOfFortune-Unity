using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private List<TextMeshProUGUI> _zoneTexts = new List<TextMeshProUGUI>();
        private enum ZoneType
        {
            Normal,
            Safe,
            Super
        }
        private Vector3 _gridHolderInitialPos;
        private Image _zoneBackgroundImg;
        private int _zoneCounter = 1;
        private float _zoneRectWidth;
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
        }
        private void InitializeScrollGrid()
        {
            _zoneRectWidth = _panelRect.rect.width / _settings.GroupMaxActiveSize;
            _zonesGridLayout.cellSize = new Vector2(_zoneRectWidth, _settings.GroupCellHeight);
            _zoneBackground.sizeDelta = new Vector2(_zoneRectWidth, _zoneBackground.sizeDelta.y);
            _zonesGridLayout.transform.localPosition = new Vector3(
                -_zonesGridLayout.cellSize.x / 2,
                _zonesGridLayout.transform.localPosition.y,
                _zonesGridLayout.transform.localPosition.z);
        }
        private void AddZones(int value)
        {
            for (int i = 1; i <= value; i++)
            {
                TextMeshProUGUI zoneText =  Instantiate(_settings.ZonePrefab, _zonesGridLayout.transform);
                zoneText.text = i.ToString();

                _zoneTexts.Add(zoneText);

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
                () => ChangeZoneBgImg(GetZoneType(_zoneCounter)));

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
        public void ScrollZone(int value)
        {
            _gridHolderRect.DOLocalMove(
                _gridHolderRect.localPosition + _settings.GroupSlideDir * _zoneRectWidth * value,
                _settings.ScrollTime)
                .SetEase(_settings.ScrollEase);

            _zoneTexts[_zoneCounter - 1].color = Color.white;
            _zoneCounter += value;

            _zoneTexts[_zoneCounter - 1].color = Color.black;

            CurrentZoneBgChangeAnim();
        }
        public void ResetZones()
        {
            foreach (TextMeshProUGUI text in _zoneTexts)
            {
                Destroy(text.gameObject);
            }
            _zoneTexts.Clear();
            _zoneCounter = 1;
            AddZones(_settings.GroupMaxActiveSize * _settings.GroupsAtStart);
            _gridHolderRect.anchoredPosition = _gridHolderInitialPos;
        }
    }
}

