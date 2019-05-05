using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoss3 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide, midHSide;
    int bullTime;  //发弹间隔计数器
    int bullTimeLimit; //发弹间隔时间
    float allBullTime; //总发弹时间计数器
    float allBullTimeLimit; //总发弹时间长度
    float stopBullTime; //停止发弹时间计数器
    float stopBullTimeLimit; //停止发弹时间长度
    float speed;
    int dir;  //BOSS位置标记,0左上角，1中间，2右上角，3从右上角返回的中间。
    float bullAngle; //炮弹的角度
    float stayTime; //Boss停留计数器
    float stayTimeLimit; //Boss停留的时间
    float runTime; //Boss行走计时
    float runTimeLimit; //Boss行走的时间限制
    GameObject BossBull;
    GameObject Boss3 = null;
    // Start is called before the first frame update
    void Start()
    {
        stayTime = 0;
        stayTimeLimit = 1;
        bullTime = 15;
        bullTimeLimit = 20;
        bullAngle = -0.1f;
        runTime = 2.1f;
        runTimeLimit = 2;
        allBullTime = 3.1f;
        allBullTimeLimit = 3;
        stopBullTime = 0;
        stopBullTimeLimit = 1.0f;
        speed = 1.2f;
        leftSide = -1.8f;
        upSide = 3.76f;
        downSide = 2.80f;
        rightSide = 2.0f;
        dir = 0;
        BossBull = GameObject.Find("BossBull3");
        Boss3 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Boss3.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        Boss3.AddComponent<Boss3Info>();
        Boss3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/Boss3");
        Boss3.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        Boss3.name = "Boss3";
        Boss3.transform.position = new Vector3(Random.Range(leftSide,rightSide), upSide+0.1f, 0.92f);
        Boss3.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (Boss3.transform.position.y > upSide)
            Boss3.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Boss3 != null)
        {
            //发弹控制
            if (allBullTime <= allBullTimeLimit)
            {
                allBullTime += Time.deltaTime;
                Fire();
            }
            else
            {
                stopBullTime += Time.deltaTime;
                if(stopBullTime >= stopBullTimeLimit)
                {
                    stopBullTime = 0;
                    allBullTime = 0;
                    stopBullTimeLimit = Random.Range(1.0f, 2.0f);
                    allBullTimeLimit = Random.Range(2.0f, 3.0f);
                }
            }
            //移动控制
            if(runTime <= runTimeLimit)
            {
                runTime += Time.deltaTime;
                switch (dir)
                {
                    case 0:
                        if (Boss3.transform.position.x < rightSide && Boss3.transform.position.y > downSide)
                            Boss3.transform.Translate(new Vector3(-1f, 0, 0.5f) * 0.025f);
                        break;
                    case 1:
                        if (Boss3.transform.position.x > leftSide && Boss3.transform.position.y > downSide)
                            Boss3.transform.Translate(new Vector3(1f, 0, 0.5f) * 0.025f);
                        break;
                    case 2:
                        if (Boss3.transform.position.x < rightSide)
                            Boss3.transform.Translate(-Vector3.right * 0.025f);
                        break;
                    case 3:
                        if (Boss3.transform.position.y < upSide)
                            Boss3.transform.Translate(-Vector3.forward * 0.025f);
                        break;
                    case 4:
                        if (Boss3.transform.position.x < rightSide && Boss3.transform.position.y < upSide)
                            Boss3.transform.Translate(new Vector3(-1f, 0, -0.5f) * 0.025f);
                        break;
                    case 5:
                        if (Boss3.transform.position.y > downSide)
                            Boss3.transform.Translate(Vector3.forward * 0.025f);
                        break;
                    case 6:
                        if (Boss3.transform.position.x > leftSide)
                            Boss3.transform.Translate(Vector3.right * 0.025f);
                        break;
                    case 7:
                        if (Boss3.transform.position.x > leftSide && Boss3.transform.position.y < upSide)
                            Boss3.transform.Translate(new Vector3(1f, 0, -0.5f) * 0.025f);
                        break;
                }
            }
            else
            {
                stayTime += Time.deltaTime;
                if (stayTime >= stayTimeLimit)
                {
                    stayTime = 0;
                    runTime = 0;
                    stayTimeLimit = Random.Range(0.5f, 1.0f);
                    runTimeLimit = Random.Range(1.0f, 2.0f);
                    dir = (dir + 1) % 8;
                }
            }
        }
    }
    void Fire()
    {
        bullAngle += 0.1f;
        bullTime++;
        if (bullTime == bullTimeLimit)
        {
            //仙女散花弹幕
            float bullA = 0.000001f + bullAngle;
            for (int i = 0; i < 8; ++i, bullA += Mathf.PI / 4)
            {
                Vector3 position = Boss3.transform.position;
                float yOffset = Mathf.Cos(bullA) * 0.1f;
                float xOffset = Mathf.Sin(bullA) * 0.1f;
                position.y += yOffset;
                position.x += xOffset;
                GameObject bullet = Instantiate(BossBull, position, BossBull.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(speed * Mathf.Sin(bullA), speed * Mathf.Cos(bullA), 0);
                Destroy(bullet, 5);
            }
            bullTime = 0;
        }
        if (bullAngle >= 6.28) bullAngle = 0;
    }
}
