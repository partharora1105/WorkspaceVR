using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public  float speed;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0)
        {
            speed = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.position += new Vector3(0.001f * speed, 0, 0);
        obj.transform.localScale += new Vector3(0.001f * speed, 0, 0);
    }

}
