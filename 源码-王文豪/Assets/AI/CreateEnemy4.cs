using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy4 : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide;
    int bullTime; //发弹计数器
    float speedY;
    int dir;
    int bullTimeLimit;
    int bullCount; //炮弹计数
    int dirTime; //方向改变计数器
    GameObject AIBull;
    GameObject AI4 = null;
    // Start is called before the first frame update
    void Start()
    {
        bullCount = 5;
        bullTime = 0;
        bullTimeLimit = 60;
        speedY = -0.8f;
        downSide = 2.3f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
        dir = 0;
        dirTime = 0;
        AIBull = GameObject.Find("AIBull4");
        AI4 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        AI4.transform.localScale = new Vector3(0.032f, 0.01f, 0.032f);
        AI4.AddComponent<Enemy4Info>();
        AI4.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/enemy4");
        AI4.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
        AI4.name = "AI4";
        AI4.transform.position = new Vector3(Random.Range(leftSide, rightSide), upSide+0.1f, 0.92f);
        AI4.transform.rotation = GameObject.Find("Player").transform.rotation;
        while (AI4.transform.position.y > upSide)
            AI4.transform.Translate(Vector3.forward * 0.000005f);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (AI4 != null)
        {
            bullTime++;
            if (bullTime == bullTimeLimit && (Random.Range(1, 10) <= 9))
            {
                bullCount = Random.Range(0, 100)%11-5;
                Vector3 position = AI4.transform.position;
                position.y -= 0.1f;
                position.x += 0.05f * bullCount;
                GameObject bullet = Instantiate(AIBull, position, AIBull.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speedY * 0.5f * bullCount, speedY, 0);
                Destroy(bullet, 4);
                bullTime = 0;
                bullTimeLimit = Random.Range(50, 70);
            }
            dirTime++;
            if (dirTime == 100)
            {
                dir = Random.Range(0, 100) % 4;
                dirTime = 0;
            }

            switch (dir)
            {
                case 0:
                    if (AI4.transform.position.y > downSide)
                        AI4.transform.Translate(Vector3.forward * 0.01f);
                    break;
                case 1:
                    if (AI4.transform.position.y < upSide)
                        AI4.transform.Translate(-Vector3.forward * 0.01f);
                    break;
                case 2:
                    if (AI4.transform.position.x > leftSide)
                        AI4.transform.Translate(Vector3.right * 0.01f);
                    break;
                case 3:
                    if (AI4.transform.position.x < rightSide)
                        AI4.transform.Translate(-Vector3.right * 0.01f);
                    break;
            }
        }
    }
}
