using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    [SerializeField] Transform player;

    [Header("objects")]
    [SerializeField] GameObject[] objects;
    [SerializeField] float[] Xactivation;
    [SerializeField] int range;

    [Header("Animals")]
    [SerializeField] GameObject[] animals;
    [SerializeField] float[] AXactivation;
    [SerializeField] int Arange;

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
            x = player.position.x;
            check(Xactivation, objects);
            check(AXactivation, animals);

            // Wait for 0.1 seconds (10 times per second)
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    /*
    void FixedUpdate()
    {

    }
    */

    private void check(float[] pos, GameObject[] ob)
    {
        bool disableRight = false;
        bool disableLeft = false;

        for (int i = 0; i < ob.Length; i++)
        {
            if (x > pos[i] + range && !disableRight)
            {
                if (ob[i].activeSelf) ob[i].SetActive(false);
                disableRight = true;
            }
            else if (x < pos[i] - range && !disableLeft)
            {
                if (ob[i].activeSelf) ob[i].SetActive(false);
                disableLeft = true;
            }
            else
            {
                if (!ob[i].activeSelf) ob[i].SetActive(true);
            }
        }
    }
}
