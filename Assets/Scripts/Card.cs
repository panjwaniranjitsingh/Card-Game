using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite frontFace;
    [SerializeField] private Sprite backFace;
    public bool isFrontFace;
    private SpriteRenderer spriteRenderer;
    public int cardValue;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isFrontFace && frontFace != null)
        {
            spriteRenderer.sprite = frontFace;
        }
        else
        {
            spriteRenderer.sprite = backFace;
        }
    }
    public void SetFrontFace(Sprite cardFace)
    {
        frontFace = cardFace;
    }
}
