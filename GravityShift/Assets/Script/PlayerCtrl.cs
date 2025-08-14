using System;
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
    
    [Header("Map")]
    public GameObject[] bottom;
    public GameObject[] Right;
    public GameObject[] Top;
    public GameObject[] Left;
    public int idx;
    
    
    [Header("Gravity")]
    public Vector3 gravity = new Vector3(0, -10, 0);
    public int gravityStrength = 10;
    bool isInGravity = false;
    [SerializeField]private float moveDuration = 0.15f,rotValue = 14f,rotDuration = 0.15f;
    private PlayerGravity playerGravity = PlayerGravity.Down;
    private PlayerEffection playerEffection;
    private float curRot = 0;
    
    
    [Header("Shift")]
    private bool OnShift;
    public float shiftCoolTime;
    private bool isshiftCoolTime;
    private bool isleftwall;
    private bool isrightwall;

    
    [Header("Move")]
    private bool OnMove;
    public float moveCoolTime;
    

    [Header("Item")]
    public bool isInvincible = false;
    public bool isAdhesion = false;
    
    
    [Header("Illusion")]
    public bool isIllusion;
    private int randomGravity;
    
    private void Awake()
    {
        playerEffection = GetComponent<PlayerEffection>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.useGravity = false;
        idx = 1; //시작 위치 가운데로
        OnShift = false;
        isshiftCoolTime = false;

        OnMove = true;
    }

    void Update()
    {
        Move();
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(gravity, ForceMode.Acceleration); //지정한 중력 방향으로 계속 힘 받기
    }

    private void Move()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            Debug.Log("LS 눌렸습니다");
            if (!OnShift)
            {
                if (isshiftCoolTime) return; // 쿨타임 중이면 아무것도 하지 않음
                TimeController.ChangeTimeScale(0.25f, 0.35f);
                playerEffection.Show();
                VolumeController.Inst.GravityProduction(true);
                OnShift = true;
            }
            else
            {
                playerEffection.Hide();
                TimeController.ChangeTimeScale(1, 0.15f);
                VolumeController.Inst.GravityProduction(false);
                OnShift = false;
            }
        }
        
         // 캐릭터 중력 방향 전환
        if (OnShift&&!isAdhesion)
        {
            
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽 벽으로 이동
            {
                if (gravity.y == -10) //중력 방향 Bottom -> Left
                    toGravityLeft();
                else if (gravity.x == -10) //중력 방향 Left -> Top
                    toGravityTop();
                else if (gravity.y == 10) //중력 방향 Top -> Right
                    toGravityRight();
                else if (gravity.x == 10) //중력 방향 Right -> Bottom
                    toGravityBottom();
                StartCoroutine(setShiftCoolTime(shiftCoolTime)); //얘는 마지막 자리에 오게 해주세요 아니면 연출이 작동이 안돼요
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (gravity.y == -10) //중력 방향 Bottom -> Right
                    toGravityRight();
                else if (gravity.x == 10) //중력 방향 Right -> Top
                    toGravityTop();
                else if (gravity.y == 10) //중력 방향 Top -> Left
                    toGravityLeft();
                else if (gravity.x == -10) //중력 방향 Left -> Bottom
                    toGravityBottom();
                StartCoroutine(setShiftCoolTime(shiftCoolTime));//얘는 마지막 자리에 오게 해주세요 아니면 연출이 작동이 안돼요
            }
        }
        //캐릭터 좌우 이동 구현 (shift 안눌렸을때)
        else if (!OnShift && !isAdhesion)
        {
            if (!OnMove) return;
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A만 눌림");
                if (idx != 0)
                {
                    Vector3 newPos = Vector3.zero;
                    idx -= 1;
                    if (gravity.y == -10) //중력 방향 Bottom
                        newPos = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        newPos = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        newPos = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        newPos = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                    
                    transform.DOMove(newPos, moveDuration);
                    transform.DOLocalRotate(new Vector3(0,0,curRot + rotValue),rotDuration)
                        .OnComplete(()=>transform.DOLocalRotate(new Vector3(0,0,curRot),rotDuration));
                    StartCoroutine(setMoveCoolTime(moveCoolTime)); 
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D만 눌림");
                if (idx != 2)
                {
                    Vector3 newPos = Vector3.zero;
                    idx += 1;
                    if (gravity.y == -10) //중력 방향 Bottom
                        newPos = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        newPos = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        newPos = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        newPos = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                    transform.DOMove(newPos, moveDuration);
                    transform.DOLocalRotate(new Vector3(0,0,curRot-rotValue),rotDuration)
                        .OnComplete(()=>transform.DOLocalRotate(new Vector3(0,0,curRot),rotDuration));
                    StartCoroutine(setMoveCoolTime(moveCoolTime)); 
                }
            }  
        }
        else if (isAdhesion)
        {
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽 벽으로 이동
            {
                if (gravity.y == -10) //중력 방향 Bottom -> Left
                    toGravityLeft();
                else if (gravity.x == -10) //중력 방향 Left -> Top
                    toGravityTop();
                else if (gravity.y == 10) //중력 방향 Top -> Right
                    toGravityRight();
                else if (gravity.x == 10) //중력 방향 Right -> Bottom
                    toGravityBottom();
                isAdhesion = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (gravity.y == -10) //중력 방향 Bottom -> Right
                    toGravityRight();
                else if (gravity.x == 10) //중력 방향 Right -> Top
                    toGravityTop();
                else if (gravity.y == 10) //중력 방향 Top -> Left
                    toGravityLeft();
                else if (gravity.x == -10) //중력 방향 Left -> Bottom
                    toGravityBottom();
                isAdhesion = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (gravity.y == -10) //중력 방향 Bottom -> top
                    toGravityTop();
                else if (gravity.x == 10) //중력 방향 Right -> Left
                    toGravityLeft();
                else if (gravity.y == 10) //중력 방향 Top -> Bottom
                    toGravityBottom();
                else if (gravity.x == -10) //중력 방향 Left -> Right
                    toGravityRight();
                isAdhesion = false;
            }
            
        }
    }

    public void ActiveInvincible(float InvincibleTime)
    {
        StartCoroutine(InvincibleCoroutine(InvincibleTime));
    }
    
    IEnumerator setShiftCoolTime(float cool)
    {
        OnShift = false;
        isshiftCoolTime = true;
        SetCurRot();
        playerEffection.Hide(playerGravity);
        CameraController.Inst.MoveCamera(playerGravity);
        TimeController.ChangeTimeScale(1, 0.15f);
        VolumeController.Inst.GravityProduction(false);
        yield return new WaitForSeconds(cool);
        isshiftCoolTime = false;
    }

    private void SetCurRot()
    {
        switch (playerGravity)
        {
            case PlayerGravity.Down:
                curRot = 0;
                break;
            case PlayerGravity.Right:
                curRot = 90;
                break;
            case PlayerGravity.Up:
                curRot = 180;
                break;
            case PlayerGravity.Left:
                curRot = 270;
                break;
                
        }
    }

    public void ActiveAdhesion()
    {
        isAdhesion = true;
    }
    
    private void toGravityLeft()
    {
        playerGravity = PlayerGravity.Left;
        rb.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        gravity = new Vector3(-10, 0, 0);
        transform.position = new Vector3( Left[idx].transform.position.x, Left[idx].transform.position.y, 0);
    }

    private void toGravityRight()
    {
        playerGravity = PlayerGravity.Right;
        rb.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        gravity = new Vector3(10, 0, 0);
        transform.position = new Vector3(Right[idx].transform.position.x, Right[idx].transform.position.y, 0);
    }

    private void toGravityTop()
    {
        playerGravity = PlayerGravity.Up;
        rb.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        gravity = new Vector3(0, 10, 0);
        transform.position = new Vector3(Top[idx].transform.position.x, Top[idx].transform.position.y, 0);
    }

    private void toGravityBottom()
    {
        playerGravity = PlayerGravity.Down;
        rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gravity = new Vector3(0, -10, 0);
        transform.position = new Vector3(bottom[idx].transform.position.x, bottom[idx].transform.position.y, 0);
    }

    public void RandomGravity()
    {
        randomGravity = UnityEngine.Random.Range(1, 4);
        if(randomGravity == 1)
            toGravityBottom();
        else if (randomGravity == 2)
            toGravityLeft();
        else if (randomGravity == 3)
            toGravityTop();
        else if (randomGravity == 4)
            toGravityRight();
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "illusion")
            isIllusion = true;

        if (obj.gameObject.tag != "illusion")
            isIllusion = false;
    }

    IEnumerator setMoveCoolTime(float cool)
    {
        OnMove = false;
        yield return new WaitForSeconds(cool);
        OnMove = true;
    }

    IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}