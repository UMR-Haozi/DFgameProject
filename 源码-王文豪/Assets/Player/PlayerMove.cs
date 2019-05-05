using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float downSide, upSide, leftSide, rightSide;
    GameObject player;
    Object[] animStatic,animMoveLeft,animMoveRight,nowAnim,oldAnim;
    int nowFram = 0;
    public bool isTrigger { get; set; }
    float fps = 10, time = 0;
    float speedScale;
    // Start is called before the first frame update
    void Start()
    {
        speedScale = 0.0000115f;
        isTrigger = false;
        player = GameObject.Find("Player");
        animMoveLeft = Resources.LoadAll("Img/playersMoveLeft");
        animMoveRight = Resources.LoadAll("Img/playersMoveRight");
        animStatic = Resources.LoadAll("Img/playersStatic");
        oldAnim = nowAnim = animStatic;
        downSide = 1.23f;
        leftSide = -2.52f;
        upSide = 3.76f;
        rightSide = 2.48f;
    }
    private void OnGUI()
    {
        speedScale = GUI.HorizontalSlider(new Rect(Screen.width*0.93f, Screen.height * 0.96f, Screen.width * 0.07f, Screen.height * 0.04f), speedScale, 0.000009f, 0.000014f);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            setAnimation(animStatic);
            if(player.transform.position.y <= upSide)
                player.transform.Translate(-Vector3.forward * Screen.width * speedScale);
        }
        if (Input.GetKey(KeyCode.S))
        {
            setAnimation(animStatic);
            if (player.transform.position.y >= downSide)
                player.transform.Translate(Vector3.forward * speedScale * Screen.width);
        }
        if (Input.GetKey(KeyCode.A))
        {
            setAnimation(animMoveLeft);
            if (player.transform.position.x >= leftSide)
                player.transform.Translate(Vector3.right * speedScale * Screen.width);
        }
        if (!Input.GetKey(KeyCode.A) && nowAnim == animMoveLeft)
        {
            setAnimation(animStatic);
        }
        if (Input.GetKey(KeyCode.D))
        {
            setAnimation(animMoveRight);
            if (player.transform.position.x <= rightSide)
                player.transform.Translate(-Vector3.right * speedScale * Screen.width);
        }
        if (!Input.GetKey(KeyCode.D) && nowAnim == animMoveRight)
        {
            setAnimation(animStatic);
        }
        DrawAnimation(nowAnim);
    }
    void DrawAnimation(Object[] tex)
    {
        time += Time.deltaTime;
        if (time >= 1.0 / fps)
        {
            nowFram++;
            time = 0;
            if (nowFram >= tex.Length)
            {
                nowFram = (tex == animStatic ? 0 : 4);
            }
        }
        if (isTrigger && nowFram % 2 == 0)
        {
            player.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            player.transform.localScale = new Vector3(0.024f, 0.001f, 0.036f);
        }
        player.GetComponent<Renderer>().material.mainTexture = (Texture)tex[nowFram];
    }
    void setAnimation(Object[] tex)
    {
        nowAnim = tex;
        if (!oldAnim.Equals(nowAnim))
        {
            nowFram = 0;
            oldAnim = nowAnim;
        }
    }

    
}
