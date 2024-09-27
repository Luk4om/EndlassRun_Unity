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

//private CharacterController controller;: การประกาศตัวแปร controller เป็นตัวควบคุมตัวละคร.
//private Vector3 direction;: เก็บเวกเตอร์ทิศทางของการเคลื่อนที่ของผู้เล่น.
//public float forwardSpeed;: ความเร็วในแนวหน้า.
//public float maxSpeed;: ความเร็วสูงสุดที่ผู้เล่นสามารถวิ่งได้.
//private int desiredLane = 1;: ช่องทางที่ผู้เล่นต้องการไป(0: ซ้าย, 1: กลาง, 2: ขวา).
//public float laneDistance = 4;: ระยะห่างระหว่างช่องทาง.
//public bool isGrounded;: ตรวจสอบว่าผู้เล่นอยู่บนพื้นหรือไม่.
//public LayerMask groundLayer;: แสดงชั้นของพื้น.
//public Transform groundCheck;: ตำแหน่งที่ใช้ในการตรวจสอบว่าผู้เล่นอยู่บนพื้นหรือไม่.
//public float jumpForce;: แรงกระโดด.
//public float Gravity = -20;: แรงโน้มถ่วง.
//public Animator animator;: อ้างอิงไปยัง Animator ของตัวละคร.

//else: ถ้าไม่ได้อยู่บนพื้น(ไม่ได้สั่งกระโดด) ให้เพิ่มแรงโน้มถ่วงในแนวลงมาด้วย Gravity * Time.deltaTime.
//การตรวจสอบ input ที่ผู้เล่นกด:
//Input.GetKeyDown(KeyCode.D): ถ้าผู้เล่นกดปุ่ม D, จะทำการเปลี่ยน lane ไปทางขวา (desiredLane เพิ่มขึ้น).
//Input.GetKeyDown(KeyCode.A): ถ้าผู้เล่นกดปุ่ม A, จะทำการเปลี่ยน lane ไปทางซ้าย (desiredLane ลดลง).
//Vector3 targetProsition = transform.position.z * transform.forward + transform.position.y * transform.up;: กำหนดตำแหน่งที่ผู้เล่นต้องการไป(target position) โดยให้ Z เท่ากับตำแหน่ง Z ปัจจุบันของตัวละคร และ Y เท่ากับตำแหน่ง Y ปัจจุบันของตัวละคร.
//if (desiredLane == 0) ... else if (desiredLane == 2) ...: กำหนดตำแหน่งในแต่ละ lane โดยให้ target position เลื่อนไปทางซ้ายหรือขวาตาม lane ที่ผู้เล่นเลือก.
//if (transform.position == targetProsition) return;: คำสั่งนี้ตรวจสอบว่าตำแหน่งปัจจุบันของตัวละคร(transform.position) ตรงกับตำแหน่งที่ต้องการไป(targetProsition) หรือไม่ ถ้าตรงกันแล้ว ฟังก์ชันจะหยุดทำงานทันทีโดยการใช้คำสั่ง return.
//Vector3 diff = targetProsition - transform.position;: ตรวจสอบความต่างระหว่างตำแหน่งปัจจุบันกับตำแหน่งที่ต้องการไป โดยนำตำแหน่งที่ต้องการไปลบกับตำแหน่งปัจจุบัน.
//Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;: ทิศทางของการเคลื่อนที่ นั่นคือ diff ที่ถูก normalize และคูณด้วย 25 * Time.deltaTime.
//if (moveDir.sqrMagnitude < diff.sqrMagnitude) ... else ...: ทำการเปรียบเทียบความยาวของเวกเตอร์ moveDir กับ diff. ถ้า magnitude ของ moveDir น้อยกว่า diff จะใช้ moveDir เป็นทิศทางการเคลื่อนที่ แต่ถ้ามากกว่าหรือเท่ากันจะใช้ diff.
//controller.Move(...);: ทำการย้ายตำแหน่งของตัวละคร โดยใช้ Controller. จากนั้นตัวละครจะเคลื่อนที่ตามทิศทางที่เลือก (คือ moveDir หรือ diff).
//controller.Move(...): ทำการย้ายตำแหน่งของตัวละคร โดยใช้ Controller.
//private void FixedUpdate(): ฟังก์ชันทำงานในทุกๆ เฟรมแต่ทำงานในโลกทางลอยฟ้า โดยเคลื่อนที่ตามทิศทางที่กำหนด.
//if (PlayerManager.isGameStarted) ...: ถ้าเกมเริ่มต้นแล้ว(isGameStarted = true) ให้ทำการเคลื่อนที่ตามทิศทางที่กำหนด.