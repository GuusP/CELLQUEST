using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#region Explicação

/* 
 * classe que chama os métodos para atribuir os valores que serão salvos e chamar a função para salvar
 * SaveSystem baseado nesse vídeo: https://www.youtube.com/watch?v=uD7y4T4PVk0&t=680s
*/

#endregion

public class SaveDataManager  {

    public static Savable levelSavePointer;
    public static Savable questionSavePointer;
    public static void SaveJsonData(IEnumerable<ISavable> a_Saveables, string path) // salva uma coleção de dados salvaveis
    {
        SaveData sd = new SaveData();

        foreach (ISavable savable in a_Saveables) // pra cada dado salvavel  
        {
            savable.PopulateSaveData(sd); // coloca o dado em sd
        }
        
        if (FileManager.WriteToFile(path, sd.ToJson())) // converte sd para Json e salva no arquivo
        {
            
        }
    }

    public static void LoadJsonData(IEnumerable<ISavable> a_Saveables, string path) // carrega os dados de Json para cada dado salvavel
    {
        if (FileManager.LoadFromFile(path, out var json)) // carrega o arquivo em Json para a variável json 
        {
            
            SaveData sd = new SaveData(); 
            sd.LoadFromJson(json); // converte de Json para a classe sd

            foreach(ISavable savable in a_Saveables) // pra cada dado salvavel
            {
                savable.LoadFromSaveData(sd); // carrega os dados para cada dado salvavel
            }
            
        }

      
    }

    
    
}

public class Savable
{
    public List<ISavable> savables = new List<ISavable>();
    public string fileName;

    public Savable (ISavable savable, string _fileName)
    {
        savables.Add(savable);
        fileName = _fileName;
    }
}
