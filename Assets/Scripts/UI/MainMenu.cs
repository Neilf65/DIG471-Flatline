using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public Image imageComponent;

  [SerializeField] private GameObject settingsMenu;
  [SerializeField] private GameObject howToPlayMenu;

  public void OnPointerEnter(PointerEventData eventData)
  {
    
    imageComponent.enabled = true;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    
    imageComponent.enabled = false;
  }

  public void Play()
  {
    SceneManager.LoadScene(1);
  }

  public void Settings()
  {
    settingsMenu.SetActive(true);
  }
  
  public void QuitGame()
  {
    Application.Quit();
  }

  public void Back()
  {
    howToPlayMenu.SetActive(false);
  }

  public void HowToPlay()
  {
    howToPlayMenu.SetActive(true);
  }

}
