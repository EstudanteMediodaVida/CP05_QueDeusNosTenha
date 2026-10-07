using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
