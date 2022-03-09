using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoNext : MonoBehaviour
{
    public RectTransform[] abas;
    public ScrollRect scroll;
    public int currentIndex = 0;
    [SerializeField] private float easing = 0.5f;
 
   

    public void Avancar()
    {
        if (currentIndex < abas.Length - 1)
        {
            currentIndex++;
            UpdateAba();
        }
    }

    public void Voltar()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateAba();
        }
    }

    private void UpdateAba()
    {
        StartCoroutine(SmoothMove(scroll.content.anchoredPosition, new Vector2(-abas[currentIndex].anchoredPosition.x, scroll.content.anchoredPosition.y), easing));
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
}
