using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explicação

/* 
É o script da bola do minigame da fase 4. Só detecta a colisão com a parede inferior para tirar vida do jogador e destruir a bola
 
*/

#endregion


public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == BallsManager.Instance.DownWall)
        {
            MembranaGameManager.Instance.TakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BallsManager.Instance.DownWall)
        {
            Destroy(this.gameObject);
        }
    }
}
