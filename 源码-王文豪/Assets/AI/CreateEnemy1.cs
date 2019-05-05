using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy1 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide;
    int bullTime;//发弹计数器
    int bullTimeLimit;
    float speedX, speedY;
    int dir;
    GameObject AIBull;
    GameObject AI1 = null;
    // Start is called before the first frame update
    void Start()
    {
        bullTime = 0;
        bullTimeLimit = 60;
        speedX = 0;
        speedY = -0.8f;
        downSide = 2.1f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        dir = 0;
        AIBull = GameObject.Find("AIBull1");
        AI1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        AI1.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        AI1.AddComponent<Enemy1Info>();
        AI1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/enemy1");
        AI1.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        AI1.name = "AI1";
        AI1.transform.position = new Vector3(Random.Range(leftSide, rightSide),upSide + 0.1f, 0.92f);
        AI1.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (AI1.transform.position.y > upSide)
            AI1.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (AI1 != null)
        {
            bullTime++;
            if (bullTime == bullTimeLimit)
            {
                Vector3 position = AI1.transform.position;
                position.y -= 0.2f;
                GameObject bullet = Instantiate(AIBull, position, AIBull.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speedY, 0);
                Destroy(bullet, 4);
                bullTime = 0;
                bullTimeLimit = Random.Range(50, 70);
            }
            if (dir == 0 && AI1.transform.position.x >= rightSide)
            {
                dir = 1;
            }
            else if (dir == 1 && AI1.transform.position.x <= leftSide)
            {
                dir = 0;
            }
            switch (dir)
            {
                case 0:
                    AI1.transform.Translate(-Vector3.right * 0.01f);
                    break;
                case 1:
                    AI1.transform.Translate(Vector3.right * 0.01f);
                    break;
            }
        }
    }
}
