using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            Debug.Log("-30");
            collision.GetComponent<PlayerInfo>().OnHit(30f);
        }
        
    }

}
