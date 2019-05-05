using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MonoBehaviour
{
    public int ALLAICount { get; set; } //一个界面上总的敌人数量
    float CreatAITime; //创建敌人计时器
    float CreatAITimeLimit; //每隔多少时间创建一个敌人
    // Start is called before the first frame update
    void Start()
    {
        ALLAICount = 0;
        CreatAITime = 0;
        CreatAITimeLimit = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        CreatAITime += Time.deltaTime;
        if (CreatAITime >= CreatAITimeLimit)
        {
            if (ALLAICount < 4)
            {
                int type = Random.Range(0, 100000000) % 4;
                switch (type)
                {
                    case 0:
                        gameObject.AddComponent<CreateEnemy1>();
                        break;
                    case 1:
                        gameObject.AddComponent<CreateEnemy2>();
                        break;
                    case 2:
                        gameObject.AddComponent<CreateEnemy3>();
                        break;
                    case 3:
                        gameObject.AddComponent<CreateEnemy4>();
                        break;
                }
                ALLAICount++;
            }
            CreatAITime = 0;
        }
    }
}
