using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; //0:l 1:m 2:r
    public float laneDistance = 4; //the dis between two lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        direction.z = forwardSpeed;

        if(controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetProsition = transform.position.z * transform.forward + transform.position.y * transform.up;
    
        if(desiredLane == 0)
        {
            targetProsition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetProsition += Vector3.right * laneDistance;
        }

        if (transform.position == targetProsition)
            return;
        Vector3 diff = targetProsition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        if (PlayerManager.isGameStarted)
        {
            controller.Move(direction * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("Over");
        }
    }
}

//private CharacterController controller;: ��û�С�ȵ���� controller �繵�ǤǺ�������Ф�.
//private Vector3 direction;: ���ǡ�����ȷҧ�ͧ�������͹���ͧ������.
//public float forwardSpeed;: �����������˹��.
//public float maxSpeed;: ���������٧�ش������������ö�����.
//private int desiredLane = 1;: ��ͧ�ҧ�������蹵�ͧ����(0: ����, 1: ��ҧ, 2: ���).
//public float laneDistance = 4;: ������ҧ�����ҧ��ͧ�ҧ.
//public bool isGrounded;: ��Ǩ�ͺ��Ҽ��������躹����������.
//public LayerMask groundLayer;: �ʴ���鹢ͧ���.
//public Transform groundCheck;: ���˹觷����㹡�õ�Ǩ�ͺ��Ҽ��������躹����������.
//public float jumpForce;: �ç���ⴴ.
//public float Gravity = -20;: �ç�����ǧ.
//public Animator animator;: ��ҧ�ԧ��ѧ Animator �ͧ����Ф�.

//else: �����������躹���(�������觡��ⴴ) ��������ç�����ǧ���ŧ�Ҵ��� Gravity * Time.deltaTime.
//��õ�Ǩ�ͺ input �������蹡�:
//Input.GetKeyDown(KeyCode.D): ��Ҽ����蹡����� D, �зӡ������¹ lane 价ҧ��� (desiredLane �������).
//Input.GetKeyDown(KeyCode.A): ��Ҽ����蹡����� A, �зӡ������¹ lane 价ҧ���� (desiredLane Ŵŧ).
//Vector3 targetProsition = transform.position.z * transform.forward + transform.position.y * transform.up;: ��˹����˹觷������蹵�ͧ����(target position) ����� Z ��ҡѺ���˹� Z �Ѩ�غѹ�ͧ����Ф� ��� Y ��ҡѺ���˹� Y �Ѩ�غѹ�ͧ����Ф�.
//if (desiredLane == 0) ... else if (desiredLane == 2) ...: ��˹����˹������ lane ����� target position ����͹价ҧ�������͢�ҵ�� lane �����������͡.
//if (transform.position == targetProsition) return;: ����觹���Ǩ�ͺ��ҵ��˹觻Ѩ�غѹ�ͧ����Ф�(transform.position) �ç�Ѻ���˹觷���ͧ����(targetProsition) ������� ��ҵç�ѹ���� �ѧ��ѹ����ش�ӧҹ�ѹ���¡�������� return.
//Vector3 diff = targetProsition - transform.position;: ��Ǩ�ͺ������ҧ�����ҧ���˹觻Ѩ�غѹ�Ѻ���˹觷���ͧ���� �¹ӵ��˹觷���ͧ����ź�Ѻ���˹觻Ѩ�غѹ.
//Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;: ��ȷҧ�ͧ�������͹��� ��蹤�� diff ���١ normalize ��Фٳ���� 25 * Time.deltaTime.
//if (moveDir.sqrMagnitude < diff.sqrMagnitude) ... else ...: �ӡ�����º��º������Ǣͧ�ǡ���� moveDir �Ѻ diff. ��� magnitude �ͧ moveDir ���¡��� diff ���� moveDir �繷�ȷҧ�������͹��� �����ҡ����������ҡѹ���� diff.
//controller.Move(...);: �ӡ�����µ��˹觢ͧ����Ф� ���� Controller. �ҡ��鹵���Фè�����͹�������ȷҧ������͡ (��� moveDir ���� diff).
//controller.Move(...): �ӡ�����µ��˹觢ͧ����Ф� ���� Controller.
//private void FixedUpdate(): �ѧ��ѹ�ӧҹ㹷ء� �����ӧҹ��š�ҧ��¿�� ������͹�������ȷҧ����˹�.
//if (PlayerManager.isGameStarted) ...: ����������������(isGameStarted = true) ���ӡ������͹�������ȷҧ����˹�.