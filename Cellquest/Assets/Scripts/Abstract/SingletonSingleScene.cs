

#region Explicação

/* 
Uso esse script como base class para toda classe que eu quero que seja um Singleton para ter fácil acesso a ela em todo script 
e durante UMA só cena do jogo 
 
Herda a classe abstract ISave para poder chamar a função de salvar dados da classe nas classes que são Singleton, caso precise
 
*/

#endregion

using UnityEngine;

public class SingletonSingleScene<T> : ISave where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public override void LoadData()
    {
     
    }

    public override void SaveData()
    {
       
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}