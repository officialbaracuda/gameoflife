using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    void Start()
    {
        cam.transform.position = new Vector3(94, 94, 47);
        cam.transform.rotation = Quaternion.Euler(65.0f, -90.0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
