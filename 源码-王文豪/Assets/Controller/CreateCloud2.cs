using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloud2 : MonoBehaviour
{
    GameObject cloud2;
    float leftSide;
    float rightSide;
    // Start is called before the first frame update
    void Start()
    {
        leftSide = -2.67f;
        rightSide = 2.7f;
        cloud2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        cloud2.transform.localScale = new Vector3(0.128f, 0.001f, 0.128f);
        cloud2.name = "Cloud2";
        cloud2.transform.position = new Vector3(1.97f, 4.6f, 0.94f);
        cloud2.transform.rotation = GameObject.Find("Player").transform.rotation;
        cloud2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Img/cloud2");
        cloud2.GetComponent<Renderer>().material.shader = GameObject.Find("Player").GetComponent<Renderer>().material.shader;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (cloud2 != null)
        {
            cloud2.transform.Translate(Vector3.forward * 0.01f);
        }
    }
}
