using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float currentStamina, _stamina, width, Height;
    [SerializeField] private PlayerController playerController;

    [SerializeField] RectTransform staminaBar;

    public void SetStamina(float Stamina)
    {
        currentStamina = Stamina;
        float newWidth = (currentStamina / _stamina) * width;

        staminaBar.sizeDelta = new Vector2(newWidth, Height);
        Debug.Log("Changing Stamina bar now");
    }
}
