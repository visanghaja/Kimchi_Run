using UnityEngine;

public class Heart : MonoBehaviour
{
    [Header("References")]
    public Sprite OnHeart;
    public Sprite OffHeart;
    public SpriteRenderer SpriteRenderer; // sprite render 해주는 역할
    public int LiveNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Lives >= LiveNumber){
            SpriteRenderer.sprite = OnHeart;
        }
        else {
            SpriteRenderer.sprite = OffHeart;
        }
    }
}
