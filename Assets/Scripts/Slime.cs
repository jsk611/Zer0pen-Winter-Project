using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour
{
    bool input;
    Vector2 touchPosition;
    public GameManager gameManager;

    bool stay;
    int moveDir;
    float speed;

    bool overload;
    int touchCnt;
    public SpriteRenderer spr;


    private void Start()
    {
        touchCnt = 0;
    }
    private void Update()
    {
        //슬라임 터치 시 골드 증가
        if(Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            input = true;
        }
        else
            input = false;

        if (input && !overload)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(touchPosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("+1원");
                gameManager.coin++;
                touchCnt++;
            }
        }
        if(touchCnt >= 10 && !overload)
        {
            overload = true;
            
            StartCoroutine(Delay());
        }

        //슬라임 움직임
        if(!stay) // 2.5초마다 슬라임 움직이는 방향 변경
        {
            stay = true;
            moveDir = Random.Range(-1, 2);
            speed = Random.Range(0.5f, 1.5f);
            StartCoroutine(ChangeMove());
        }


        

    }

    private void FixedUpdate() 
    {
        //슬라임 움직임
        Vector2 direction = new Vector2(moveDir, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, .5f,LayerMask.GetMask("Wall"));
        if(hit.collider != null)
        {
            Debug.Log("벽에 부딪힘");
            moveDir *= -1;
        }
    }

    IEnumerator ChangeMove() 
    {
        yield return new WaitForSeconds(2.5f);
        stay = false;
    }

    IEnumerator Delay()
    {
        spr.color = new Color(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(5);
        touchCnt = 0;
        spr.color = new Color(1f, 1f, 1f);
        overload = false;
    }


}
