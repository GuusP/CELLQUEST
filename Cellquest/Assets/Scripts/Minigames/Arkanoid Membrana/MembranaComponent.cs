using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Explicação

/* 
Script do componente (sprite) de membrana que fica dentro dos tijolos  
*/

#endregion

public class MembranaComponent : MonoBehaviour
{

    public void OnBrickDestroyed()
    {
        Rigidbody2D Rb = GetComponent<Rigidbody2D>();
        Rb.bodyType = RigidbodyType2D.Dynamic;
        Rb.gravityScale = 0.8f;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DownWall")
        {
            Destroy(this.gameObject);
        }
    }
}
