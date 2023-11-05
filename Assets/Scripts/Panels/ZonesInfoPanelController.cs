using TMPro;
using UnityEngine;

namespace WheelOfFortune.Panels
{
    public class ZonesInfoPanelController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _textSafeZoneNum;
        [SerializeField] private TextMeshProUGUI _textSuperZoneNum;

        public void UpdateZoneInfo(int safeZoneNum, int superZoneNum)
        {
            _textSafeZoneNum.text = safeZoneNum.ToString();
            _textSuperZoneNum.text = superZoneNum.ToString();
        }
    }
}
