using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Controller : MonoBehaviour
{
    public float differenceFromOrigin=0;

    public bool isBeingDragged = false;
    
    private Collider2D collider2D;
    private bool canMove=false;
    

    // Start is called before the first frame update
    void Start()
    {
        collider2D=GetComponent<Collider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(isBeingDragged){
           GameManager.dragPiece(mousePosition,gameObject.tag);
        }
        if(Input.GetMouseButtonDown(0)){
            
            if (collider2D == Physics2D.OverlapPoint(mousePosition)){
            canMove=true;
            Debug.Log("_______________________SHOULD BE ONCE___________________");
            GameManager.setDelta(mousePosition,gameObject.tag);
            }
            else canMove=false;
            if(canMove)isBeingDragged=true;
        }
        
        if(Input.GetMouseButtonUp(0)){
            canMove=false;
            isBeingDragged=false;
        }

    }

    
}
