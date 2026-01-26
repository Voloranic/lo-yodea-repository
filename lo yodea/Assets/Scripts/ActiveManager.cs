using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    [Header("Object !In activation order!")] 
    [SerializeField] GameObject[] objects;
    [Header("int !same pos as object! from small to big!")]
    [SerializeField] float[] Xactivation;
    [SerializeField] Transform player;
    [SerializeField] int range;
    float x;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(OptimizationTick());
    }

    IEnumerator OptimizationTick()
    {
        while (true)
        {
            x = transform.position.x;
            check();
            // Wait for 0.1 seconds (10 times per second)
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
  /*  void FixedUpdate()
    {

    }
  */
    private void check()
    {
        bool disableRight = false;
        bool disableLeft = false;
        for(int i = 0; i < Xactivation.Length; i++)
        {
            if (x > Xactivation[i] + range && !disableRight)
            {
                if(objects[i].activeSelf) objects[i].SetActive(false);
                disableRight = true;
            }
            else if (x < Xactivation[i] - range && !disableLeft)
            {
                if (objects[i].activeSelf) objects[i].SetActive(false);
                disableLeft = true;
            }
            else
            {
                if (!objects[i].activeSelf) objects[i].SetActive(true);
            }

        }
    }
   
}
