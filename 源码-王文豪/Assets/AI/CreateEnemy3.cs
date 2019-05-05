using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy3 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide;
    int bullTime; //发弹计数器
    float speedY;
    int dir;
    int bullTimeLimit;
    int bullCount; //炮弹计数
    int dirTime; //方向改变计数器
    GameObject AIBull;
    GameObject AI3 = null;
    // Start is called before the first frame update
    void Start()
    {
        bullCount = 5;
        bullTime = 0;
        bullTimeLimit = 60;
        speedY = -0.8f;
        downSide = 2.7f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        dir = 0;
        dirTime = 0;
        AIBull = GameObject.Find("AIBull3");
        AI3 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        AI3.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        AI3.AddComponent<Enemy3Info>();
        AI3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/enemy3");
        AI3.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        AI3.name = "AI3";
        AI3.transform.position = new Vector3(Random.Range(leftSide, rightSide), upSide+0.1f, 0.92f);
        AI3.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (AI3.transform.position.y > upSide)
            AI3.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (AI3 != null)
        {
            bullTime++;
            if (bullTime == bullTimeLimit && (Random.Range(1, 10) <= 9))
            {
                bullCount--;
                Vector3 position = AI3.transform.position;
                position.y -= 0.1f;
                position.x += 0.05f * bullCount;
                GameObject bullet = Instantiate(AIBull, position, AIBull.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speedY * 0.5f * bullCount, speedY, 0);
                Destroy(bullet, 4);
                bullTime = 0;
                bullTimeLimit = Random.Range(50, 70);
            }
            if (bullCount == -5) bullCount = 5;
            dirTime++;
            if(dirTime == 100)
            {
                dir = Random.Range(0, 100) % 4;
                dirTime = 0;
            }
            
            switch (dir)
            {
                case 0:
                    if (AI3.transform.position.y > downSide)
                        AI3.transform.Translate(Vector3.forward * 0.01f);
                    break;
                case 1:
                    if (AI3.transform.position.y < upSide)
                        AI3.transform.Translate(-Vector3.forward * 0.01f);
                    break;
                case 2:
                    if (AI3.transform.position.x > leftSide)
                        AI3.transform.Translate(Vector3.right * 0.01f);
                    break;
                case 3:
                    if (AI3.transform.position.x < rightSide)
                        AI3.transform.Translate(-Vector3.right * 0.01f);
                    break;
            }
        }
    }
}
