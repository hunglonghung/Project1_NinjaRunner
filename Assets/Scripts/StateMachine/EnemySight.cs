using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;
// Đảm bảo rằng Box Collider 2D có thuộc tính Is Trigger được bật
// Đảm bảo rằng đã thêm Rigidbody 2D vào đối tượng này hoặc đối tượng player

private void OnTriggerEnter2D(Collider2D collision)
{
    // Kiểm tra tag của đối tượng va chạm
    if(collision.tag == "Player")
    {
        // Gọi phương thức SetTarget với tham chiếu đến PlayerInfo
        enemy.SetTarget(collision.GetComponent<PlayerInfo>());
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    if (collision.tag == "Player")
    {
        Debug.Log("Player lost");
        enemy.SetTarget(null);
    }
}


}
