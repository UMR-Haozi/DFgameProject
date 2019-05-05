using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    int level = 0;
    float BossTime;
    float cloud1Time,cloud1TimeLimit,cloud1DownSide;
    float cloud2Time,cloud2TimeLimit,cloud2DownSide;
    GameObject nowMission;
    MusicControl musicControl;
    bool boss1, mission2, boss2, mission3, boss3;

    // Start is called before the first frame update
    void Start()
    {
        mission2 = false;
        mission3 = false;
        boss1 = false;
        boss2 = false;
        boss3 = false;

        musicControl = GameObject.Find("Plane").GetComponent<MusicControl>();
        BossTime = 0;
        cloud1Time = 0;
        cloud1TimeLimit = 0.5f;
        cloud2Time = 0;
        cloud2TimeLimit = 1;
        cloud1DownSide = 0.42f;
        cloud2DownSide = 1.05f;
        nowMission = GameObject.CreatePrimitive(PrimitiveType.Plane);
        nowMission.transform.localScale = new Vector3(0, 0, 0);
        nowMission.AddComponent<Mission1>();
    }

    // Update is called once per frame
    void Update()
    {
        int score = GameObject.Find("Player").GetComponent<PlayerInfo>().Score;
        //Boss1
        if (level == 0 && score >= 1000 && score < 1150) 
        {
            if (!boss1)
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
                musicControl.PlayMusic(2);
                boss1 = true;
            }
            Destroy(nowMission);
            DeleteAI();
            GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
            BossTime += Time.deltaTime;
            if(BossTime >= 2.0)
            {
                level++;
                ChangeSenceToBoss1();
                gameObject.AddComponent<CreateBoss1>();
                BossTime = 0;
            }
        }
        //Mission2
        else if (level == 1 && score >= 1150 && score < 10000)
        {
            if (!mission2)
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
                musicControl.PlayMusic(3);
                mission2 = true;
            }
            Destroy(gameObject.GetComponent<CreateBoss1>());
            level++;
            nowMission = GameObject.CreatePrimitive(PrimitiveType.Plane);
            nowMission.transform.localScale = new Vector3(0, 0, 0);
            nowMission.AddComponent<Mission2>();
        }
        //Boss2
        else if (level == 2 && score >= 10000 && score < 11500)
        {
            if (!boss2)
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
                musicControl.PlayMusic(4);
                boss2 = true;
            }
            Destroy(nowMission);
            DeleteAI();
            GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
            BossTime += Time.deltaTime;
            if (BossTime >= 2.0)
            {
                level++;
                ChangeSenceToBoss2();
                gameObject.AddComponent<CreateBoss2>();
                BossTime = 0;
            }
        }
        // Mission3
        else if (level == 3 && score >= 11500 && score < 80000)
        {
            if (!mission3)
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
                musicControl.PlayMusic(5);
                mission3 = true;
            }
            Destroy(gameObject.GetComponent<CreateBoss2>());
            level++;
            nowMission = GameObject.CreatePrimitive(PrimitiveType.Plane);
            nowMission.transform.localScale = new Vector3(0, 0, 0);
            nowMission.AddComponent<Mission3>();
        }
        //Boss3
        else if (level == 4 && score >= 80000 && score < 92000)
        {
            if (!boss3)
            {
                GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
                musicControl.PlayMusic(6);
                boss3 = true;
            }
            Destroy(nowMission);
            DeleteAI();
            BossTime += Time.deltaTime;
            GameObject.Find("Player").GetComponent<PlayerInfo>().Life = 3;
            if (BossTime >= 2.0)
            {
                level++;
                ChangeSenceToBoss3();
                gameObject.AddComponent<CreateBoss3>();
                BossTime = 0;
            }
        }
        else if (level == 5 && score >= 92000)
        {
            Destroy(gameObject.GetComponent<CreateBoss3>());
            Win();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Player").GetComponent<PlayerInfo>().isGameOver = true;
        }
    }
    private void FixedUpdate()
    {
        GameObject cloud1 = GameObject.Find("Cloud1");
        if (cloud1 == null)
        {
            cloud1Time += Time.fixedDeltaTime;
            if(cloud1Time >= cloud1TimeLimit)
            {
                gameObject.AddComponent<CreateCloud1>();
                cloud1Time = 0;
                cloud1TimeLimit = Random.Range(0.0f, 0.5f);
            }
        }
        else
        {
            if(cloud1.transform.position.y <= cloud1DownSide)
            {
                Destroy(cloud1);
                Destroy(gameObject.GetComponent<CreateCloud1>());
            }
        }
        GameObject cloud2 = GameObject.Find("Cloud2");
        if (cloud2 == null)
        {
            cloud2Time += Time.fixedDeltaTime;
            if (cloud2Time >= cloud2TimeLimit)
            {
                gameObject.AddComponent<CreateCloud2>();
                cloud2Time = 0;
                cloud2TimeLimit = Random.Range(0.5f, 1.0f);
            }
        }
        else
        {
            if (cloud2.transform.position.y <= cloud2DownSide)
            {
                Destroy(cloud2);
                Destroy(gameObject.GetComponent<CreateCloud2>());
            }
        }
    }
    public void DeleteAI()
    {
        Destroy(GameObject.Find("AI1"));
        Destroy(GameObject.Find("AI2"));
        Destroy(GameObject.Find("AI3"));
        Destroy(GameObject.Find("AI4"));
    }
    void ChangeSenceToBoss1()
    {
        Debug.Log("Boss1");
    }
    void ChangeSenceToBoss2()
    {
        Debug.Log("Boss2");
    }
    void ChangeSenceToBoss3()
    {
        Debug.Log("Boss3");
    }
    void Win()
    {
        gameObject.SetActive(false);
        GameObject.Find("Plane").GetComponent<GetCamera>().YouWinCamera.SetActive(true);
        Destroy(gameObject.GetComponent<MainControl>());
        Destroy(gameObject.GetComponent<CreatePlayer>());
    }
    public void Lose()
    {
        gameObject.SetActive(false);
        switch (level)
        {
            case 1:
                Destroy(gameObject.GetComponent<CreateBoss1>());
                Destroy(GameObject.Find("Boss1"));
                break;
            case 3:
                Destroy(gameObject.GetComponent<CreateBoss2>());
                Destroy(GameObject.Find("Boss2"));
                break;
            case 5:
                Destroy(gameObject.GetComponent<CreateBoss3>());
                Destroy(GameObject.Find("Boss3"));
                break;
            default:
                Destroy(nowMission);
                break;
        }
        
        GameObject.Find("Plane").GetComponent<GetCamera>().YouLoseCamera.SetActive(true);
        Destroy(gameObject.GetComponent<MainControl>());
        Destroy(gameObject.GetComponent<CreatePlayer>());
        
    }
    public void subAICount()
    {
        if(level == 0)
        {
            nowMission.GetComponent<Mission1>().ALLAICount--;
        }
        else if(level == 2)
        {
            nowMission.GetComponent<Mission2>().ALLAICount--;
        }
        else if(level == 4)
        {
            nowMission.GetComponent<Mission3>().ALLAICount--;
        }
    }
}
