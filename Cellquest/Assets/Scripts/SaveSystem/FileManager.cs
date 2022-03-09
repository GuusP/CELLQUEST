using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;

#region Explicação

/* 
 * classe que escreve nos arquivos para salvar os dados do jogo e lê os dados salvos para atualizar os dados do jogo 
 * SaveSystem baseado nesse vídeo: https://www.youtube.com/watch?v=uD7y4T4PVk0&t=680s
*/

#endregion

public class FileManager { 

    public static bool WriteToFile(string a_FileName, string a_FileContents) // método para escrever no arquivo. retorna true se conseguiu escrever e false se não
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName,  out string result) // método para pegar as coisas do arquivo e passar para result
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }

    public static void Reset(string a_FileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        File.Delete(fullPath);
    }
}
