using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_background : MonoBehaviour
{
    public float speed;
    public Renderer background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
