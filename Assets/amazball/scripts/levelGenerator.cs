using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
    //public Texture2D map;

    public GameObject table;

    int[,] buttomLayer;
    int[,] topLayer;
    int[] player;
    public GameObject playerIllusion;
    public bool moving = false;
    public string direction;

    private TrailRenderer tr;
    private Renderer skin;
    private Color rColor;
    private GameObject hat;

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
                //setParent
                newCube.transform.SetParent(table.transform);
                string cubeName = "" + i + j;
                buttomCubeMap.Add(cubeName, newCube);
                buttomLayer[i, j] = 0;
            }
        }
    }

    int colom2 = 0;
    int colom3 = 0;
    //colom4 is empty
    int colom5 = 0;
    //colom6 empty
    //colom7 empty
    int colom8 = 0;

    void drawTopLayer()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Color pixelColor = map.GetPixel(i, j);
                if (i== 0)
                {
                    //spawn passable cubes 0
                    topLayer[i, j] = 0;
                }

                if (i == 1)
                {
                    if (colom2 == 0)
                    {
                        colom2 = Random.Range(1, 6);
                    }
                   
                    topLayer[i, colom2]=0;
                    //set player on the top layer
                    playerIllusion.transform.position = new Vector3(i, colom2, -1);
                    //set parent
                    playerIllusion.transform.SetParent(table.transform);
                    player[0] = i;
                    player[1] = colom2;
                    buttomLayer[i, colom2] = 1;

                    if (colom2!=0&&j != colom2)
                    {
                        //spawn block can not pass 1
                        GameObject walls= Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                        //set parent
                        walls.transform.SetParent(table.transform);

                        topLayer[i, j] = 1;
                    }
                }

                //keep the exit and randomly chose go up or go down
                if (i ==2)
                {
                    topLayer[i, colom2] = 0;
                    if (colom3 == 0)
                    {
                        colom3 = Random.Range(1, 10);
                    }
                    //go up
                    if (colom3 >= 5)
                    {
                        if (colom3!=0&&j < colom2)
                        {
                            //spawn block can not pass 1
                            GameObject walls= Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }
                    else
                    {
                        if (colom3!=0&&j > colom2)
                        {
                            //spawn block can not pass 1
                            GameObject walls=Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }
                }

                //colom 4 pass

                //colom 5 chose top or bottom
                if (i == 4)
                {
                    if (colom5 == 0)
                    {
                        colom5 = Random.Range(1, 10);
                    }

                    if (colom5 >= 5)
                    {
                        //top
                        if (colom5 != 0 &&j!=0)
                        {
                            //spawn block can not pass 1
                            GameObject walls = Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }
                    else
                    {
                        if (colom5 != 0 &&j!=7)
                        {
                            //spawn block can not pass 1
                            GameObject walls = Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }


                }

                //colom 8 chose top or bottom 2 or 3 space
                if (i == 7)
                {
                    if (colom8 == 0)
                    {
                        colom8 = Random.Range(1, 10);
                    }

                    if (colom8 >= 5)
                    {
                        //top empty
                        if(colom8!=0 && j != 0 && j != 1)
                        {
                            //spawn block can not pass 1
                            GameObject walls = Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }
                    else
                    {
                        if(colom8!=0 && j!=7 && j != 6)
                        {
                            //spawn block can not pass 1
                            GameObject walls = Instantiate(wall, new Vector3(i, j, -1), Quaternion.identity);
                            //set parent
                            walls.transform.SetParent(table.transform);

                            topLayer[i, j] = 1;
                        }
                    }
                }


            }
        }

        //after rotate camera the game massed up haha
        //RotateTable();
    }


    void RotateTable()
    {
        table.transform.eulerAngles = new Vector3(0, 0, 90*Random.Range(-2,2));
    }


    public void playerMove(string direction)
    {
        if (direction == "right")
        {
            while (player[0] < 7 && topLayer[player[0] + 1, player[1]] != 1)
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
                GameManager.getInstance().playSfx("bouncy");
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
                GameManager.getInstance().playSfx("bouncy");
            }
            //play the stop animation
        }

        if (direction == "up")
        {
            while (player[1] < 7 && topLayer[player[0], player[1] + 1] != 1)
            {
                Vector3 end = new Vector3(transform.position.x, transform.position.y + 1, -1);
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
                GameManager.getInstance().playSfx("bouncy");
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
                GameManager.getInstance().playSfx("bouncy");
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
                    buttomCubeMap[findName].GetComponent<Renderer>().material.color = rColor;
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
                if (topLayer[i, j] == 0)
                {
                    win = false;
                }
            }
        }
        return win;
    }

    void Start()
    {
        rColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f)
    );
        skin = GetComponent<Renderer>();
        skin.material.color = rColor;

        tr = GetComponent<TrailRenderer>();
        tr.startColor = rColor;
        tr.endColor = Color.white;

        hat = GameObject.Find("hatPos");
        hat.transform.position = new Vector3(hat.transform.position.x, hat.transform.position.y, -1);

        Time.timeScale = 1;
        buttomLayer = new int[8, 8];
        topLayer = new int[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                buttomLayer[i, j] = 0;
                topLayer[i, j] = 0;
            }
        }

        player = new int[2] { 0, 0 };

        stop = GetComponent<ParticleSystem>();

        stop.startColor = rColor;

        an = GetComponent<Animator>();

        drawButtomLayer();
        drawTopLayer();
    }

    void Update()
    {
        Time.timeScale = 1;
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
            if (Mathf.Abs(endPos.y - startPos.y) > 50 || Mathf.Abs(endPos.x - startPos.x) > 50)
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
