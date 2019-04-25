using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Renderer rend;

    public Material m1;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        //rend.material = m1;
    }

    public void change()
    {
        if (rend.material != m1)
        {
            rend.material = m1;
        }
        
    }
}
