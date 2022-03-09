using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Explicação

/* 
Usado para gerenciar a UI (barras) de progresso do jogador no minigame da fase 4. Acho que pode ser melhorado (caso seja de interesse) juntando os scripts/managers de UI que estão no jogo
porque tem muitos scripts de UI, que tem função de gerenciar a UI, que podiam ser juntados entre si 
 
*/

#endregion

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
     [SerializeField]Slider Progress_Bar;
  
    

    public void SetProgressSettings(int minValue, int maxValue)
    {
        Progress_Bar.minValue = minValue;
        Progress_Bar.maxValue = maxValue;
        Progress_Bar.value = 0;
    }

    public void AddOneInProgressValue()
    {
        Progress_Bar.value++;
    }

    public void SetCurrentProgress(int newValue)
    {
        Progress_Bar.value = newValue;
    }
}
