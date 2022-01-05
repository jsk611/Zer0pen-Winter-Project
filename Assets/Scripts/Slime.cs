using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour
{
    bool input;
    Vector2 touchPosition;
    public GameManager gameManager;

    private void Start()
    {
        
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            input = true;
        }
        else
            input = false;

        if (!input)
            return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(touchPosition);
        Ray2D ray = new Ray2D(pos, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit.collider != null && hit.collider.tag == "Slime")
        {
            Debug.Log("+1¿ø");
            gameManager.coin++;
        }
    }


}
