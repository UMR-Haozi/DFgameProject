using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloud1 : MonoBehaviour
{
    GameObject cloud1;
    float leftSide;
    float rightSide;
    // Start is called before the first frame update
    void Start()
    {
        leftSide = -2.6f;
        rightSide = 2.5f;
        cloud1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        cloud1.transform.localScale = new Vector3(0.128f, 0.001f, 0.128f);
        cloud1.name = "Cloud1";
        cloud1.transform.position = new Vector3(Random.Range(leftSide,rightSide),3.96f,0.94f);
        cloud1.transform.rotation = GameObject.Find("Player").transform.rotation;
        cloud1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/cloud1");
        cloud1.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(cloud1 != null)
        {
            cloud1.transform.Translate(Vector3.forward * 0.01f);
        }
    }
}
