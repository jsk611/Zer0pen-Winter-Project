using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour
{
    public int slimeLevel;
    [SerializeField] Sprite[] sprites;

    bool input;
    Vector2 touchPos;
    public GameManager gameManager;
    public AchieveManager achieveManager;
    [SerializeField] GameObject slimeParticle;

    float moveDirX;
    float moveDirY;
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
    public TextMesh fusionT;
    public GameObject fusionMessage;
    Animator anim;
    int fusionCost;

    float t = 0;
    float startPosx;
    float startPosY;
    [SerializeField]
    bool isBeingHeld = false;
    bool a;
    bool isCoroutineWork;
    public bool isInLine;
    float timelinePosY;
    [SerializeField]
    GameObject fusionEntity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        touchCnt = 0;
        StartCoroutine(ProduceDelay());
        StartCoroutine(ChangeMove());
        Instantiate(slimeParticle, transform.position, Quaternion.identity);
        anim.SetTrigger("levelChange");
    }

  

    void SlimeShape(int level)
    {
        
        anim.SetInteger("slimeLevel", level);
        switch (level)
        {
            case 1:
                spr.sprite = sprites[0];
                maxTouchCnt = 10;
                earn = 3;
                fusionCost = 1;
                break;
            case 2:
                spr.sprite = sprites[1];
                maxTouchCnt = 20;
                earn = 4;
                fusionCost = 2;
                break;
            case 3:
                spr.sprite = sprites[2];
                maxTouchCnt = 30;
                earn = 6;
                fusionCost = 3;
                break;
            case 4:
                maxTouchCnt = 30;
                earn = 7;
                fusionCost = 4;
                break;
            case 5:
                maxTouchCnt = 30;
                earn = 10;
                fusionCost = 5;
                break;
            case 6:
                maxTouchCnt = 35;
                earn = 12;
                fusionCost = 6;
                break;
            case 7:
                maxTouchCnt = 35;
                earn = 14;
                fusionCost = 7;
                break;
            case 8:
                maxTouchCnt = 40;
                earn = 17;
                fusionCost = 8;
                break;
            case 9:
                maxTouchCnt = 40;
                earn = 19;
                fusionCost = 9;
                break;
        }
    }
    private void Update()
    {
        //������ �ִϸ��̼�
        if(moveDirX == 0 && moveDirY == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
            anim.SetBool("isMoving", true);

        
        //������ ��ġ �� ��� ����
        if (Input.GetMouseButtonDown(0) && !overload)
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                int rn = Random.Range(1, 101);
                if (rn == 1)
                {
                    //DNA �߰�
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

        if(overload)
        {
            spr.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            spr.color = new Color(1, 1, 1);
        }


        if (a) // Ȧ�� ������
        {
            t += Time.deltaTime;
            if (t >= 0.5f)
                isBeingHeld = true;
        }
        

        if (isBeingHeld) // Ȧ�� ��
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.position = new Vector2(mousePos.x - startPosx, mousePos.y - startPosY);
            fusionMessage.SetActive(true);
            fusionT.text = fusionCost.ToString();
        }
       
            

        if (fusionEntity != null && fusionEntity.GetComponent<Slime>().slimeLevel == slimeLevel
            && isBeingHeld == false) //��ü
        {
            if (gameManager.DNA >= fusionCost)
            {
                Debug.Log("��ü");
                gameManager.DNA -= fusionCost;
                Destroy(fusionEntity);
                slimeLevel += 1;
                fusionEntity = null;
                Instantiate(slimeParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                if (!achieveManager.achieves[slimeLevel - 1])
                    achieveManager.GetAchievement(slimeLevel);

                anim.SetTrigger("levelChange");
            }
            else
            {
                fusionEntity = null;
                gameManager.warning.SetActive(false);
                gameManager.warning.SetActive(true);
            }
            
        }
        

        SlimeShape(slimeLevel);
    }

    private void FixedUpdate() 
    {
        //������ ������
        Vector2 direction = new Vector2(moveDirX, moveDirY).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, direction, .5f,LayerMask.GetMask("Wall"));
        RaycastHit2D hitY = Physics2D.Raycast(transform.position, direction, .5f,LayerMask.GetMask("WallY"));
        if(hitX.collider != null)
        {
            Debug.Log("���� �ε���");
            moveDirX *= -1;
        }
        if (hitY.collider != null)
        {
            Debug.Log("���� �ε���");
            moveDirY *= -1;
        }
    }

    IEnumerator ChangeMove() //������ ������ ��ȭ
    {
        while(true)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirX = Mathf.Abs(x) < 0.3f ? 0 : x;
            moveDirY = Mathf.Abs(y)<0.3f? 0 : y;
            speed = Random.Range(0.5f, 1.5f);
            float m = Random.Range(1.5f, 3.5f);
            yield return new WaitForSeconds(m);

        }
       
    }

    IEnumerator Delay() //������ ���� ������
    {
        isCoroutineWork = true;
        //spr.color = new Color(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(5);
        touchCnt = 0;
        //spr.color = new Color(1f, 1f, 1f);
        isCoroutineWork = false;
        overload = false;
    }

    IEnumerator ProduceDelay() // �ڵ� DNA ����
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
            coinT = Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponentInChildren<TextMesh>(); // ��� �߰� ȿ��
            coinT.text = "+" + num.ToString();
        }
        else if(type == 1)
        {
            dnaT = Instantiate(dna, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity).GetComponentInChildren<TextMesh>(); // ��� �߰� ȿ��
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

        fusionMessage.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isBeingHeld == true && collision.CompareTag("Slime") && collision.GetComponent<Slime>().slimeLevel == slimeLevel)
        {
            fusionEntity = collision.gameObject;
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.3f);
        }

        if (collision.gameObject.tag == "WetArea" && !isCoroutineWork)
        {
            overload = true;
            StartCoroutine(Delay());
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBeingHeld == true && collision.CompareTag("Slime") && collision.GetComponent<Slime>().slimeLevel == slimeLevel)
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);
            fusionEntity = null;
        }
        
    }

    

}
