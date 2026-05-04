using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float currentStamina, maxStamina, width, Height;
    [SerializeField] private PlayerController playerController;

    [SerializeField] RectTransform staminaBar;

    public void SetStamina(float Stamina)
    {
        currentStamina = Stamina;
        float newWidth = (currentStamina / maxStamina) * width;

        staminaBar.sizeDelta = new Vector2(newWidth, Height);
        Debug.Log("Changing Stamina bar now");
    }
}
