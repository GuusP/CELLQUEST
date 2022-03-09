using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explicação

/* 
ScriptableObject com as perguntas que serão feitas numa fase. 
tem uma lista das perguntas que ainda não foram respondias (alimentada inGame pelo array de perguntas possíveis)
e um array com todas as perguntas que podem ser usadas na fase (alimentada no inspector)
*/

#endregion

[System.Serializable]
[CreateAssetMenu(fileName = "Perguntas Level", menuName = "Perguntas/Perguntas Level")]
public class PerguntaSO : ScriptableObject
{
    [Tooltip("Perguntas que ainda não foram respondidas")]
    public List<PerguntasBlueprint> notAnsweredQuestions = new List<PerguntasBlueprint>();
    public PerguntasBlueprint[] PerguntasBlueprintObjet; // array que serve de banco de dados de perguntas que podem ser usadas na fase
}
