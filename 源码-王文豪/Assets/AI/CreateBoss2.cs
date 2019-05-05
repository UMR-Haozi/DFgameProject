using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoss2 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide, midHSide;
    int bullTime;  //发弹计数器
    float speedY;
    int dir;  //BOSS位置标记,0左上角，1中间，2右上角，3从右上角返回的中间。
    int bullTimeLimit;
    float bullDirTime; //炮弹方向计时
    float bullAngle; //炮弹的角度
    float stayTime; //Boss停留计数器
    float stayTimeLimit; //Boss停留的时间
    GameObject BossBull;
    GameObject Boss2 = null;
    // Start is called before the first frame update
    void Start()
    {
        stayTime = 0;
        stayTimeLimit = 3;
        bullTime = 0;
        bullTimeLimit = 10;
        bullDirTime = 0;
        bullAngle = 0.00001f;
        speedY = -0.6f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        midHSide = (leftSide + rightSide) / 2;
        dir = 0;
        BossBull = GameObject.Find("BossBull2");
        Boss2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Boss2.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        Boss2.AddComponent<Boss2Info>();
        Boss2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/Boss2");
        Boss2.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        Boss2.name = "Boss2";
        Boss2.transform.position = new Vector3(leftSide, upSide+0.1f, 0.92f);
        Boss2.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (Boss2.transform.position.y > upSide)
            Boss2.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Boss2 != null)
        {
            //Boss在左上角
            if (dir == 0 && Boss2.transform.position.x <= leftSide)
            {
                bullDirTime += Time.deltaTime;
                if (bullDirTime >= 0.1)
                {
                    bullAngle += 0.05f;
                    bullDirTime = 0;
                }
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //从上往下扫
                    Vector3 position = Boss2.transform.position;
                    float yOffset = Mathf.Sin(bullAngle) * 0.1f;
                    float xOffset = Mathf.Cos(bullAngle) * 0.1f;
                    position.y -= yOffset;
                    position.x += xOffset;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet, 5);
                    //从下往上扫
                    position = Boss2.transform.position;
                    yOffset = Mathf.Cos(bullAngle) * 0.1f;
                    xOffset = Mathf.Sin(bullAngle) * 0.1f;
                    position.y -= yOffset;
                    position.x += xOffset;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(-speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet1, 5);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if (stayTime >= 4)
                {
                    dir = 1;
                    bullTime = 0;
                    stayTime = 0;
                    bullDirTime = 0;
                    bullAngle = 0;
                }
            }
            //Boss在中间
            if ((dir == 1 && Boss2.transform.position.x >= midHSide) ||
                (dir == 3 && Boss2.transform.position.x <= midHSide))
            {
                bullDirTime += Time.deltaTime;
                if (bullDirTime >= 0.1)
                {
                    bullAngle += 0.05f;
                    bullDirTime = 0;
                }
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //从下往左扫
                    Vector3 position = Boss2.transform.position;
                    float yOffset = Mathf.Cos(bullAngle) * 0.1f;
                    float xOffset = Mathf.Sin(bullAngle) * 0.1f;
                    position.y += yOffset;
                    position.x += xOffset;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet, 5);
                    //从下往右扫
                    position = Boss2.transform.position;
                    yOffset = Mathf.Cos(bullAngle) * 0.1f;
                    xOffset = Mathf.Sin(bullAngle) * 0.1f;
                    position.y -= yOffset;
                    position.x -= xOffset;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet1, 5);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if (stayTime >= 4)
                {
                    dir = (dir+1)%4;
                    bullTime = 0;
                    stayTime = 0;
                    bullDirTime = 0;
                    bullAngle = 0;
                }
            }
            //Boss在右边
            if (dir == 2 && Boss2.transform.position.x >= rightSide)
            {
                bullDirTime += Time.deltaTime;
                if (bullDirTime >= 0.1)
                {
                    bullAngle += 0.05f;
                    bullDirTime = 0;
                }
                bullTime++;
                if (bullTime == bullTimeLimit)
                {
                    //从上往下扫
                    Vector3 position = Boss2.transform.position;
                    float yOffset = Mathf.Sin(bullAngle) * 0.1f;
                    float xOffset = Mathf.Cos(bullAngle) * 0.1f;
                    position.y -= yOffset;
                    position.x -= xOffset;
                    GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet, 5);
                    //从下往上扫
                    position = Boss2.transform.position;
                    yOffset = Mathf.Cos(bullAngle) * 0.1f;
                    xOffset = Mathf.Sin(bullAngle) * 0.1f;
                    position.y -= yOffset;
                    position.x -= xOffset;
                    GameObject bullet1 = Instantiate(BossBull, position, BossBull.transform.rotation);
                    bullet1.GetComponent<Rigidbody>().velocity = new Vector3(speedY * xOffset / yOffset, speedY, 0);
                    Destroy(bullet1, 5);
                    bullTime = 0;
                }
                stayTime += Time.deltaTime;
                if (stayTime >= 4)
                {
                    dir = 3;
                    bullTime = 0;
                    stayTime = 0;
                    bullDirTime = 0;
                    bullAngle = 0;
                }
            }
            if (bullAngle == 1.5) bullAngle = 0;


            switch (dir)
            {
                case 0:
                    if (Boss2.transform.position.x > leftSide)
                        Boss2.transform.Translate(new Vector3(1f, 0, -0.2f) * 0.02f);
                    break;
                case 1:
                    if (Boss2.transform.position.x < midHSide)
                        Boss2.transform.Translate(new Vector3(-1f, 0, 0.2f) * 0.02f);
                    break;
                case 2:
                    if (Boss2.transform.position.x < rightSide)
                        Boss2.transform.Translate(new Vector3(-1f, 0, -0.2f) * 0.02f);
                    break;
                case 3:
                    if (Boss2.transform.position.x > midHSide)
                        Boss2.transform.Translate(new Vector3(1f, 0, 0.2f) * 0.02f);
                    break;
            }
        }
    }
}
