using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Explica��o

/* 
Abstract class com as fun��es base para salvar os dados de cada classe. Cada classe que herda ISave consegue definir o que fazer quando 
SaveData() e LoadData() forem chamados, j� que s�o m�todos abstract

Al�m disso, a cada 1 segundo, cada classe chama SaveData() para salvar o jogo. Acho que d� pra melhorar isso porque n�o sei se � muito 
sustent�vel salvar os dados a cada t�o pouco tempo

As classes que herdam ISave tamb�m chamam SaveData() toda vez que o jogador tira o jogo de foco do celular (fecha, minimiza etc)
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
