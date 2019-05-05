using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    Texture2D bg;
    public GUISkin MainMenuSkin;
    bool ok;
    GameObject MainMenuCamera;
    float buttonWidth, buttonHeight;
    float upOffset, leftOffset;
    MusicControl musicControl;
    // Start is called before the first frame update
    void Start()
    {
        musicControl = GameObject.Find("Plane").GetComponent<MusicControl>();
        buttonWidth = Screen.width * 0.16f;
        buttonHeight = Screen.height * 0.08f;
        upOffset = Screen.height * 0.55f;
        leftOffset = Screen.width * 0.42f;
        MainMenuCamera = GameObject.Find("Plane").GetComponent<GetCamera>().MainMenuCamera;
        bg = (Texture2D)Resources.Load("Img/bgWin");
        ok = false;
    }
    private void OnGUI()
    {
        GUI.skin = MainMenuSkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg, ScaleMode.StretchToFill, true, 0);
        ok = GUI.Button(new Rect(),"返回主菜单");
        //GUI.Button(new Rect(320, 360, 100, 30), "退出游戏");
        if (ok)
        {
            gameObject.SetActive(false);
            MainMenuCamera.SetActive(true);
            musicControl.PlayMusic(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
