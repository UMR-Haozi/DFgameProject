using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int Life { get; set; }
    public int Score { get; set; }
    Texture2D hurt,star;
    bool isTrigger;
    int timeTrigger; //碰撞计时器
    public bool isGameOver;
    float gameOverTime;
    float gameOverTimeLimit;
    MainControl mainControl;
    GUISkin playerInfoSkin;

    float hurtWidth;
    float starWidth,starHeight;
    float labelWidth,labelHeight;
    // Start is called before the first frame update
    void Start()
    {
        playerInfoSkin = (GUISkin)Resources.Load("Skins/PlayerInfoSkin");
        playerInfoSkin.label.fontSize = Screen.width/60;
        hurtWidth = Screen.width * 0.042f;
        starWidth = Screen.width * 0.052f;
        starHeight = Screen.width * 0.0482f;
        labelWidth = Screen.width * 0.0556f;
        labelHeight = Screen.width * 0.030f;
        isGameOver = false;
        mainControl = GameObject.Find("Main Camera").GetComponent<MainControl>();
        gameOverTime = 0;
        gameOverTimeLimit = 0.1f;
        Life = 3;
        Score = 0;
        isTrigger = false;
        timeTrigger = 0;
        hurt = Resources.Load("Img/hurt") as Texture2D;
        star = Resources.Load("Img/star") as Texture2D;
    }
    public void addScore(int value)
    {
        Score += value;
    }
    public void subLife()
    {
        Life--;
    }
    private void OnGUI()
    {
        GUI.skin = playerInfoSkin;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.BeginHorizontal();
        for(int i = 0; i < Life; ++i)
        {
            GUILayout.Box(hurt,GUILayout.Width(hurtWidth));
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.BeginVertical();
        GUILayout.Box(star,GUILayout.Width(starWidth),GUILayout.Height(starHeight));
        GUILayout.Label(Score.ToString(),GUILayout.Width(labelWidth),GUILayout.Height(labelHeight));
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isTrigger)
        {
            timeTrigger++;
        }
        if (timeTrigger == 60)
        {
            timeTrigger = 0;
            isTrigger = false;
            gameObject.GetComponent<PlayerMove>().isTrigger = false;
        }

        if(isGameOver == true)
        {
            gameOverTime += Time.deltaTime;
            mainControl.DeleteAI();
            if(gameOverTime >= gameOverTimeLimit)
            {
                Destroy(gameObject);
                mainControl.Lose();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger || other.gameObject.name.StartsWith("PlayerBull")) return;
        isTrigger = true;
        gameObject.GetComponent<PlayerMove>().isTrigger = true;
        Life--;
        if (Life == 0)
        {
            isGameOver = true;
            return;
        }
    }
}
