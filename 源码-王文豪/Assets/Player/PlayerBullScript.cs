using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullScript : MonoBehaviour
{
    public GameObject bull;
    float speed;
    Vector3 position;
    float bullTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        bull = GameObject.Find("PlayerBull");
        speed = 1.8f;
        bullTime = 0.2f;
    }
    private void OnGUI()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            bullTime += Time.deltaTime;
            if (bullTime >= 0.2)
            {
                position = GameObject.Find("Player").transform.position;
                position.y += 0.32f;
                GameObject bullet = Instantiate(bull, position, bull.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
                Destroy(bullet, 3);
                bullTime = 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            bullTime = 0.2f;
        }
    }
}
