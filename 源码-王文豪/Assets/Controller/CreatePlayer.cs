using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.CreatePrimitive(PrimitiveType.Plane);
        player.transform.localScale = new Vector3(0.024f, 0.001f, 0.036f);
        player.name = "Player";
        player.transform.position = new Vector3(0.04f,1.42f,0.92f);
        player.transform.rotation = Quaternion.Euler(90, 90, -90);
        player.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/player");
        player.GetComponent<MeshCollider>().convex = true;
        player.GetComponent<MeshCollider>().isTrigger = true;
        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().useGravity = false;
        player.AddComponent<PlayerBullScript>();
        player.AddComponent<PlayerInfo>();
        player.AddComponent<PlayerMove>();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
