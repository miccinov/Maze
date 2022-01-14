using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    public Text testo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)     {
            for (int i = 0; i < nbTouches; i++)            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)                {
                    Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(screenRay, out hit))                  { 
                        GameObject hitted = hit.collider.gameObject;
                        
                        if (hitted.tag.Equals("Floor"))                        {
                            if (!hitted.transform.position.Equals(GameManager.current.startPos) && !hitted.transform.position.Equals(GameManager.current.endPos) )
                             hitted.GetComponent<Floor>().CreateWall();
                        }
                        if (hitted.tag.Equals("InsideWall"))                        {
                            hitted.GetComponentInParent<InsideWall>().Destroy();
                        }
                    }
                }

            }
        }
    }

      
}
