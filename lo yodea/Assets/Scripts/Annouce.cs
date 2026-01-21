using UnityEngine;

public class Annouce : MonoBehaviour
{
    [SerializeField]GameObject[] panels;
    int times = 0;
    [SerializeField] EdgeCollider2D[] colliders;
    [SerializeField] LayerMask PLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(PLayer))
        {
            colliders[times].enabled = false;
            panels[times].SetActive(true);
            Invoke("SetPannelsOff", 5);
            times++;


        }
    }
    private void SetPanelOff()
    {
        panels[times].SetActive(false);
    }
}
