using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;


#region Explicação

/* 
Funciona como "GameManager" das fases de pergunta. Spawna a pergunta, lida com o acerto ou erro
 
*/

#endregion

public class QuestionManager : MonoBehaviour
{
    public static NormalLevelButton NextLevel;

    [Tooltip("GameObject referente ao botão de alternativa")]
    public GameObject alternativasGameObj; // objeto para instanciar 

    [Tooltip("ScriptableObject com a pergunta da fase")]
    public PerguntaSO Pergunta;

    [Tooltip("GameObject com o texto para mostrar de onde a pergunta foi retirada")]
    public TextMeshProUGUI questionFontText;

    [Tooltip("Pergunta que está sendo respondida no momento")]
    public PerguntasBlueprint CurrentQuestion;

    [SerializeField] GameObject QuestionBox; // objeto para instanciar 
    [SerializeField] GameObject PerguntaGameObj;

    [Tooltip("Objeto para colocar o botão da pergunta e os botões de alternativa")]
    [SerializeField] GameObject PerguntaPrefabObj;// objeto para instanciar 

    char _contador = 'A'; // usado para setar a letra de cada alternativa

    [Tooltip("Quantas perguntas terão na fase")]
    [SerializeField] private int qntPerguntas; // quantidade de perguntas que serão jogadas na fase
    [SerializeField] private int _questionLives;
    public OnPhaseEnded phaseEnded;
    PerguntasBlueprint PerguntasBlueprint_;
    int currentQuestionIndex;
    int currentLevel;
    public AudioToPlay acertouAudio;
    public AudioToPlay errouAudio;
    [SerializeField] AudioManager AudioManager;
    private void Start()
    {
       
        currentLevel = LevelManager.selectedLevel;
        Pergunta = LevelManager.selectedLevelScript.pergunta;
        Active();
        MenuUiManager.Instance.SetQuestionLife(_questionLives);

    }


    public void Active() // ativa a pergunta para jogar 
    {
        if (Pergunta.notAnsweredQuestions == null || Pergunta.notAnsweredQuestions.Count == 0) // se não tiver nenhuma pergunta que ainda não foi respondida
        {
            SetQuestions();
        }
        CurrentQuestion = Pergunta.notAnsweredQuestions.First();
        Pergunta.notAnsweredQuestions.Remove(CurrentQuestion);
        PerguntaGameObj = GameObject.Instantiate(PerguntaPrefabObj, this.transform); // instancia o gameObj das perguntas 
        QuestionBox = PerguntaGameObj.GetComponentInChildren<Image>().gameObject;
        QuestionBox.GetComponentInChildren<TextMeshProUGUI>().text = CurrentQuestion.pergunta; // seta o texto da pergunta na UI

        foreach (Alternativas alt in CurrentQuestion.alternativas) // para cada alternativa na pergunta selecionada
        {

            GameObject alterButton;

            alterButton = Instantiate(alternativasGameObj, PerguntaGameObj.transform); // instancia um gameObj (botão) de alternativa 
            alterButton.GetComponent<Button>().onClick.AddListener(delegate { Click(alterButton); });
            alterButton.GetComponentInChildren<TextMeshProUGUI>().text = alt.texto; // seta o texto da alternativa
            alterButton.name = _contador.ToString(); // seta o nome do botão de alternativa como a letra que a alternativa representa
            alterButton.tag = "AlternativaButton";
            _contador++;

        }
        _contador = 'A';

        questionFontText.text = CurrentQuestion.questionFont;
    }

    private void OnDisable()
    {

    }

    public void Click(GameObject gameObject)
    {
        
      
        if (CurrentQuestion.altCorreta.ToString() == gameObject.name) // se for a alternativa certa
        {
            Acertou();
            gameObject.GetComponent<BotaoAlternativa>().Acertou();

        }
        else // se for a alternativa errada
        {
            Errou();
            gameObject.GetComponent<BotaoAlternativa>().Errou();
        }
    }

    private void Errou()
    {
        _questionLives--;
        AudioManager.PlayAudio(errouAudio);
        if (_questionLives <= 0)
        {
            Debug.Log(_questionLives);
            phaseEnded.gameObject.SetActive(true);
            phaseEnded.levelFinishedStatus = levelFinishedStatus.Lost;
            phaseEnded.StarsAchieved = _questionLives;

        }
        else
        {
            Debug.Log("Errou: " + _questionLives);
            
        }
        MenuUiManager.Instance.SetQuestionLife(_questionLives);
    }

    private void Acertou()
    {

        qntPerguntas--;
        AudioManager.PlayAudio(acertouAudio);
        
        //Destroy(PerguntaGameObj); // destroi o gameObj da pergunta respondida 
        if (qntPerguntas > 0) // se ainda tiverem perguntas a serem respondidas na fase
        {
            Active(); // ativa a próxima pergunta
        }
        else // se não tiver mais nenhuma pergunta a ser respondida
        {
            phaseEnded.gameObject.SetActive(true);
            phaseEnded.levelFinishedStatus = levelFinishedStatus.Won;
            phaseEnded.StarsAchieved = _questionLives;
            
        }
    }

    private void SetQuestions() // redefine a lista de perguntas "não jogadas"
    {
        foreach (PerguntasBlueprint question in Pergunta.PerguntasBlueprintObjet)
        {
            Pergunta.notAnsweredQuestions.Add(question);
        }
    }

}