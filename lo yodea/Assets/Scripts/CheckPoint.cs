using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector2 spawnPoint;
    public static int spawnIndex;
    [SerializeField] int index;
    [SerializeField] BoxCollider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(spawnIndex == index)
        {
            spawnPoint = transform.position;
            col.enabled = false;
        }
        else if(spawnIndex > index)
        {
            col.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            index++;
            spawnPoint = new Vector2(transform.position.x, transform.position.y + 1);
            col.enabled = false;
            
        }
       
    }


}
