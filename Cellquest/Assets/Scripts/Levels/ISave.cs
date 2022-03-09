using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Explicação

/* 
Abstract class com as funções base para salvar os dados de cada classe. Cada classe que herda ISave consegue definir o que fazer quando 
SaveData() e LoadData() forem chamados, já que são métodos abstract

Além disso, a cada 1 segundo, cada classe chama SaveData() para salvar o jogo. Acho que dá pra melhorar isso porque não sei se é muito 
sustentável salvar os dados a cada tão pouco tempo

As classes que herdam ISave também chamam SaveData() toda vez que o jogador tira o jogo de foco do celular (fecha, minimiza etc)
*/

#endregion

public abstract class ISave : MonoBehaviour
{
    public abstract void SaveData();
    public abstract void LoadData();

    public virtual void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            SaveData();
    }

    public IEnumerator SaveEnumerator()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1f);
        while (true)
        {
          
                SaveData();

            
            yield return waitTime;
        }
    }

}
