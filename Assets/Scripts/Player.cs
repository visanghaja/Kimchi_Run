using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody; // RigidBody2D 로 부터 받아옴
    public Animator PlayerAnimator; // animator 에서 받아오기
    public BoxCollider2D PlayerCollider;
    
    private bool isGrounded = true;

    public bool isInvincible = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded){ // Input 으로 유저가 입력하는 값을 받음!
        // isGrounded 로 계속 점프 못하도록
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse); // ForceMode2D.Impulse 이거는 즉시 힘을 가한다는 이야기
            // PlayerRigidBody 로 부터 받아와서...
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name.Equals("Platform")){ // 만약에 충돌이 일어난게 플랫폼이라면 isGrounded를 true 로 바꿔줌
            if(!isGrounded){
                PlayerAnimator.SetInteger("state", 2); // 처음 시작할때는 발동 되지 않도록
            }
            isGrounded = true;
        }
    }
    
    public void KillPlayer() {
        PlayerCollider.enabled = false; // player 의 collider false로 !
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
    void Hit() {
        GameManager.Instance.Lives -= 1;
    }

    void Heal() {
         GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1); // 3 또는 lives + 1 중 더 작은 값을 반환하도록
    }

    void StartInvincible() {
        isInvincible = true;
        Invoke("StopInvincible", 5f); // 5초 동안 무적!
    }

    void StopInvincible() {
        isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Enemy")){
            if(!isInvincible){
                Destroy(collider.gameObject);
            }
            Hit();
        }
        else if(collider.gameObject.tag.Equals("food")){
            Destroy(collider.gameObject);
            Heal();
        }
        else if(collider.gameObject.tag.Equals("golden")){
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
