using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetActiveF", 1.33f);
    }

    void SetActiveF()
    {
        gameObject.SetActive(false);
    }
}
