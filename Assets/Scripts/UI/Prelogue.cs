using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Prelogue : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image imageComponent;
    [SerializeField] private GameObject nextButton;
    float startDist;
    float endDist;
    float textTime;
    public void OnPointerEnter(PointerEventData eventData)
  {
    
    imageComponent.enabled = true;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    
    imageComponent.enabled = false;
  }

  public void Next()
    {
        SceneManager.LoadScene("Hub Alpha");
    }

    void Update()
    {
    
    }
}
