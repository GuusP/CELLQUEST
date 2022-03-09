using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explicação

/* 
 * Esse ScriptableObject é usado para definir o que o jogador vai ganhar ou perder dependendo do seu resultado numa fase.
 * Tem variáveis como coinToGive (moedas para dar) que armazena quantas moedas o jogador deve ganhar se vencer na fase
 * e lifeToTake (vida para tirar) que armazena quanto de vida o jogador deve perder caso perca
 * 
 * Crio um LevelConsequence para cada nível e passo pelo inspector para cada NormalLevelButton (level)
*/

#endregion


[CreateAssetMenu(fileName = "Level Consequence", menuName = "Levels/Level Consequence")]
[System.Serializable]
public class LevelConsequenceSO : ScriptableObject
{
    public int coinToGive = 50;
    public int lifeToTake = 1;
    public bool alreadyFinished = false;
    public List<ItemSO> rewardItems = new List<ItemSO>();
    public void Won()
    {
        
        PlayerManager.Instance.UpdateCoins(coinToGive);

        if (!alreadyFinished)
        {
            foreach(ItemSO item in rewardItems)
            {
                item.UnlockItem();
            }
            alreadyFinished = true;
        }
    }

    public void Lost()
    {
        PlayerManager.Instance.UpdateLife(-lifeToTake);
    }

}
