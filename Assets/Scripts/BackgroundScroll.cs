using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")] // unity 에서 표시!
    [Tooltip("How fast should the texture scroll?")]
    public float scrollSpeed; // 이렇게 하면 유니티에서 설정 할 수 있음

    [Header("References")]
    public MeshRenderer meshRenderer; // 이 스크립트가 렌더링에 대한 권한을 받을 수 있도록 (mesh renderer 에서 드래그앤 드롭)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.Instance.CalculateGameSpeed() / 20 * Time.deltaTime, 0); // 계속 변해야 함으로 +=
        // material 의 offset은 x,y 값 두 개만 가지고 있었기 때문에 vector 2, z 까지 있으면 vector3
        // deltaTime 은 이전 프레임에서부터 현재 프레임까지의 시간
    }
}
