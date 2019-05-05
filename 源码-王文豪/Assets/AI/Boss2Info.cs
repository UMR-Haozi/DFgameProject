using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Info : MonoBehaviour
{
    int life;
    bool isTrigger;
    int timeTrigger; //碰撞计时器
    // Start is called before the first frame update
    void Start()
    {
        life = Random.Range(70, 80);
        isTrigger = false;
        timeTrigger = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isTrigger)
        {
            if (timeTrigger / 3 % 2 == 0)
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = null;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/Boss2");
            }

            timeTrigger++;
        }
        if (timeTrigger == 22)
        {
            timeTrigger = 0;
            isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.StartsWith("PlayerBull")) return;
        isTrigger = true;
        Destroy(other.gameObject);
        life--;
        if (life == 0)
        {
            GameObject.Find("Player").GetComponent<PlayerInfo>().addScore(30000);
            Destroy(gameObject);
            return;
        }
    }
}
