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
    public GameObject[] bottom;
    public GameObject[] Right;
    public GameObject[] Top;
    public GameObject[] Left;
    [SerializeField] private Transform mark,shield;
    public int idx;
    
    public Vector3 gravity = new Vector3(0, -10, 0);
    public int gravityStrength = 10;
    
    bool isInGravity = false;
    [SerializeField]private float moveDuration = 0.15f,rotValue = 14f,rotDuration = 0.15f;
    
    [Header("Illusion")]
    public bool isIllusion;

    private int randomGravity;
    
    private bool OnShift;
    public float shiftCoolTime;
    private bool isshiftCoolTime;
    private bool isleftwall;
    private bool isrightwall;

    private bool OnMove;
    private Vector3 rayDirVector;
    private Vector3 markSize;
    public float moveCoolTime;
    
    public PlayerGravity playerGravity = PlayerGravity.Down;
    private PlayerEffection playerEffection;
    private PlayerKeyController playerKeyController;
    private Animator animator;
    private float curRot = 0;

    public bool isInvincible = false;
    public bool isAdhesion = false;
    
    private void Awake()
    {
        playerEffection = GetComponent<PlayerEffection>();
        rb = GetComponent<Rigidbody>();
        playerKeyController = GetComponent<PlayerKeyController>();
        animator = GetComponent<Animator>();
        markSize = mark.localScale;
        mark.localScale = Vector3.zero;
    }

    void Start()
    {
        rb.useGravity = false;
        idx = 1; //시작 위치 가운데로
        OnShift = false;
        isshiftCoolTime = false;
        shield.gameObject.SetActive(false);
        OnMove = true;
    }

    void Update()
    {
        if (!OnMove) return;
        Move();
    }

    public void PlayerDead(bool isFall)
    {
        gravityStrength = 0;
        gravity = Vector3.zero;
       // rb.mass = 0;
        animator.enabled = false;
        transform.DOKill();
        playerKeyController.ChangeState(PlayerState.NoneKey);
        VolumeController.Inst.GravityProduction(false);
        playerEffection.Hide(playerGravity);
        if (isFall)
        {
            d();
            Debug.Log("바닥이 없음");
        }
        else
        {
            Debug.Log("가시에 걸림");
            SettingController.Inst.EndingUI(false);
        }
       
    }

    private void d()
    {
        SetRayDir();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.1f);
        seq.Append(mark.DOScale(markSize,0.1f));
        seq.AppendInterval(0.5f);
        seq.Append(transform.DOMove(transform.position + (rayDirVector * 10), 0.5f));
        seq.JoinCallback(()=>SFXManager.Inst.PlaySound("fallDie"));
        seq.AppendInterval(0.7f);
        seq.AppendCallback(()=>SettingController.Inst.EndingUI(false));
        seq.SetUpdate(true);
        seq.Play();

    }
    
    void SetRayDir()
    {
        switch (playerGravity)
        {
            case PlayerGravity.Down:
                rayDirVector = Vector3.down;
                break;
            case PlayerGravity.Up:
                rayDirVector = Vector3.up;
                break;
            case PlayerGravity.Right:
                rayDirVector = Vector3.right;
                break;
            case PlayerGravity.Left:
                rayDirVector = Vector3.left;
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(gravity, ForceMode.Acceleration); //지정한 중력 방향으로 계속 힘 받기
    }

    private void Move()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerKeyController.CheckCurState(PlayerState.OnlyShift))
        {
            
            Debug.Log("LS 눌렸습니다");
            if (!OnShift)
            {
                if (isshiftCoolTime) return; // 쿨타임 중이면 아무것도 하지 않음
                SetDirEffction();
                GravityEffect(true);
                OnShift = true;
            }
            else
            {
                GravityEffect(false);
                GravityDirEffctionController.Inst.HideEffection();
                OnShift = false;
            }
        }

        if (!playerKeyController.CheckCurState(PlayerState.OnlyLR)) return;
        
         // 캐릭터 중력 방향 전환
        if (OnShift&&!isAdhesion)
        {
            
            if (Input.GetKeyDown(KeyCode.A)) //왼쪽 벽으로 이동
            {
                transform.DOKill();
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
                transform.DOKill(); 
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
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A만 눌림");
                if (idx != 0)
                {
                    Vector3 newPos = Vector3.zero;
                    idx -= 1;
                    SFXManager.Inst.PlaySound("playerLRmove");
                    if (gravity.y == -10) //중력 방향 Bottom
                        newPos = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        newPos = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        newPos = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        newPos = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                    
                    transform.DOMove(newPos, moveDuration).SetUpdate(true);
                    transform.DOLocalRotate(new Vector3(0,0,curRot + rotValue),rotDuration)
                        .OnComplete(()=>transform.DOLocalRotate(new Vector3(0,0,curRot),rotDuration)).SetUpdate(true);
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
                    SFXManager.Inst.PlaySound("playerLRmove");
                    if (gravity.y == -10) //중력 방향 Bottom
                        newPos = new Vector3(bottom[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == -10) //중력 방향 Left
                        newPos = new Vector3(transform.position.x, Left[idx].transform.position.y, 0);
                    else if (gravity.y == 10) //중력 방향 Top
                        newPos = new Vector3(Top[idx].transform.position.x, transform.position.y, 0);
                    else if (gravity.x == 10) //중력 방향 Right
                        newPos = new Vector3(transform.position.x, Right[idx].transform.position.y, 0);
                    transform.DOMove(newPos, moveDuration).SetUpdate(true);
                    transform.DOLocalRotate(new Vector3(0,0,curRot-rotValue),rotDuration)
                        .OnComplete(()=>transform.DOLocalRotate(new Vector3(0,0,curRot),rotDuration)).SetUpdate(true);
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
                GravityEffect(false);
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
                GravityEffect(false);
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
                GravityEffect(false);
            }
            
        }
    }

    public void ActiveInvincible(float InvincibleTime)
    {
        SFXManager.Inst.PlaySound("useItem");
        StartCoroutine(InvincibleCoroutine(InvincibleTime));
    }
    
    IEnumerator setShiftCoolTime(float cool)
    {
        OnShift = false;
        isshiftCoolTime = true;
        GravityEffect(false);
        yield return new WaitForSeconds(cool);
        isshiftCoolTime = false;
    }

    private void GravityEffect(bool isShow,bool isItem = false)
    {
        if (isShow)
        {
            playerEffection.Show(isItem);
            TimeController.ChangeTimeScale(0.25f, 0.35f);
        }
        else
        {
            GravityDirEffctionController.Inst.HideEffection();
            playerEffection.Hide(playerGravity);
            TimeController.ChangeTimeScale(1, 0.15f);
        }
        SetCurRot();
        CameraController.Inst.MoveCamera(playerGravity);
        VolumeController.Inst.GravityProduction(isShow);
    }
    
    public void RandomGravity()
    {
        transform.DOKill();
        randomGravity = UnityEngine.Random.Range(1, 5);
        SFXManager.Inst.PlaySound("illusionChange");
        switch (randomGravity)
        {
            case 1:
                toGravityBottom();
                break;
            case 2:
                toGravityLeft();
                break;
            case 3:
                toGravityTop();
                break;
            case 4:
                toGravityRight();
                break;
        }
        SetCurRot();
        CameraController.Inst.MoveCamera(playerGravity);
        Debug.Log(randomGravity + "로 중력변환 했습니다");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("illusion"))
        {
            Debug.Log("환각존입니다");
            isIllusion = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("illusion"))
        {
            isIllusion = false;
        }
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

    private void SetDirEffction(bool isItem = false)
    {
        GravityDirEffctionController.Inst.ShowEffction(Top[idx].transform,bottom[idx].transform, 
            Left[idx].transform,Right[idx].transform,isItem,playerGravity);
    }

    public void ActiveAdhesion()
    {
        isAdhesion = true;
        SFXManager.Inst.PlaySound("useItem");
        SetDirEffction(true);
        GravityEffect(true,true);
    }
    
    private void toGravityLeft()
    {
        playerGravity = PlayerGravity.Left;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        gravity = new Vector3(-10, 0, 0);
        transform.position = new Vector3( Left[idx].transform.position.x, Left[idx].transform.position.y, 0);
    }

    private void toGravityRight()
    {
        playerGravity = PlayerGravity.Right;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        gravity = new Vector3(10, 0, 0);
        transform.position = new Vector3(Right[idx].transform.position.x, Right[idx].transform.position.y, 0);
    }

    private void toGravityTop()
    {
        playerGravity = PlayerGravity.Up;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        gravity = new Vector3(0, 10, 0);
        transform.position = new Vector3(Top[idx].transform.position.x, Top[idx].transform.position.y, 0);
    }

    private void toGravityBottom()
    {
        playerGravity = PlayerGravity.Down;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gravity = new Vector3(0, -10, 0);
        transform.position = new Vector3(bottom[idx].transform.position.x, bottom[idx].transform.position.y, 0);
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
        shield.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        shield.gameObject.SetActive(false);
        isInvincible = false;
    }
}