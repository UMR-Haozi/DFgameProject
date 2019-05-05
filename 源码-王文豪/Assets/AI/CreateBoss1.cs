using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoss1 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide,midHSide;
    int bullTime;  //发弹计数器
    float speedY;
    int dir;  //BOSS位置标记,0左上角，1中间，2右上角，3从右上角返回的中间。
    int bullTimeLimit;
    int bullCount; //炮弹计数
    float stayTime; //Boss停留计数器
    float stayTimeLimit; //Boss停留的时间
    GameObject BossBull;
    GameObject Boss1 = null;
    // Start is called before the first frame update
    void Start()
    {
        stayTime = 0;
        stayTimeLimit = 3;
        bullCount = 5;
        bullTime = 0;
        bullTimeLimit = 10;
        speedY = -1f;
        downSide = 3.0f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        midHSide = (leftSide + rightSide) / 2;
        dir = 0;
        BossBull = GameObject.Find("BossBull1");
        Boss1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Boss1.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        Boss1.AddComponent<Boss1Info>();
        Boss1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/Boss1");
        Boss1.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        Boss1.name = "Boss1";
        Boss1.transform.position = new Vector3(leftSide, upSide+0.1f, 0.92f);
        Boss1.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (Boss1.transform.position.y > upSide)
            Boss1.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Boss1 != null)
        {
            //Boss在左上角
            if(dir == 0 && Boss1.transform.position.x <= leftSide)
            {
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //第一排子弹
                    Vector3 position = Boss1.transform.position;
                    position.y -= 0.1f;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speedY, 0);
                    Destroy(bullet, 4);
                    //第二排子弹
                    position.x += 0.1f;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(-speedY, speedY, 0);
                    Destroy(bullet1, 4);
                    //第三排子弹
                    position.x += 0.1f;
                    GameObject bullet2 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet2.GetComponent<Rigidbody>().velocity = new Vector3(-speedY*2, speedY, 0);
                    Destroy(bullet2, 4);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if(stayTime >= 3)
                {
                    dir = 1;
                    bullTime = 0;
                    stayTime = 0;
                }
            }
            //Boss在中间
            if ((dir == 1 && Boss1.transform.position.x >= midHSide) ||
                (dir == 3 && Boss1.transform.position.x <= midHSide))
            {
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //第一排子弹
                    Vector3 position = Boss1.transform.position;
                    position.y -= 0.1f;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speedY, 0);
                    Destroy(bullet, 4);
                    //第二排子弹
                    position.x += 0.1f;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(-speedY, speedY, 0);
                    Destroy(bullet1, 4);
                    //第三排子弹
                    position.x -= 0.2f;
                    GameObject bullet2 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet2.GetComponent<Rigidbody>().velocity = new Vector3(speedY, speedY, 0);
                    Destroy(bullet2, 4);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if (stayTime >= 3)
                {
                    dir = (dir == 1)?2:0;
                    bullTime = 0;
                    stayTime = 0;
                }
            }
            //Boss在右边
            if (dir == 2 && Boss1.transform.position.x >= rightSide)
            {
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //第一排子弹
                    Vector3 position = Boss1.transform.position;
                    position.y -= 0.1f;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speedY, 0);
                    Destroy(bullet, 4);
                    //第二排子弹
                    position.x -= 0.1f;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(speedY, speedY, 0);
                    Destroy(bullet1, 4);
                    //第三排子弹
                    position.x -= 0.1f;
                    GameObject bullet2 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet2.GetComponent<Rigidbody>().velocity = new Vector3(speedY*2, speedY, 0);
                    Destroy(bullet2, 4);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if (stayTime >= 3)
                {
                    dir = 3;
                    bullTime = 0;
                    stayTime = 0;
                }
            }



            switch (dir)
            {
                case 0:
                    if (Boss1.transform.position.x > leftSide)
                            Boss1.transform.Translate(new Vector3(1f,0,-0.2f) * 0.01f);
                    break;
                case 1:
                    if (Boss1.transform.position.x < midHSide)
                            Boss1.transform.Translate(new Vector3(-1f,0,0.2f) * 0.01f);
                    break;
                case 2:
                    if (Boss1.transform.position.x < rightSide)
                            Boss1.transform.Translate(new Vector3(-1f,0,-0.2f) * 0.01f);
                    break;
                case 3:
                    if (Boss1.transform.position.x > midHSide)
                        Boss1.transform.Translate(new Vector3(1f, 0, 0.2f) * 0.01f);
                    break;
            }
        }
    }
}
