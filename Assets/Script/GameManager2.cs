using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class GameManager : MonoBehaviour
{
    public int sizeX, sizeZ;
    public Floor floorPrefab;

    public Floor[,] blocks;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject effectPrefab;
    private GameObject instanEffect;

    public GameObject playerPrefab;

    public GameObject GvrEvent, eventSystem;

    public Maze maze;

    public Vector2 startPos, endPos;
    bool[,] wasHere;
    bool[,] solution;
    bool[,] wall;

    public static GameManager current;

    private Text testo;
    private void Awake()
    {
        current = this;
        blocks = new Floor[sizeX, sizeZ];
        wasHere = new bool[sizeX, sizeZ];
        solution = new bool[sizeX, sizeZ];
        wall = new bool[sizeX, sizeZ];
        testo = GameObject.Find("Output").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        XRSettings.LoadDeviceByName("");
        XRSettings.enabled = false;
        generateFloorMaze();
        generateOutsideWall();
        GvrEvent.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {



    }

    public void generateFloorMaze()
    {

        Camera.main.transform.position = new Vector3(4.5f, 20, 4.5f);

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Vector2 pos = new Vector2(x, z);
                if (pos.Equals(startPos))
                    Instantiate(startPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
                else if (pos.Equals(endPos))
                {
                    Instantiate(endPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
                    Instantiate(effectPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
                }
                else
                {
                    blocks[x, z] = Instantiate(floorPrefab);
                    blocks[x, z].name = "(" + x + " , " + z + " )";
                    blocks[x, z].transform.parent = maze.transform;
                    blocks[x, z].transform.localPosition = new Vector3(x, 0, z);
                }
                wall[x, z] = false;
                solution[x, z] = false;

            }
        }
    }

    public void generateOutsideWall()
    {
        /* Vector2 pos0;
         Vector2 pos1;
         for (int x = 0; x < sizeX; x++) {
             pos0 = new Vector2(x, 0);
             pos1 = new Vector2(x, sizeZ - 1);

             if (!pos0.Equals(startPos) && !pos0.Equals(endPos))
             {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(x, 0.5f, 0);
                 wall[x, 0] = true;
             }
             if (!pos1.Equals(startPos) && !pos1.Equals(endPos))
             {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(x, 0.5f, sizeZ - 1);
                 wall[x, sizeZ - 1] = true;
             }
             if (pos0.Equals(startPos)) {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(pos0.x, 0.5f, pos0.y-1);
             }
             if (pos1.Equals(endPos)) {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(pos1.x, 0.5f, pos1.y +1);
                            }
         }

         for (int z = 1; z < sizeZ - 1; z++)
         {
             pos0 = new Vector2(0, z);
             pos1 = new Vector2(sizeX - 1, z);
             if (!pos0.Equals(startPos) && !pos0.Equals(endPos))
             {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(0, 0.5f, z);
                 wall[0, z] = true;
             }
            if (!pos1.Equals(startPos) && !pos1.Equals(endPos))
             {
                 outsideWall = Instantiate(outsideWallPrefab) as OutsideWall;
                 outsideWall.transform.parent = maze.transform;
                 outsideWall.transform.localPosition = new Vector3(sizeX - 1, 0.5f, z);
                 wall[sizeX - 1, z] = true;
             }
         }*/
    }


    public void solveMaze()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                wasHere[x, z] = false;
                solution[x, z] = false;
            }
        }
        bool b = recursiveSolve((int)startPos.x, (int)startPos.y);
        testo.text = b.ToString();

        if (b) { spawnPlayer(); }
        else
        {

            testo.text = "No EXIT";
        }
    }

    private bool recursiveSolve(int _x, int _y)
    {


        if (endPos.Equals(new Vector2(_x, _y))) return true; // If you reached the end

        if (wall[_x, _y] || wasHere[_x, _y]) return false; // If you are on a wall or already were here
        wasHere[_x, _y] = true;

        if (_x != 0) // Checks if not on left edge
            if (recursiveSolve(_x - 1, _y))
            { // Recalls method one to the left
                solution[_x, _y] = true; // Sets that path value to true;
                return true;
            }
        if (_x != sizeX - 1) // Checks if not on right edge
            if (recursiveSolve(_x + 1, _y))
            { // Recalls method one to the right
                solution[_x, _y] = true;
                return true;
            }
        if (_y != 0)  // Checks if not on top edge
            if (recursiveSolve(_x, _y - 1))
            { // Recalls method one up
                solution[_x, _y] = true;
                return true;
            }
        if (_y != sizeZ - 1) // Checks if not on bottom edge
            if (recursiveSolve(_x, _y + 1))
            { // Recalls method one down
                solution[_x, _y] = true;
                return true;
            }
        return false;
    }




    public void addWall(int _x, int _z)
    {
        wall[_x, _z] = true;
    }

    public void removeWall(int _x, int _z)
    {
        wall[_x, _z] = false;
    }


    public void spawnPlayer()
    {

        StartCoroutine(SwitchToVR());
        Instantiate(playerPrefab, new Vector3(startPos.x, 0.5f, startPos.y), Quaternion.identity);

        GvrEvent.SetActive(true);
        eventSystem.SetActive(false);


    }

    IEnumerator SwitchToVR()
    {

        string newDevice = "cardboard"; // Or "cardboard".

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        if (string.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            testo.text = "VR ATTIVO";
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;

        }
        else { testo.text = "Errore split"; }


    }



}


