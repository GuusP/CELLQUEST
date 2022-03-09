using System.Collections;
using System.Collections.Generic;
using UnityEngine;



#region Explicação

/* 
Define cada tijolo do minigame, detecta a colisão com a bolinha, lida com a colisão com a bola (tira vida do tijolo etc)
 
*/

#endregion

public class Brick : MonoBehaviour
{
    

    
    public BrickType BrickType;
    public List<Sprite> BrickSprites = new List<Sprite>(); 
    [SerializeField] private Sprite BrickComponentSprite;
     public int hitLives;
    public Queue<Sprite> BrickSprite = new Queue<Sprite>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            CollisionLogic();
        }
        
    }

    private void CollisionLogic()
    {
        hitLives--;

        if (hitLives <= 0)
        {
            MembranaGameManager.Instance.AudioManager.PlayAudio(MembranaGameManager.Instance.ultimaBatidaBloco);
            MembranaComponent membranaComponent = GetComponentInChildren<MembranaComponent>();
            transform.DetachChildren();
            membranaComponent.OnBrickDestroyed();
            BrickManager.OnBrickDestruction?.Invoke(this);
            Destroy(this.gameObject);
        }
        else
        {
            MembranaGameManager.Instance.AudioManager.PlayAudio(MembranaGameManager.Instance.baterBloco);
            GetComponent<SpriteRenderer>().sprite = BrickSprites[hitLives - 1];
           // GetComponent<SpriteRenderer>().sprite = BrickSprite.Dequeue();
        }
    }
}
