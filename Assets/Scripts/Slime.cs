using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour
{
    public int slimeLevel;
    [SerializeField]
    Sprite[] sprites;

    bool input;
    Vector2 touchPosition;
    public GameManager gameManager;

    int moveDir;
    float speed;

    int earn;
    int maxTouchCnt = 10;
    bool overload;
    int touchCnt;
    public SpriteRenderer spr;

    [SerializeField]
    GameObject coin;
    TextMesh coinT;
    [SerializeField]
    GameObject dna;
    TextMesh dnaT;

    float t = 0;
    float startPosx;
    float startPosY;
    [SerializeField]
    bool isBeingHeld = false;
    bool a;
    public bool isInLine;
    float timelinePosY;
    [SerializeField]
    GameObject fusionEntity;

    private void Start()
    {
        
        touchCnt = 0;
        StartCoroutine(ProduceDelay());
        StartCoroutine(ChangeMove());
    }

  

    void SlimeShape(int level)
    {
        switch(level)
        {
            case 1:
                spr.sprite = sprites[0];
                maxTouchCnt = 10;
                earn = 1;
                
                //autoProduceTime = 2;
                break;
            case 2:
                spr.sprite = sprites[1];
                maxTouchCnt = 20;
                earn = 5;
                //autoProduceTime = 1;
                break;
            case 3:
                spr.sprite = sprites[2];
                maxTouchCnt = 30;
                earn = 10;
                //autoProduceTime = 0.5f;
                break;
        }
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
            if(hit.collider != null && hit.collider.gameObject == gameObject) //슬라임을 클릭했을때
            {
                int rn = Random.Range(1, 101);
                if(rn == 1)
                {
                    //DNA 추가
                    gameManager.DNA++;
                    EarningEffect(1, 1);
                }
                else
                {
                    EarningEffect(0, earn);
                    gameManager.coin += earn;
                    touchCnt++;

                }
            }
        }
        if(touchCnt >= maxTouchCnt && !overload)
        {
            overload = true;
            
            StartCoroutine(Delay());
        }



        

        if (a)
        {
            t += Time.deltaTime;
            if (t >= 1)
                isBeingHeld = true;
        }
        

        if (isBeingHeld)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.position = new Vector2(mousePos.x - startPosx, mousePos.y - startPosY);
            
        }

        if (fusionEntity != null && fusionEntity.GetComponent<Slime>().slimeLevel == slimeLevel
            && isBeingHeld == false)
        {
            //합체
            Debug.Log("합체");
            Destroy(fusionEntity);
            slimeLevel += 1;
            fusionEntity = null;
        }

        SlimeShape(slimeLevel);
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

    IEnumerator ChangeMove() //슬라임 움직임 변화
    {
        while(true)
        {
            moveDir = Random.Range(-1, 2);
            speed = Random.Range(0.5f, 1.5f);
            float m = Random.Range(1.5f, 3.5f);
            yield return new WaitForSeconds(m);

        }
       
    }

    IEnumerator Delay() //슬라임 생산 과부하
    {
        spr.color = new Color(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(5);
        touchCnt = 0;
        spr.color = new Color(1f, 1f, 1f);
        overload = false;
    }

    IEnumerator ProduceDelay() // 자동 DNA 생산
    {
        while(true)
        {
            yield return new WaitForSeconds(gameManager.autoProduceTime);
            gameManager.DNA++;
            EarningEffect(1, 1);
        }
        
    }

    void EarningEffect(int type, int num)
    {
        if(type == 0)
        {
            coinT = Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponentInChildren<TextMesh>(); // 골드 추가 효과
            coinT.text = "+" + num.ToString();
        }
        else if(type == 1)
        {
            dnaT = Instantiate(dna, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponentInChildren<TextMesh>(); // 골드 추가 효과
            dnaT.text = "+" + num.ToString();
        }
    }



    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, .5f);
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            startPosx = mousePos.x - transform.position.x;
            startPosY = mousePos.y - transform.position.y;

            a = true;
            //t = 0;
            //isBeingHeld = true;

        }
    }

    private void OnMouseUp()
    {
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);
        isBeingHeld = false;
        a = false;
        t = 0;
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isBeingHeld == true && collision.CompareTag("Slime"))
        {
            fusionEntity = collision.gameObject;
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.3f);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBeingHeld == true && collision.CompareTag("Slime"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);
            fusionEntity = null;
        }
    }

    

}
