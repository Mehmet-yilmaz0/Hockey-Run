using UnityEngine;

public class Event : MonoBehaviour
{
    public EventManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GameManager")
        {
            manager.DestroyLastEvent();
        }
        if(collision.gameObject.tag == "Player")
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameManager")
        {
            manager.DestroyLastEvent();
        }
    }
}
