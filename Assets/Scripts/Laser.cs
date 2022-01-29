using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SummonBullet", 2f);
    }

    void SummonBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
