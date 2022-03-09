using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuSwiper : MonoBehaviour, IEndDragHandler
{
    private ScrollRect scroll;
    public float numeroDeAbas;
    [SerializeField] private float needyPercentage = 0.3f;
    [SerializeField] private float easing = 0.5f;
    [SerializeField] private Vector2 panelLocation;
    [SerializeField] private int paginaAtual = 3;
    
    public RectTransform[] abasTransforms;
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        scroll = GetComponent<ScrollRect>();
        Input.multiTouchEnabled = false;
        numeroDeAbas = scroll.content.childCount;
       panelLocation = scroll.content.anchoredPosition;
    
    }

   

    public void OnEndDrag(PointerEventData data)
    {

        float percenteY = (data.pressPosition.y - data.position.y) / Screen.width;
   
        if (Mathf.Abs(percenteY) < 0.05 == true)
        {
            StopAllCoroutines();
            float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
          

            if (Mathf.Abs(percentage) >= needyPercentage)
            {
                Vector2 newLocation = panelLocation;
                if (percentage > 0 && paginaAtual < numeroDeAbas)
                {
                    paginaAtual++;
                    newLocation += new Vector2(-scroll.viewport.rect.width, 0);
                }
                else if (percentage < 0 && paginaAtual > 1)
                {
                    paginaAtual--;
                    newLocation += new Vector2(scroll.viewport.rect.width, 0);
                }

                StartCoroutine(SmoothMove(scroll.content.anchoredPosition, newLocation, easing));
                panelLocation = newLocation;

            }

        }else
        {
            return;
        }
    }

    IEnumerator SmoothMove(Vector2 startpos, Vector2 endpos, float seconds)
    {

      
            float t = 0f;
            while (t <= 1.0)
            {

                t += Time.deltaTime / seconds;
                scroll.content.anchoredPosition = Vector2.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
        
    }
    
    public void Home()
    {
        panelLocation = new Vector2(-(abasTransforms[1].anchoredPosition.x ), scroll.content.anchoredPosition.y);
        StartCoroutine(SmoothMove(scroll.content.anchoredPosition, new Vector2(-abasTransforms[1].anchoredPosition.x, scroll.content.anchoredPosition.y), easing));
        paginaAtual = 2;
    }

    public void Loja()
    {
        panelLocation = new Vector2(-(abasTransforms[0].anchoredPosition.x), scroll.content.anchoredPosition.y);
        StartCoroutine(SmoothMove(scroll.content.anchoredPosition, new Vector2(-abasTransforms[0].anchoredPosition.x, scroll.content.anchoredPosition.y), easing));
        paginaAtual = 1;
    }


    public void Perfil()
    {
        panelLocation = new Vector2(-(abasTransforms[2].anchoredPosition.x ), scroll.content.anchoredPosition.y);
        StartCoroutine(SmoothMove(scroll.content.anchoredPosition, new Vector2(-abasTransforms[2].anchoredPosition.x, scroll.content.anchoredPosition.y), easing));
        paginaAtual = 3; 
    }
    
}
