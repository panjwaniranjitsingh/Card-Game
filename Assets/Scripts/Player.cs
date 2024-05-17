using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int sumOfCardValues;
    private CardManager cardManager;
    private void Start()
    {
        cardManager = FindObjectOfType<CardManager>();
    }

    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                Card selectedCard = hit.collider.GetComponent<Card>();
                //Debug.Log("From Mouse "+ selectedCard.name);
                cardManager.HandlePlayerInput(selectedCard);
                return;
            }
        }
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -10));
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (hit.collider != null)
                    {
                        Card selectedCard = hit.collider.GetComponent<Card>();
                        //Debug.Log("From Touch " + selectedCard.name);
                        cardManager.HandlePlayerInput(selectedCard);
                    }
                    break;
            }
        }
    }
}
