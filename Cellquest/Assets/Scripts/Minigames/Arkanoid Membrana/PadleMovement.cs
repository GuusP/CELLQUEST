using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Explicação

/* 
Define o movimento da plataforma do minigame (velocidade etc), o toque do jogador na plataforma, a colisão da plataforma com a bolinha
*/

#endregion

public class PadleMovement : MonoBehaviour
{
    float directionX;
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    RaycastHit2D raycastHit2D;
    [SerializeField]
    private float moveSpeed = 0.5f;
    Touch touch;
    [SerializeField] private float reflectForce;
    public float paddlePosOffset;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public float maxPosLeft;
    public float maxPosRight;
    public Vector3 PaddleInitialPos;
    void Start()
    {
        MembranaGameManager.Instance.OnPlayerDamaged += SetPaddleDefaultPos;
        PaddleInitialPos = transform.position;
        rightScreenEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        leftScreenEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        maxPosRight = rightScreenEdge - paddlePosOffset;
        maxPosLeft = leftScreenEdge + paddlePosOffset;
  
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MembranaGameManager.Instance.IsGameStarted)
        {
            return;
        }
        if (transform.position.x < maxPosLeft)
        {
            transform.position = new Vector2(maxPosLeft, transform.position.y);
        }
        if (transform.position.x > maxPosRight)
        {
            transform.position = new Vector2(maxPosRight, transform.position.y);
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                raycastHit2D = Physics2D.Raycast((Vector2)touchPosition, Vector2.zero);
            }
            
          directionX = touch.deltaPosition.x;
        }
    }

    private void FixedUpdate()
    {
        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject == this.gameObject)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    rb.velocity = new Vector2(directionX, 0) * moveSpeed;
                }else {
                    rb.velocity = Vector2.zero;
                }



                if (touch.phase == TouchPhase.Ended)
                {
                    rb.velocity = Vector2.zero;
                    raycastHit2D = new RaycastHit2D();
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D){
       if (collision2D.gameObject.tag == "Ball" && MembranaGameManager.Instance.IsBallFlying){
            Rigidbody2D BallRb = collision2D.gameObject.GetComponent<Rigidbody2D>();
            Vector3 HitPoint = collision2D.GetContact(0).point;
            Vector3 PadleCenter = this.transform.position;

            BallRb.velocity = Vector2.zero;

             float difference = HitPoint.x - PadleCenter.x;
           
            if (HitPoint.x < PadleCenter.x){
                
                    BallRb.AddForce(new Vector2((difference*reflectForce), BallsManager.Instance.InitialBallSpeed));
            }else if(HitPoint.x > PadleCenter.x){
                
                    BallRb.AddForce(new Vector2((difference*reflectForce), BallsManager.Instance.InitialBallSpeed));
            }else{
                BallRb.AddForce(new Vector2(0, BallsManager.Instance.InitialBallSpeed));
            }
        }
    }

    public void SetPaddleDefaultPos(Image[] image, Sprite sprite, int i)
    {
        
        this.transform.position = PaddleInitialPos;
    }
}
