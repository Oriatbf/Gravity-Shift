using UnityEngine;
using System.Collections;

public class NewPlayerCtrl : MonoBehaviour
{
    private Rigidbody rb;
    
    public Vector3 gravity = new Vector3(0, -10, 0);
    public int gravityStrength = 10;
    private Vector3 pos;
    public bool OnShift;
    public float cooltime;
    public bool isCoolTime;
    public bool isleftwall;
    public bool isrightwall;

    // --- Boxcast 설정을 위한 변수 ---
    // 캐릭터 크기에 맞게 Inspector 창에서 조절하세요.
    public Vector3 boxSize = new Vector3(0.4f, 0.9f, 0.4f); 
    // 벽에 얼마나 가까이 붙을 수 있는지 결정합니다.
    public float maxDistance = 0.6f;

    // public Vector2 left; // 이 변수들은 코드에서 사용되지 않아 주석 처리하거나 삭제해도 됩니다.
    // public Vector2 middle;
    // public Vector2 right;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        OnShift = false;
        isCoolTime = false;
    }
    
    // Update 함수 전체를 아래 코드로 교체
    void Update()
    {
        pos = transform.position;

        // LShift 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("LS 눌렸습니다");
            if (!OnShift)
            {
                if (isCoolTime) return; // 쿨타임 중이면 아무것도 하지 않음
                OnShift = true;
            }
            else
            {
                OnShift = false;
            }
        }

        // --- Shift 활성화 (중력 전환) 로직 ---
        if (OnShift)
        {
            Time.timeScale = 0.5f;
            
            // 왼쪽 방향으로 중력변환
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (gravity.y == -10) // 중력 방향 Bottom -> Left
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    gravity = new Vector3(-10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(3, 9, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(3, 6, 0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(3, 3, 0);
                }
                else if (gravity.x == -10) // 중력 방향 Left -> Top
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    gravity = new Vector3(0, 10, 0);
                    if (2 <= pos.y && pos.y <= 4)
                        pos = new Vector3(3, 9, 0);
                    else if (5 <= pos.y && pos.y <= 7)
                        pos = new Vector3(6, 9, 0);
                    else if (8 <= pos.y && pos.y <= 10)
                        pos = new Vector3(9, 9, 0);
                }
                else if (gravity.y == 10) // 중력 방향 Top -> Right
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    gravity = new Vector3(10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(9, 9, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(9, 6, 0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(9, 3, 0);
                }
                else if (gravity.x == 10) // 중력 방향 Right -> Bottom
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gravity = new Vector3(0, -10, 0);
                    if (2 <= pos.y && pos.y <= 4)
                        pos = new Vector3(3, 3, 0);
                    else if (5 <= pos.y && pos.y <= 7)
                        pos = new Vector3(6, 3, 0);
                    else if (8 <= pos.y && pos.y <= 10)
                        pos = new Vector3(9, 3, 0);
                }
                StartCoroutine(setCoolTime(cooltime));
            }
            
            // 오른쪽 방향으로 중력 변환
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Shift+D 눌렸습니다");
                if (gravity.y == -10) // 중력 방향 Bottom -> Right
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    gravity = new Vector3(10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(9, 3, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(9, 6, 0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(9, 9, 0);
                }
                else if (gravity.x == 10) // 중력 방향 Right -> Top
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    gravity = new Vector3(0, 10, 0);
                    if (2 <= pos.y && pos.y <= 4)
                        pos = new Vector3(9, 9, 0);
                    else if (5 <= pos.y && pos.y <= 7)
                        pos = new Vector3(6, 9, 0);
                    else if (8 <= pos.y && pos.y <= 10)
                        pos = new Vector3(3, 9, 0);
                }
                else if (gravity.y == 10) // 중력 방향 Top -> Left
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    gravity = new Vector3(-10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(3, 3, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(3, 6, 0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(3, 9, 0);
                }
                else if (gravity.x == -10) // 중력 방향 Left -> Bottom
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gravity = new Vector3(0, -10, 0);
                    if (2 <= pos.y && pos.y <= 4)
                        pos = new Vector3(9, 3, 0);
                    else if (5 <= pos.y && pos.y <= 7)
                        pos = new Vector3(6, 3, 0);
                    else if (8 <= pos.y && pos.y <= 10)
                        pos = new Vector3(3, 3, 0);
                }
                StartCoroutine(setCoolTime(cooltime));
            }
            transform.position = pos;
        }
        // --- Shift 비활성화 (일반 이동) 로직 ---
        else
        {
            Time.timeScale = 1f;

            RaycastHit hit;
            
            // Boxcast를 사용한 벽 감지
            isleftwall = Physics.BoxCast(transform.position, boxSize, -transform.right, out hit, transform.rotation, maxDistance);
            isrightwall = Physics.BoxCast(transform.position, boxSize, transform.right, out hit, transform.rotation, maxDistance);

            
            // 왼쪽으로 이동
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!isleftwall) // 왼쪽에 벽이 없을 때만 이동
                {
                    if (gravity.y == -10) pos.x -= 3;
                    if (gravity.x == -10) pos.y += 3;
                    if (gravity.y == 10) pos.x += 3;
                    if (gravity.x == 10) pos.y -= 3;
                    transform.position = pos;
                }
            }

            // 오른쪽으로 이동
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!isrightwall) // 오른쪽에 벽이 없을 때만 이동
                {
                    if (gravity.y == -10) pos.x += 3;
                    if (gravity.x == -10) pos.y -= 3;
                    if (gravity.y == 10) pos.x -= 3;
                    if (gravity.x == 10) pos.y += 3;
                    transform.position = pos;
                }
            }
        }

        // 지정한 중력 방향으로 계속 힘 받기
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    ///<summary>
    ///쿨타임 후 키 입력 가능으로 전환
    ///</summary>
    IEnumerator setCoolTime(float cool)
    {
        OnShift = false;
        isCoolTime = true;
        yield return new WaitForSeconds(cool);
        isCoolTime = false;
    }
}