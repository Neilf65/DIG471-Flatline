using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider qualitySlider;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        RefreshSettings();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu pauseMenu = GetComponent<PauseMenu>();
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
        pauseMenu.SetActive(true);
    }
}
