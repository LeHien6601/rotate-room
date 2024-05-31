using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    void Start()
    {
        
    }
    public void DestroyThis()
    {
        Debug.Log("lol");
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
