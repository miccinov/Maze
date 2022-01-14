using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
public class Floor : MonoBehaviour
{
   
    public InsideWall insideWallPrefab;
    private InsideWall insideWall;
    
    public UnityEngine.UI.Text testo;
    // Start is called before the first frame update

    

    private void Awake()
    {
        testo = GameObject.Find("Text").GetComponent<Text>();
      
    }
    void Start()
    {
        GetComponentInChildren<MeshRenderer>().sortingLayerName = "floor";
        GetComponentInChildren<MeshRenderer>().sortingOrder = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* private void OnMouseDown()
     {

         if (insideWall == null)
         {

             insideWall = Instantiate(insideWallPrefab) as InsideWall;
             insideWall.transform.parent = transform;
             insideWall.transform.localPosition = new Vector3(0, 0, -0.5f);
         }
     }*/

    public void CreateWall()
    {
        if (insideWall == null)
        {

            insideWall = Instantiate(insideWallPrefab) as InsideWall;
            insideWall.transform.parent = transform;
            insideWall.transform.localPosition = new Vector3(0, 0, -0.5f);
            insideWall.name = "Wall :" + name + " ";
            GameManager.current.addWall((int)transform.position.x, (int)transform.position.z);
          
        }
    }

    public void DestroyWall() {

        Destroy(insideWall.gameObject);
        GameManager.current.removeWall((int)transform.position.x, (int)transform.position.z);
    }

}
