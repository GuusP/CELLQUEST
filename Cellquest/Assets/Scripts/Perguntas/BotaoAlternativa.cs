using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region Explicação

/* 
Script do botão de alternativa das fases de pergunta
 
*/

#endregion

public class BotaoAlternativa : MonoBehaviour
{
    [SerializeField] Animator animator;
    Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Acertou()
    {
        animator.SetTrigger("Acertou");
    }

    public void Errou()
    {
        animator.SetTrigger("Errou");
    }
}
