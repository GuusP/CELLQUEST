using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

#region Explicação

/* 
Usado para spawnar as bolas do minigame, definir suas velocidades, detectar o toque do jogador para iniciar o minigame, desativar a bola se necessário

*/

#endregion

public class BallsManager : MonoBehaviour
{
    #region  Singleton
    private static BallsManager _instance;

    public static BallsManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake(){
        if (_instance != null){
            Destroy(gameObject);
        }else {
            _instance = this;
        }
    }
    #endregion
    
    RaycastHit2D raycastHit2D;
    public PadleMovement Padle;
    public List<Ball> Balls {get; set;}
    [SerializeField] GameObject BallPrefab;
    GameObject InitialBall;
    public float InitialBallSpeed;
    Rigidbody2D InitialBallRb;
    [SerializeField] BrickManager BrickManager;
    public GameObject DownWall;
    public Transform BallInitialPos;
    public Vector3 maxPos;
    public GameObject menu;
    public GameObject dica;
    public SpriteRenderer spriteRenderer;
    public Dictionary<BrickType, string> BrickNames = new Dictionary<BrickType, string>()
    {
        {BrickType.Fosfolipídio, "Fosfolipídio" },
        {BrickType.Glicolipídio, "Glicolipídio" },
        {BrickType.Colesterol, "Colesterol" },
        {BrickType.P_Integral, "Proteína Integral" },
        {BrickType.P_Periférica, "Proteína Periférica" }
    };
    [SerializeField] private float ballYOffset; 
    // Start is called before the first frame update
    void Start()
    {
        
        InitializeBall();
        spriteRenderer = InitialBall.GetComponentInChildren<SpriteRenderer>();
        
       
    }

    // Update is called once per frame
    void Update()
    {

        if (menu.activeInHierarchy || dica.activeInHierarchy)
        {
            spriteRenderer.gameObject.SetActive(false);
            InitialBallRb.velocity = new Vector2(0f, 0f);
         
        }
        else
        {
            spriteRenderer.gameObject.SetActive(true);
        }

        if (!MembranaGameManager.Instance.IsBallFlying && MembranaGameManager.Instance.IsGameStarted)
        {

        
            InitialBall.transform.position = BallInitialPos.position;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                     if (touch.phase == TouchPhase.Began)
                     {
                      raycastHit2D = Physics2D.Raycast((Vector2)touchPosition, Vector2.zero);
                     }

                if (raycastHit2D.collider.gameObject == InitialBall.gameObject)
                {

                    
                   
                        if (touch.phase == TouchPhase.Ended && MembranaGameManager.Instance.IsGameStarted)
                        {
                            InitialBallRb.isKinematic = false;
                            InitialBallRb.AddForce(new Vector2(0, InitialBallSpeed));
                            MembranaGameManager.Instance.IsBallFlying = true;
                            raycastHit2D = new RaycastHit2D();
                        }
                  
                      
                }
            
            
    
            }
        }

        
    }

    public void InitializeBall(Image[] image, Sprite sprite, int i){

        if (i > 0)
        {
            Vector3 PadlePos = Padle.transform.position;
            Vector3 StartingPos = new Vector3(PadlePos.x, PadlePos.y + ballYOffset, PadlePos.z);
            InitialBall = Instantiate(BallPrefab, StartingPos, Quaternion.identity);
           
            InitialBallRb = InitialBall.GetComponent<Rigidbody2D>();

            Balls = new List<Ball>{
            InitialBall.GetComponent<Ball>()
             };
        }
        spriteRenderer = InitialBall.GetComponentInChildren<SpriteRenderer>();
    }

    public void InitializeBall()
    {
        Vector3 PadlePos = Padle.transform.position;
        Vector3 StartingPos = new Vector3(BallInitialPos.position.x, BallInitialPos.position.y, BallInitialPos.position.z);
        InitialBall = Instantiate(BallPrefab, StartingPos, Quaternion.identity);
        
        InitialBallRb = InitialBall.GetComponent<Rigidbody2D>();

        Balls = new List<Ball>{
            InitialBall.GetComponent<Ball>()
        };
        spriteRenderer = InitialBall.GetComponentInChildren<SpriteRenderer>();
    }

    public void DestroyBall()
    {
        Destroy(InitialBall);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(maxPos, new Vector3(1, 1, 1));
    }

    public void SetVelocity()
    {
        InitialBallRb.velocity = new Vector2(0, 2f);
    }
}
