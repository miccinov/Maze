using UnityEngine;
using UnityEngine.UI;

public class OutsideWall : MonoBehaviour
{
    // Start is called before the first frame update

    public Text testo;

    
    void Start()
    {
        GetComponentInChildren<MeshRenderer>().sortingLayerName = "Wall";
        GetComponentInChildren<MeshRenderer>().sortingOrder = 1;
    }
    private void Awake()
    {
        testo = GameObject.Find("Text").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        checkClick();


    }
    private void OnMouseDown()
    {
        Debug.Log(tag);
    }
    public void checkClick()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name.Equals(name))
                    {
                        
                    }
                }

            }
        }
    }
}
