using System.Collections;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject[] bottom;
    public GameObject[] Right;
    public GameObject[] Top;
    public GameObject[] Left;
    public int idx;
    
    public Vector3 gravity = new Vector3(0, -10, 0);
    public int gravityStrength = 10;
    
    bool isInGravity = false;
<<<<<<< Updated upstream
    [SerializeField]private float moveDuration = 0.15f,rotValue = 14f,rotDuration = 0.15f;
=======

    public bool OnShift;
    public float cooltime;
    public bool isCoolTime;
    public bool isleftwall;
    public bool isrightwall;

>>>>>>> Stashed changes
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        idx = 1; //시작 위치 가운데로
        OnShift = false;
        isCoolTime = false;

    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isInGravity = !isInGravity;
            TimeController.ChangeTimeScale(isInGravity?0.25f:1, isInGravity?0.35f:0.15f);
            VolumeController.Inst.GravityProduction(isInGravity);
           
        }
    }

    private void Move()
    {
        
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
        
         // 캐릭터 중력 방향 전환
        if (OnShift)
        {
            Time.timeScale = 0.5f;
            
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽 벽으로 이동
            {
                Debug.Log("Shift+A 눌렸습니다");
                if (gravity.y == -10) //중력 방향 Bottom -> Left
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    gravity = new Vector3(-10, 0, 0);
                    transform.position = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                }
                else if (gravity.x == -10) //중력 방향 Left -> Top
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    gravity = new Vector3(0, 10, 0);
                    transform.position = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                }
                else if (gravity.y == 10) //중력 방향 Top -> Right
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    gravity = new Vector3(10, 0, 0);
                    transform.position = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                }
                else if (gravity.x == 10) //중력 방향 Right -> Bottom
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gravity = new Vector3(0, -10, 0);
                    transform.position = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                }
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Shift+D 눌렸습니다");
                if (gravity.y == -10) //중력 방향 Bottom -> Right
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    gravity = new Vector3(10, 0, 0);
                    transform.position = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                }       
                else if (gravity.x == 10) //중력 방향 Right -> Top
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    gravity = new Vector3(0, 10, 0);
                    transform.position = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                }
                else if (gravity.y == 10) //중력 방향 Top -> Left
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    gravity = new Vector3(-10, 0, 0);
                    transform.position = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                }
                else if (gravity.x == -10) //중력 방향 Left -> Bottom
                {
                    rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gravity = new Vector3(0, -10, 0);
                    transform.position = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                }
            }
        }
        //캐릭터 좌우 이동 구현 (shift 안눌렸을때)
        else
        {
            Time.timeScale = 1f;
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A만 눌림");
                if (idx != 0)
                {
                    idx -= 1;
                    if (gravity.y == -10) //중력 방향 Bottom
                    {
                        transform.DOMove(new Vector3(bottom[idx].transform.position.x, transform.position.y, 0), moveDuration);
                        transform.DOLocalRotate(new Vector3(0,0,rotValue),rotDuration)
                            .OnComplete(()=>transform.DOLocalRotate(Vector3.zero, rotDuration));
                    }
                        //transform.position = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        transform.position = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        transform.position = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        transform.position = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D만 눌림");
                if (idx != 2)
                {
                    idx += 1;
                    if (gravity.y == -10) //중력 방향 Bottom
                    {
                        transform.DOMove(new Vector3(bottom[idx].transform.position.x, transform.position.y, 0), moveDuration);
                        transform.DOLocalRotate(new Vector3(0,0,-rotValue),rotDuration)
                            .OnComplete(()=>transform.DOLocalRotate(Vector3.zero, rotDuration));
                    }
                        //transform.position = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        transform.position = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        transform.position = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        transform.position = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                }
            }  
        }
        rb.AddForce(gravity, ForceMode.Acceleration); //지정한 중력 방향으로 계속 힘 받기
    }
    
    IEnumerator setCoolTime(float cool)
    {
        OnShift = false;
        isCoolTime = true;
        yield return new WaitForSeconds(cool);
        isCoolTime = false;
    }
    
    
    
    
}
