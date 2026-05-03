using UnityEngine;
using UnityEngine.UI;

public class BatteryMeter : MonoBehaviour
{
    public float BatteryCount, maxBatteries, Width, Height;
    [SerializeField] private PlayerController playerController;

    [SerializeField] RectTransform BatteryBar;

    public void SetBattery(float Batteries)
    {
        BatteryCount = Batteries;
        float newWidth = (BatteryCount / maxBatteries) * Width;

        BatteryBar.sizeDelta = new Vector2(newWidth, Height);
    }
}