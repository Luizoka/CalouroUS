using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer Wire_end;
    Vector3 startPoint;
    Vector3 startPosition;
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        //Start is called before the first frame update
        startPoint = transform.parent.position;  
        startPosition = transform.position;                                               
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);
                if(transform.parent.name.Equals(collider.transform.parent.name))
                {
                  //finish step
                  collider.GetComponent<Wire>()?.Done();
                  Done();
                }
                return;
            }
        }


        //update wire
        UpdateWire(newPosition);
    }

    void Done(){
        //turn on light
        Light.SetActive(true);

        // destory the script
        Destroy(this);
    }

    private void OnMouseUp()
    {
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
         //update position
        transform.position = newPosition;

        // update direction 
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //update scale
        float dist = Vector2.Distance(startPoint, newPosition);
        Wire_end.size = new Vector2(dist, Wire_end.size.y);
    }
}
