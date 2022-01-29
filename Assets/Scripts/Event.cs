using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject rain;
    [SerializeField] GameObject wetArea;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TriggerEvent", 10f, 30f);
    }

    void TriggerEvent()
    {
        int rand = Random.Range(0,3);
        switch(rand)
        {
            case 0:
                StartCoroutine(HunterEvent());
                break;
            case 1:
                StartCoroutine(Rain());
                break;
            default:
                break;
        }
    }

    IEnumerator HunterEvent()
    {
        for(int i=0; i<Random.Range(2,6); i++)
        {
            int rand = Random.Range(0, 2);
            if(rand == 0)
                Instantiate(laser, points[rand].position, Quaternion.Euler(0, 0, Random.Range(-45,46)));
            else
                Instantiate(laser, points[rand].position, Quaternion.Euler(0, 0,180 + Random.Range(-45, 46)));
            yield return new WaitForSeconds(6f);

        }
    }

    IEnumerator Rain()
    {
        GameObject r = Instantiate(rain);
        Destroy(r, 21f);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Instantiate(wetArea,new Vector2(Random.Range(-2.5f,2.5f),Random.Range(-2.5f, 2.5f)), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

    }

    


}
