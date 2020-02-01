using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = new Material(renderer.material.shader);
        renderer.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
