using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy2 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide;
    int bullTime;//发弹计数器
    float speedX1,speedX2, speedY;
    int dir;
    int bullTimeLimit;
    GameObject AIBull;
    GameObject AI2 = null;
    // Start is called before the first frame update
    void Start()
    {
        bullTime = 0;
        bullTimeLimit = 60;
        speedX1 = -0.8f;
        speedX2 = 0.8f;
        speedY = -0.8f;
        downSide = 2.8f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        dir = 0;
        AIBull = GameObject.Find("AIBull2");
        AI2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        AI2.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        AI2.AddComponent<Enemy2Info>();
        AI2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/enemy2");
        AI2.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        AI2.name = "AI2";
        AI2.transform.position = new Vector3(Random.Range(leftSide, rightSide), upSide+0.1f, 0.92f);
        AI2.transform.rotation = GameObject.Find("Player").transform.rotation;
        while(AI2.transform.position.y > upSide)
            AI2.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (AI2 != null)
        {
            bullTime++;
            if (bullTime == bullTimeLimit && (Random.Range(1, 10) <= 9))
            {
                Vector3 position = AI2.transform.position;
                position.y -= 0.2f;
                position.x -= 0.2f;
                GameObject bullet1 = Instantiate(AIBull, position, AIBull.transform.rotation);
                bullet1.GetComponent<Rigidbody>().velocity = new Vector3(speedX1, speedY, 0);
                Destroy(bullet1, 4);
                position.x += 0.4f;
                GameObject bullet2 = Instantiate(AIBull, position, AIBull.transform.rotation);
                bullet2.GetComponent<Rigidbody>().velocity = new Vector3(speedX2, speedY, 0);
                Destroy(bullet2, 4);
                bullTime = 0;
                bullTimeLimit = Random.Range(50, 70);
            }
            if (dir == 0 && AI2.transform.position.y <= downSide)
            {
                dir = Random.Range(0, 100)%4;
            }
            else if (dir == 1 && AI2.transform.position.y >= upSide)
            {
                dir = Random.Range(0, 100)%4;
            }
            else if(dir == 2 && AI2.transform.position.x <= leftSide)
            {
                dir = Random.Range(0, 100)%4;
            }
            else if(dir == 3 && AI2.transform.position.x >= rightSide)
            {
                dir = Random.Range(0, 100)%4;
            }
            switch (dir)
            {
                case 0:
                    if(AI2.transform.position.y > downSide)
                        AI2.transform.Translate(Vector3.forward * 0.01f);
                    break;
                case 1:
                    if(AI2.transform.position.y < upSide)
                        AI2.transform.Translate(-Vector3.forward * 0.01f);
                    break;
                case 2:
                    if(AI2.transform.position.x > leftSide)
                        AI2.transform.Translate(Vector3.right * 0.01f);
                    break;
                case 3:
                    if(AI2.transform.position.x < rightSide)
                        AI2.transform.Translate(-Vector3.right * 0.01f);
                    break;
            }
        }
    }
}
