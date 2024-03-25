using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    float kunaiCount = 0;

    [SerializeField]public Rigidbody2D rb;
    [SerializeField]public GameObject hit_VFX;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * 5f;
        
    }

    // Update is called once per frame
    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        kunaiCount ++;
        Invoke(nameof(OnDespawn),4f);
        
    }
    public void OnDespawn()
    {
        if(kunaiCount >= 2)
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        collision.GetComponent<PlayerInfo>().OnHit(30f);
        Instantiate(hit_VFX, transform.position,transform.rotation);
        OnDespawn();
    }

}
