using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject HitVFX;
    // Start is called before the first frame update
    public Rigidbody2D rb;
    [SerializeField] private float speed;
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        rb.velocity = transform.right * speed;
        Invoke(nameof(OnDestroy),3f);

    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<PlayerInfo>().OnHit(30f);
            Instantiate(HitVFX,transform.position,transform.rotation);
            OnDestroy();
        }
    }
}