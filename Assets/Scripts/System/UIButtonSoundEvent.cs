using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {    

	public void OnPointerEnter( PointerEventData ped ) 
    {
	    // SoundEffectsOSManager.PlayOSSound(SoundType.PRESS, .6f);
	}

	public void OnPointerDown( PointerEventData ped ) 
    {
	    // SoundEffectsOSManager.PlayOSSound(SoundType.HOVER, .5f);
	}    
}
