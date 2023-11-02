using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Wheel;

public class RewardsPanelContent : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    private void Awake()
    {
        if (_image == null)
            _image = GetComponentInChildren<Image>();
        if (_text == null )   
            _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetReward(Sprite sprite, int rewardCount)
    {
        _image.sprite = sprite;
        _text.text = rewardCount.ToString();
    }
}
