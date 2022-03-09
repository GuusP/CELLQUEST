using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#region Explicação

/* 
Usado para chamar a caixa/UI que aparece quando o jogador clica para jogar um nível. Acho que pode ser melhorado (caso seja de interesse) juntando os scripts/managers de UI que estão no jogo
porque tem muitos scripts de UI, que tem função de gerenciar a UI, que podiam ser juntados entre si 
 
*/

#endregion

public class LevelWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator LevelWindowAnimator;
    [SerializeField] GameObject FadeEffect;
    [SerializeField] Animator FadeEffectaAnimator;

    [SerializeField] Button PlayButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(FindObjectOfType<LevelManager>().Jogar);
    }

    void Start()
    {
        LevelManager.OnSelectLevel += OpenLevelWindow;
        LevelWindowAnimator = gameObject.GetComponent<Animator>();
        FadeEffect = GameObject.Find("FadeEffect");
        FadeEffectaAnimator = FadeEffect.GetComponent<Animator>();
        gameObject.SetActive(false);
        FadeEffect.SetActive(false);
    }



    void OpenLevelWindow(int levelNumber)
    {

        this.gameObject.SetActive(true);
        LevelWindowAnimator.SetBool("tra", true);
        FadeEffect.SetActive(true);
        FadeEffectaAnimator.SetBool("Ligado", true);
        GetComponentInChildren<TextMeshProUGUI>().text = "FASE " + levelNumber;
    }

    public void CloseLevelWindow()
    {

        LevelWindowAnimator.SetBool("tra", false);
        FadeEffectaAnimator.SetBool("Ligado", false);
        StartCoroutine("enumerator");
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.25f);
        FadeEffect.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        LevelManager.OnSelectLevel -= OpenLevelWindow;
    }
}
