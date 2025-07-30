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

    public Vector2 left;
    public Vector2 middle;
    public Vector2 right;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        OnShift = false;
        isCoolTime = false;
    }
    
    void Update()
    {

        pos = transform.position;
        Time.timeScale = 1f;

        // LShift 입력될 시        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("LS 눌렸습니다");
            // 1. Shift 첫 활성화시 => LSBool == true
            if (!OnShift) // Shitf가 비활성 상태
            {
                if (isCoolTime)
                {
                    // 아직 쿨타임
                    return;
                }
                else
                {
                    OnShift = true;
                }
            }
            else
            {
                OnShift = false;
                // TODO: 만약 중력 안써도 쿨타임 돌아야되면?
                // SetCoolTime()
            }
        }
        
        // 캐릭터 중력 방향 전환
        if (OnShift)     // Shift 활성화 상태
        {
            Time.timeScale = 0.5f;
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽 방향으로 중력변환
            {
                if (gravity.y == -10) //중력 방향 Bottom -> Left
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    gravity = new Vector3(-10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(3, 9, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(3,6,0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(3,3,0);
                }
                else if (gravity.x == -10) //중력 방향 Left -> Top
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
                else if (gravity.y == 10) //중력 방향 Top -> Right
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
                else if (gravity.x == 10) //중력 방향 Right -> Bottom
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
            
            if (Input.GetKeyDown(KeyCode.D)) //오른쪽 방향으로 중력 변환
            {
                Debug.Log("Shift+D 눌렸습니다");
                if (gravity.y == -10) //중력 방향 Bottom -> Right
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    gravity = new Vector3(10, 0, 0);
                    if (2 <= pos.x && pos.x <= 4)
                        pos = new Vector3(9, 3, 0);
                    else if (5 <= pos.x && pos.x <= 7)
                        pos = new Vector3(9,6,0);
                    else if (8 <= pos.x && pos.x <= 10)
                        pos = new Vector3(9,9,0);
                }       
                else if (gravity.x == 10) //중력 방향 Right -> Top
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
                else if (gravity.y == 10) //중력 방향 Top -> Left
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
                else if (gravity.x == -10) //중력 방향 Left -> Bottom
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

        RaycastHit hitinfo;
        
        if ((Physics.Raycast(this.transform.position, -this.transform.right, out hitinfo, 2f)))
        {
            isleftwall = false;
        }
        else
        {
            isleftwall = true;
        }
        
        if (Physics.Raycast(this.transform.position, this.transform.right, out hitinfo, 2f))
        {
            isrightwall = false;
        }
        else
        {
            isrightwall = true;
        }
        
      
        // 왼쪽으로 이동
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isleftwall)
            {
                if (gravity.y == -10) //bottom일 때
                {
                    pos.x -= 3;
                }

                if (gravity.x == -10) //left일 때
                {
                    pos.y += 3;
                }

                if (gravity.y == 10) // top일 때
                {
                    pos.x += 3;
                }

                if (gravity.x == 10) // right일 때
                {
                    pos.y -= 3;
                }
                transform.position = pos;
            }
        }
        
        // 오른쪽으로 이동
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isrightwall)
            {
                if (gravity.y == -10) //bottom일 때
                {
                    pos.x += 3;
                }

                if (gravity.x == -10) //left일 때
                {
                    pos.y -= 3;
                }

                if (gravity.y == 10) // top일 때
                {
                    pos.x -= 3;
                }

                if (gravity.x == 10) // right일 때
                {
                    pos.y += 3;
                }
                transform.position = pos;
            }
           
        }
        
        rb.AddForce(gravity, ForceMode.Acceleration); //지정한 중력 방향으로 계속 힘 받기
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
