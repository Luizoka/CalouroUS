using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gato : MonoBehaviour
{
    public float speed;
    public bool ground = true;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        if(ground == true){
            speed *= -1;

        } 

        if(speed > 0 && !facingRight){
            Flip();
        }   

        if(speed < 0 && facingRight){
            Flip();
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scale = transform.lossyScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
