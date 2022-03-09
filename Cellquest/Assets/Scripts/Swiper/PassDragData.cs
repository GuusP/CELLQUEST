using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PassDragData : MonoBehaviour, IEndDragHandler
{
    public MainMenuSwiper mainMenuSwiper;
    // Start is called before the first frame update
   

    public void OnEndDrag(PointerEventData data)
    {
        mainMenuSwiper.OnEndDrag(data);
    }

   
}
