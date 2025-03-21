using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -15){ // gameObject 가 화면 밖으로 나가면
            Destroy(gameObject); // gameObject 파괴 (Destroy 함수 사용해서)
        }
    }
}
