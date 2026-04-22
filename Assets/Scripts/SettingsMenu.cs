using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider qualitySlider;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        RefreshSettings();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RefreshSettings()
    {
        qualitySlider.value = Settings.QualityLevel;
        VolumeSlider.value = Settings.Volume;

        Apply();
    }

    public void Apply()
    {
        Settings.QualityLevel = (int)qualitySlider.value;
        Settings.Volume = VolumeSlider.value;

        QualitySettings.SetQualityLevel(Settings.QualityLevel);
        mixer.SetFloat("Master", Mathf.Log10(Settings.Volume) * 20);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
