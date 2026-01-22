using System;
using UnityEngine;

public class childAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 position;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = position;
    }
    public void setPos(Vector2 position)
    {
        transform.SetParent(null);
        this.position = position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void ReturnParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    
}
