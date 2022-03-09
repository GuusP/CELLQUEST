using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


#region Explicação

/* 
Uso esse script como base class para a classe (script) dos botões de fase. Fiz isso porque, inicialmente, queriamos ter diferentes tipos de botão de nível
daí resolvi unir o que todos os botões tinham em comum nessa classe abstract base. Atualmente, ela não faz muito sentido porque só tem 1 tipo de botão de nível
 
*/

#endregion

[System.Serializable]
public abstract class BaseLevelButton : ISave
{
    public abstract TextMeshProUGUI LevelText { get; set; }
    public abstract Sprite UnlockedImage {  get;  set; }
    public abstract Sprite LockedImage { get; set; }
    public abstract bool IsLocked { get; set; }
    public abstract int LevelNumber { get; set; }
    

    public abstract void SelectLevel();
    public abstract void UnlockLevel();
}
