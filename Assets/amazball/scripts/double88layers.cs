using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double88layers : MonoBehaviour
{
    public Texture2D map;
    int[,] buttomLayer;
    int[,] topLayer;
    int[] player;
    public GameObject playerIllusion;
    public bool moving=false;
    public string direction;

    private Animator an;

    ParticleSystem stop;
    public bool win = false;
    public ParticleSystem winParticle;

    Vector2 startPos, endPos;

    public GameObject buttomCube;
    Dictionary<string, GameObject> buttomCubeMap = new Dictionary<string, GameObject>();

    public GameObject wall;

    void drawButtomLayer()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //spawn color change and animation cubes
                GameObject newCube = (GameObject)Instantiate(buttomCube, new Vector3(i, j, 0), Quaternion.identity);
                string cubeName = "" + i + j;
                buttomCubeMap.Add(cubeName, newCube);
                buttomLayer[i,j] = 0;
            }
        }
    }

    void drawTopLayer()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Color pixelColor = map.GetPixel(i, j);
                if (pixelColor.a == 0)
                {
                    //spawn passable cubes 0
                    topLayer[i,j] = 0;
                }
                else if(pixelColor==Color.white)
                {
                    //spawn block can not pass 1
                   Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                    topLayer[i,j] = 1;
                }
                else
                {
                    //set player on the top layer
                    playerIllusion.transform.position = new Vector3(i, j, -1);
                    player[0] = i;
                    player[1] = j;
                    buttomLayer[i, j] = 1;
                   
                }
            }
        }
    }

    public void playerMove(string direction)
    {
        if (direction == "right")
        {
            while (player[0]<7 && topLayer[player[0]+1, player[1]]!= 1)
            {
                Vector3 end = new Vector3(transform.position.x + 1, transform.position.y, -1);
                //move one position right 
                moving = true;
                playerIllusion.transform.position = end;
                //playerIllusion.transform.position = Vector3.MoveTowards(playerIllusion.transform.position, end,  Time.deltaTime);

                    player[0]++;

                    //paint player pass spot
                    buttomLayer[player[0], player[1]] = 1;
            }
           
                //stop move
                moving = false;
            if (!stop.isPlaying)
            {
                stop.Play();
                an.SetTrigger("stop");
            }
            //play the stop animation
        }

        if (direction == "left")
        {
            while (player[0] > 0 && topLayer[player[0] - 1, player[1]] != 1)
            {
                Vector3 end = new Vector3(transform.position.x - 1, transform.position.y, -1);
                //move one position right 
                moving = true;
                playerIllusion.transform.position = end;
                //playerIllusion.transform.position = Vector3.MoveTowards(playerIllusion.transform.position, end,  Time.deltaTime);

                player[0]--;

                //paint player pass spot
                buttomLayer[player[0], player[1]] = 1;
            }

            //stop move
            moving = false;
            if (!stop.isPlaying)
            {
                stop.Play();
                an.SetTrigger("stop");
            }
            //play the stop animation
        }

        if (direction == "up")
        {
            while (player[1] < 7 && topLayer[player[0], player[1]+1] != 1)
            {
                Vector3 end = new Vector3(transform.position.x , transform.position.y+1, -1);
                //move one position right 
                moving = true;
                playerIllusion.transform.position = end;
                //playerIllusion.transform.position = Vector3.MoveTowards(playerIllusion.transform.position, end,  Time.deltaTime);

                player[1]++;

                //paint player pass spot
                buttomLayer[player[0], player[1]] = 1;
            }

            //stop move
            moving = false;
            if (!stop.isPlaying)
            {
                stop.Play();
                an.SetTrigger("stop");
            }
            //play the stop animation
        }

        if (direction == "down")
        {
            while (player[1] > 0 && topLayer[player[0], player[1] - 1] != 1)
            {
                Vector3 end = new Vector3(transform.position.x, transform.position.y - 1, -1);
                //move one position right 
                moving = true;
                playerIllusion.transform.position = end;
                //playerIllusion.transform.position = Vector3.MoveTowards(playerIllusion.transform.position, end,  Time.deltaTime);

                player[1]--;

                //paint player pass spot
                buttomLayer[player[0], player[1]] = 1;
            }

            //stop move
            moving = false;
            if (!stop.isPlaying)
            {
                stop.Play();
                an.SetTrigger("stop");
            }
            //play the stop animation
        }

    }

    void paintButtomLayer()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (buttomLayer[i, j] == 1)
                {
                    string findName = "" + i + j;
                    buttomCubeMap[findName].GetComponent<ChangeColor>().change();
                    topLayer[i, j] = 2;
                }
                

            }
        }
    }


    bool checkWin()
    {
        bool win = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (topLayer[i,j] == 0)
                {
                    win = false;
                }
            }
        }
        return win;
    }

    void Start()
    {
        Time.timeScale = 1;
        buttomLayer = new int[8, 8];
        topLayer=new int[8,8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                buttomLayer[i,j] = 0;
                topLayer[i,j] = 0;
            }
        }

        player=new int[2] { 0,0};

        stop = GetComponent<ParticleSystem>();
        an = GetComponent<Animator>();

        drawButtomLayer();
        drawTopLayer();
    }

    void Update()
    {
        paintButtomLayer();

        if (checkWin())
        {
            if (!win)
            {
                win = true;
                Debug.Log("win");

                if (!winParticle.isPlaying)
                {
                    winParticle.Play();
                }
                GameData.getInstance().main.gameWin();

            }

        }

        if (moving)
        {
            playerMove(direction);
        }
       

        if (Input.GetButtonDown("Fire1"))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            startPos = Input.mousePosition;
        }

       

        if (Input.GetButton("Fire1"))
        {

            endPos = Input.mousePosition;

            //detect its moving for a distance
            if (Mathf.Abs(endPos.y - startPos.y) > 100 || Mathf.Abs(endPos.x - startPos.x) > 100)
            {
                //check moving on y or x
                if (Mathf.Abs(endPos.y - startPos.y) > Mathf.Abs(endPos.x - startPos.x))
                {
                    //check move up or move dowm
                    if (endPos.y - startPos.y > 0)
                    {
                        //move up
                        Debug.Log("up");
                        moving = true;
                        direction = "up";
                        //update startpos
                        startPos = Input.mousePosition;
                    }
                    else
                    {
                        Debug.Log("down");
                        moving = true;
                        direction = "down";
                        startPos = Input.mousePosition;
                    }
                }
                else
                {
                    if (endPos.x - startPos.x > 0)
                    {
                        Debug.Log("right");
                        moving = true;
                        direction = "right";
                       
                        startPos = Input.mousePosition;
                    }
                    else
                    {
                        Debug.Log("left");
                        moving = true;
                        direction = "left";
                        startPos = Input.mousePosition;
                    }
                }
            }

        }

    }
}
