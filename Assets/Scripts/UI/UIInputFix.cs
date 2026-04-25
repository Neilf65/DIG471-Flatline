using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Selectable))]
public class UIInputFix : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    GameObject lastSelectedGameObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current != null && lastSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
    }
}

