using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public Text CoinText;
    public Text HighCoinText;
    int Coin = 0;
    int HighCoin = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        HighCoin = PlayerPrefs.GetInt("HighCoin", 0);
        CoinText.text = "Coins: " + Coin.ToString();
        HighCoinText.text = "HighCoins: " + HighCoin.ToString();
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
    public void AddCoin()
    {
        Coin += 1;
        CoinText.text = "Coins: " + Coin.ToString();
        if (HighCoin < Coin)
        {
            PlayerPrefs.SetInt("HighCoin", Coin);
        }
    }
}

//private void Awake(): ฟังก์ชัน Awake ทำงานทันทีที่ตอนที่เกมเริ่มทำงาน (Start) และใช้ในการกำหนดตัวแปร instance เป็นตัวที่ใช้ในการเข้าถึงคลาสนี้.
//void Start(): ฟังก์ชัน Start ทำงานทันทีหลังจาก Awake และใช้ในการตั้งค่าเริ่มต้นของเกม เช่น กำหนดค่า Time.timeScale เป็น 1 (เพื่อให้เวลาทำงานปกติ), กำหนด gameOver เป็น false (เกมยังไม่จบ), และ isGameStarted เป็น false (เกมยังไม่เริ่ม).
//นอกจากนี้ยังทำการโหลดค่า HighCoin จาก PlayerPrefs ซึ่งเป็นค่าเหรียญสูงสุดที่เคยได้ถึงมาเก็บไว้.
//และทำการอัปเดตข้อความแสดงผลเหรียญ (CoinText) และเหรียญสูงสุด(HighCoinText).
//void Update(): ฟังก์ชัน Update ทำงานทุกๆ เฟรมของเกม.
//ถ้า gameOver เป็น true, ให้แสดง panel ที่บอกว่าเกมจบและหยุดเวลา (Time.timeScale = 0).
//ถ้าผู้เล่นทำการ tap(ในที่นี้ใช้ SwipeManager.tap), จะทำการเริ่มเกม(isGameStarted = true) และทำลายข้อความ "Tap to Start" ที่ปรากฏตอนเริ่มเกม.
//public void AddCoin(): ฟังก์ชันนี้ถูกเรียกเมื่อผู้เล่นได้ทำการเก็บเหรียญ.
//เพิ่มจำนวนเหรียญ(Coin += 1).
//อัปเดตข้อความแสดงผลเหรียญ(CoinText).
//ถ้าจำนวนเหรียญที่ได้มากกว่า HighCoin, จะทำการอัปเดตค่า HighCoin และบันทึกลง PlayerPrefs.