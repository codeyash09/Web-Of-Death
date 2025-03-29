using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderBossScript : MonoBehaviour
{
    Animator myAnim;

    public bool runAfterPlayer;
    public float timer;
    float missionComplete;
    public Transform player;
    public Rigidbody rb;
    public float speed = 50;
    public GameObject[] webSpots;
    public GameObject web;
    float webSpeed = 300f;
    public Slider hp;

    Vector3 opPos;

    public static float bossHealth = 500f;

    public GameObject target;
    public static bool unableToMove;
    int index;

    bool attackChosen;
    float attack;
    List<Vector2> zones = new List<Vector2>();
    public GameObject dangerZone;

    Vector3 pPos;
    float topsHp = 0;


    void Start()
    {
        bossHealth = 500f;
        topsHp = bossHealth;
        timer = 0;
        missionComplete = 0;
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        int j = Random.Range(0, webSpots.Length - 1);
        target = webSpots[j];
        index = j;
        attackChosen = false;
        unableToMove = false;
    }


    void FixedUpdate()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 30f)
        {
            Drive.health -= 0.1f;
        }

        hp.value = bossHealth / topsHp;
        timer += Time.fixedDeltaTime;
        if(timer >= 30f)
        {
            unableToMove = false;

            float rand = Random.Range(0, 1000000);
            
            if (rand > 1000000/2)
            {
                runAfterPlayer = true;
                
            }
            else
            {
                runAfterPlayer = false;
                int j = Random.Range(0, webSpots.Length - 1);
                target = webSpots[j];
                index = j;

            }

            missionComplete = 0;
            timer = 0;
            
        }

        if (runAfterPlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 10f)
            {
                myAnim.SetBool("isRunning", true);
                myAnim.SetBool("isAttacking", false);

                transform.LookAt(player);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                rb.velocity = (new Vector3(transform.forward.x, 0, transform.forward.z).normalized * speed/2) + new Vector3(0, rb.velocity.y, 0);
            }

            if(Vector3.Distance(transform.position, player.transform.position) < 100f && !attackChosen)
            {
                float rand = Random.Range(0, 1000000);

                
                if (rand > (800000))
                {
                    JumpAttack();
                    attackChosen = true;
                    attack = 1;

                }
                else if(rand > (1000000 * (1 / 3)))
                {
                    pPos = player.transform.position;
                    WebAttack();
                    attackChosen = true;
                    attack = 2;

                }
                else
                {
                    attackChosen = true;
                    attack = 3;


                }
            }

            if(attack == 3)
            {
                myAnim.SetBool("isRunning", true);
                myAnim.SetBool("isAttacking", false);

                transform.LookAt(player);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                rb.velocity = (new Vector3(transform.forward.x, 0, transform.forward.z).normalized * speed/2) + new Vector3(0, rb.velocity.y, 0);

                if(Vector3.Distance(transform.position, player.transform.position) < 10f)
                {
                    attackChosen = false;
                    timer = 30;
                    Drive.health -= 10f;
                }
            }

        } else
        {
            if (target.transform.position.y > transform.position.y)
            {
                rb.AddForce(0, 10f, 0);
            }
            if (missionComplete == 0)
            {
                myAnim.SetBool("isRunning", false);
                myAnim.SetBool("isAttacking", true);


                transform.LookAt(target.transform);

                FireWeb(target);

                rb.velocity = transform.forward * webSpeed;
                
                if(Vector3.Distance(target.transform.position, transform.position) < 15f)
                {
                    transform.position = target.transform.position;
                }

                if (Vector3.Distance(target.transform.position, transform.position) < 5f)
                {
                    missionComplete = 1;
                    float rand = Random.Range(0, 1000000);
                    rb.velocity = Vector3.zero;
                    if (rand > 1000000 / 2)
                    {
                        int j = index + 1;
                        if (j > webSpots.Length-1)
                        {
                            j = index - 1;

                        }
                        target = webSpots[j];
                        index = j;

                    }
                    else
                    {

                        int j = index - 1;
                        if(j < 0)
                        {
                            j = index + 1;

                        }
                        target = webSpots[j];
                        index = j;

                    }
                }
            }
            else
            {
                transform.LookAt(target.transform.position);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

                myAnim.SetBool("isWalking", true);
                myAnim.SetBool("isAttacking", false);
                rb.velocity = (new Vector3(transform.forward.x, 0, transform.forward.z).normalized * (speed)) + new Vector3(0, rb.velocity.y, 0);
               
                if(Vector2.Distance(new Vector2(target.transform.position.x, target.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < 50f)
                {
                    transform.position = target.transform.position;

                }
                if ((new Vector2(transform.position.x, transform.position.z) - new Vector2(target.transform.position.x, target.transform.position.z)).magnitude < 5f)
                {
                    float rand = Random.Range(0, 1000000);
                    rb.velocity = Vector3.zero;
                    if (rand > 1000000 / 2)
                    {
                        int j = index + 1;
                        if (j > webSpots.Length - 1)
                        {
                            j = index - 1;

                        }
                        target = webSpots[j];
                        index = j;

                    }
                    else
                    {

                        int j = index - 1;
                        if (j < 0)
                        {
                            j = index + 1;

                        }
                        target = webSpots[j];
                        index = j;

                    }
                }

            }
        }



    }

    public void FireWeb(GameObject targetPoint)
    {
        GameObject websicle = Instantiate(web, transform);
        websicle.transform.localEulerAngles = new Vector3(90f, 0, 0);
        websicle.transform.parent = null;

        
        websicle.transform.parent = null;

        websicle.transform.localScale = new Vector3(0.5f, Vector3.Distance(transform.position, targetPoint.transform.position)/10f, 0.5f);
        websicle.transform.parent = null;
        websicle.transform.position = transform.position;
        

    }

    public void FireWeb(Vector3 pos)
    {
        GameObject websicle = Instantiate(web, transform);
        websicle.transform.localEulerAngles = new Vector3(90f, 0, 0);
        websicle.transform.parent = null;


        websicle.transform.parent = null;

        websicle.transform.localScale = new Vector3(0.5f, Vector3.Distance(transform.position, pos) / 10f, 0.5f);
        websicle.transform.parent = null;
        websicle.transform.position = transform.position;


    }


    void JumpAttack()
    {
        opPos = transform.position;
        transform.position = new Vector3(0, 100000, 0);

        float dangerZones = Random.Range(20, 30);
        zones.Clear();
        for(int i = 0; i < dangerZones; i++)
        {
            zones.Add(new Vector2(Random.Range(-400, 400), Random.Range(-400, 400)));
        }


        for (int i = 0; i < zones.Count - 1; i++)
        {
            GameObject dZone = Instantiate(dangerZone, transform);
            dZone.transform.parent = null;
            dZone.transform.position = new Vector3(zones[i].x, 0, zones[i].y);
            dZone.transform.localEulerAngles = Vector3.zero;

        }
        StartCoroutine(Zoner());

        
    }

    public IEnumerator Zoner()
    {
        yield return new WaitForSeconds(6f);
        transform.position = opPos;
        attackChosen = false;
    }

    public void WebAttack()
    {


        transform.LookAt(pPos);

        FireWeb(pPos);

        rb.velocity = transform.forward * speed;

        if (Vector3.Distance(pPos, transform.position) < 5f)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 10f)
            {
                Drive.health -= 20f;
                unableToMove = false;
                timer = 30f;
            }
            else
            {
                pPos = player.transform.position;
                WebAttack();
                attackChosen = true;
                attack = 2;
            }

        }
        

        StartCoroutine(FreezeOp());



    }

    public IEnumerator FreezeOp()
    {
        yield return new WaitForSeconds(3f);

        if (Vector3.Distance(pPos, player.transform.position) < 30f)
        {
            unableToMove = true;

        }
        else
        {
            unableToMove = false;

        }
    }

}
