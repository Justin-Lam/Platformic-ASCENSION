using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWalls : MonoBehaviour
{
    void Update()
    {
        // Follow the camera
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }
}
