using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Explicação

/* 
Essa classe define as características que uma pergunta vai ter. Com ela, definimos objetos de perguntas das fases.
 
*/

#endregion

[System.Serializable]

public class PerguntasBlueprint 
{
    
   
    [TextArea(3, 10)]
    public string pergunta;
    public Alternativas[] alternativas;
    public char altCorreta;
    public string questionFont = " ";
}
[System.Serializable]
public class Alternativas
{
    [TextArea(3, 10)]
    public string texto;
    public char letra;

   
}
