using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Texture2D bg;
    public GUISkin MainMenuSkin;
    bool start, introduction, exit;
    float upOffset;
    float menuLeftOffset;
    float menuUpOffset;
    float menuWidth;
    float menuHeight;
    float showWidth, showHeight, showLeftOffset, showUpOffset;
    bool show;
    ArrayList windows;
    GameObject MainCamera, MainMenuCamera,YouWinCamera,YouLoseCamera;
    MusicControl musicControl;
    // Start is called before the first frame update
    void Start()
    {
        musicControl = GameObject.Find("Plane").GetComponent<MusicControl>();
        windows = new ArrayList();
        show = false;
        upOffset = Screen.height * 0.52f;
        menuLeftOffset = Screen.width * 0.42f;
        menuUpOffset = Screen.height * 0.06f;
        menuWidth = Screen.width * 0.16f;
        menuHeight = Screen.height * 0.08f;
        showWidth = Screen.width * 0.5f;
        showHeight = Screen.height * 0.2f;
        showLeftOffset = Screen.width * 0.25f;
        showUpOffset = Screen.height * 0.3f;
        GetCamera cameraInfo = GameObject.Find("Plane").GetComponent<GetCamera>();
        MainCamera = cameraInfo.MainCamera;
        MainMenuCamera = cameraInfo.MainMenuCamera;
        YouWinCamera = cameraInfo.YouWinCamera;
        YouLoseCamera = cameraInfo.YouLoseCamera;
        MainCamera.SetActive(false);
        YouLoseCamera.SetActive(false);
        YouWinCamera.SetActive(false);
        bg = (Texture2D)Resources.Load("Img/bg3");
        start = false;
        introduction = false;
        exit = false;
    }
    private void OnGUI()
    {
        MainMenuSkin.button.fontSize = Screen.width/40;
        MainMenuSkin.textArea.fontSize = Screen.width/40;
        GUI.skin = MainMenuSkin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg, ScaleMode.StretchToFill, true, 0);
        GUILayout.BeginVertical();

        GUILayout.Space(upOffset);

        GUILayout.BeginHorizontal();
        GUILayout.Space(menuLeftOffset);
        start = GUILayout.Button("开始游戏",GUILayout.Width(menuWidth),GUILayout.Height(menuHeight));
        GUILayout.EndHorizontal();
        GUILayout.Space(menuUpOffset);

        GUILayout.BeginHorizontal();
        GUILayout.Space(menuLeftOffset);
        introduction = GUILayout.Button("游戏说明", GUILayout.Width(menuWidth), GUILayout.Height(menuHeight));
        GUILayout.EndHorizontal();
        GUILayout.Space(menuUpOffset);

        GUILayout.BeginHorizontal();
        GUILayout.Space(menuLeftOffset);
        exit = GUILayout.Button("退出游戏", GUILayout.Width(menuWidth), GUILayout.Height(menuHeight));
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        for(int i = 0; i < windows.Count; ++i)
        {
            windows[i] = GUILayout.Window(i, (Rect)windows[i], showIntroduction, "游戏说明");
        }


        if (start)
        {
            MainMenuCamera.SetActive(false);
            MainCamera.SetActive(true);
            MainCamera.AddComponent<CreatePlayer>();
            MainCamera.AddComponent<MainControl>();
            musicControl.PlayMusic(1);
        }
        if (introduction)
        {
            windows.Add(new Rect(showLeftOffset,showUpOffset,showWidth,showHeight));
        }
        if (exit)
        {
            Application.Quit();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void showIntroduction(int windowId)
    {
        GUILayout.TextArea("欢迎来到灵梦的冒险~\n" +
            "W，S，A，D分别控制灵梦上下左右移动，K控制灵梦发射子弹~\n" +
            "左上角表示生命值显示，右上角是当前分数（分别达到1000，10000，80000会遇到相应的Boss哦~）\n"+
            "左下角可以调节游戏音量(左小右大）~\n" +
            "右下角可以调节人物移动速度(左小右大）~\n" +
            "游戏时按ESC键可以返回主菜单\n" +
            "到Boss或者消灭掉Boss之后灵梦都会马上满血~＼＼└('ω')┘/ /\n" +
            "共有三个大Boss等着你哦~你能顺利通关么");
        if (GUILayout.Button("关闭说明"))
        {
            windows.RemoveAt(windowId);
        }
    }
}
