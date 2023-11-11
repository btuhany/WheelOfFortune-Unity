using DG.Tweening;
using System;
using UnityEngine;

namespace WheelOfFortune.Settings
{
    [CreateAssetMenu(menuName = "Wheel Of Fortune/Panels/Bomb Panel Settings")]
    public class BombPanelSettings : ScriptableObject
    {
        [Header("Background Image Config")]
        [SerializeField] private float _backgroundFadeTime = 1.0f;

        [Header("Bomb Image Config")]
        [SerializeField] private TweenVector3 _bombHeartbeatScaleAnim;
        [SerializeField] private float _bombAnimLoopInterval = 3f;
        [SerializeField] private float _bombAnimPunchInterval = 0.1f;

        [Header("Flash Image Config")]
        [SerializeField] private TweenVector3 _flashRotateAnim;
        [SerializeField] private TweenVector3 _flashStartScaleAnim;
        [SerializeField] private float _flashImgFadeTime = 0.7f;
        [SerializeField] private float _flashImgAlphaVal = 0.65f;

        [Header("Info Text Config")]
        [SerializeField] private float _textFadeTime = 0.6f;

        [Header("Buttons Config")]
        [SerializeField] private float _buttonAnimTime = 0.3f;
        public float BombAnimLoopInterval { get => _bombAnimLoopInterval; }
        public float BombAnimPunchInterval { get => _bombAnimPunchInterval; }
        public float BackgroundFadeTime { get => _backgroundFadeTime; }
        public float TextFadeTime { get => _textFadeTime; }
        public float FlashImgFadeTime { get => _flashImgFadeTime; }
        public float FlashImgAlphaVal { get => _flashImgAlphaVal; }
        public float ButtonAnimTime { get => _buttonAnimTime; }
        public TweenVector3 BombHeartbeatScaleAnim { get => _bombHeartbeatScaleAnim; }
        public TweenVector3 FlashRotateAnim { get => _flashRotateAnim; }
        public TweenVector3 FlashStartScaleAnim { get => _flashStartScaleAnim; }
    }
}
