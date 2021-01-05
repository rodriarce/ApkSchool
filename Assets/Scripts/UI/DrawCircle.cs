using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<LineRenderer>().useWorldSpace = false;
        var go1 = new GameObject { name = "Circle" };
        go1.transform.Rotate(180f, 90f, 90f);
        //go1.transform.Translate(new Vector3(0f, 4f, 0f));
         go1.DrawCircle(1, .02f);
        go1.transform.position = new Vector3(0.62f, 4.06f, -1.19f);
        go1.transform.eulerAngles = new Vector3(-2.13f, -90f, -111f);
        go1.transform.localScale = new Vector3(0.29f, 0.29f, 0.29f);





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
