using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeElapsed;     public float TimeElapsed => timeElapsed;

    void Start()
    {
		timeElapsed = 0f;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}
