using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explica��o

/* 
 * Esse ScriptableObject serve para criar database de Itens no jogo. Uso ele para ter um database de todos os itens presentes no jogo
 * e conseguir dar um ID int �nico para cada um deles. Fiz isso para conseguir salvar o ID dos itens que o jogador tem e os que tem na loja
 * para que possa recolocar esses itens nos seus invent�rios toda vez pegando eles no Database com seu ID �nico, j� que n�o da pra salvar
 * scriptableObjects com JSON
 * 
 * Toda vez que adiciono um item ao scriptableObject "Item Database", ele recebe um ID 
 * Baseado nesse v�deo: https://www.youtube.com/watch?v=232EqU1k9yQ&t=611s
*/

#endregion

[CreateAssetMenu(fileName ="New Item Database", menuName ="Itens/Item Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] Items;
    public Dictionary<ItemSO, int> GetID = new Dictionary<ItemSO, int>();

    public void OnAfterDeserialize()
    {
        SetIDs();
    }

    public void OnBeforeSerialize()
    {
    }

    public void SetIDs()
    {
        GetID = new Dictionary<ItemSO, int>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
            Items[i].itemID = i;
        }
    }
}
