using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallExtObj;
    [SerializeField]
    private GameObject _floorObj;

    [SerializeField]
    private int _sizeMaze;


    // Start is called before the first frame update
    void Start()
    {
        GenerateExternalWalls(_sizeMaze);
    }

    private void GenerateExternalWalls(int size)
    {
        var limX = size / 2;
        var limZ = size / 2;

        GameObject obj;

        for (int x = -limX; x <= limX; x++)
        {

            for (int z = -limZ; z <= limZ; z++)
            {
                if (Mathf.Abs(x) == limX || Mathf.Abs(z) == limZ)
                {
                    obj = Instantiate(_wallExtObj);


                }
                else
                {
                    obj = Instantiate(_floorObj);
                    obj.GetComponent<FloorController>().Init();
                }

                obj.transform.position = new Vector3(x, 0, z);
                obj.transform.SetParent(transform, false);
            }
        }
    }

    // Update is called once per frame
}
