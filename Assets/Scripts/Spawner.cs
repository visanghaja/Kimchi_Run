using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Settings")]
    public float minSpawnDelay; // spawn 되는 주기를 직접 설정해주기 위해서
    public float maxSpawnDelay;

    [Header("References")]
    public GameObject[] gameObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() // game object 가 활성화 될때마다 호출되는 메서드
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable(){
        CancelInvoke(); // 더이상 생성되지 않도록!
    }
    void Spawn(){
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)]; // 랜덤으로 설정
        Instantiate(randomObject, transform.position, Quaternion.identity); // Quaternion.identity 는 unity 에서 회전을 표현하는 방법
        // randomObject 값이 인스턴스화 되어서 스포너의 포지션 위에다 두게됨
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); // 일정 시간 후에 함수가 또 호출됨
    }
}
