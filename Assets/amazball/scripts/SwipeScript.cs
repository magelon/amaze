using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    Vector2 startPos, endPos;
    
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            startPos = Input.mousePosition;
        }

        if (Input.GetButton("Fire1")||Input.GetButtonUp("Fire1"))
        {
           
            endPos = Input.mousePosition;
           
            //detect its moving for a distance
            if(Mathf.Abs(endPos.y - startPos.y)>100|| Mathf.Abs(endPos.x - startPos.x) > 100)
            {
                //check moving on y or x
                if(Mathf.Abs(endPos.y - startPos.y)> Mathf.Abs(endPos.x - startPos.x))
                {
                    //check move up or move dowm
                    if (endPos.y - startPos.y > 0)
                    {
                        //move up
                        Debug.Log("up");
                        //update startpos
                        startPos = Input.mousePosition;
                    }
                    else
                    {
                        Debug.Log("dowm");
                        startPos = Input.mousePosition;
                    }
                }
                else
                {
                    if (endPos.x - startPos.x > 0)
                    {
                        Debug.Log("right");
                        GetComponent<double88layers>().moving = true;
                        GetComponent<double88layers>().direction = "right";

                        startPos = Input.mousePosition;
                    }
                    else
                    {
                        Debug.Log("left");
                        startPos = Input.mousePosition;
                    }
                }
            }
           
        }
      
    }
}
