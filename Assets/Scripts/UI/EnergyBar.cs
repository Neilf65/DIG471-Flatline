using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public float currentEnergy, maxEnergy, Width, Height;
    [SerializeField] private PlayerController playerController;

    [SerializeField] RectTransform energyBar;

    public void SetEnergy(float Energy)
    {
        currentEnergy = Energy;
        float newWidth = (currentEnergy / maxEnergy) * Width;

        energyBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
