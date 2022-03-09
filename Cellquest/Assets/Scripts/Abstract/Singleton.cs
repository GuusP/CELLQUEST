

#region Explica��o

/* 
Uso esse script como base class para toda classe que eu quero que seja um Singleton para ter f�cil acesso a ela em todo script
e durante toda a vida do jogo
 
Herda a classe abstract ISave para poder chamar a fun��o de salvar dados da classe nas classes que s�o Singleton, caso precise
 
*/

#endregion

using UnityEngine;

public class Singleton<T> : ISave where T : Component
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
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}