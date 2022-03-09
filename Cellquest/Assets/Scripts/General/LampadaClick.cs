using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Explicação

/* 
Usado para fazer o click do botão da lâmpada da aba home. Acho que pode ser melhorado (caso seja de interesse) juntando os scripts/managers de UI que estão no jogo
porque tem muitos scripts de UI, que tem função de gerenciar a UI, que podiam ser juntados entre si 
*/

#endregion

public class LampadaClick : MonoBehaviour
{
    Animator _Animator;
    [SerializeField] Sprite _LampadaApagadaImage;
    [SerializeField] bool isOn = true;
    [SerializeField] private Sprite _LampadaAcesaImage;

    private void Start()
    {
        _Animator = GetComponentInParent<Animator>();
      
    }

    public void OnCLick()
    {
        if (isOn)
        {
            GetComponentsInChildren<Image>()[1].sprite = _LampadaApagadaImage;
            _Animator.SetTrigger("Off");
            isOn = false;
        }else if (!isOn)
        {
            GetComponentsInChildren<Image>()[1].sprite = _LampadaAcesaImage;
            _Animator.SetTrigger("On");
            isOn = true;
        }
    }
}
