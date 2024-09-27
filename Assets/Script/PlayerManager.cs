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

//private void Awake(): �ѧ��ѹ Awake �ӧҹ�ѹ�շ��͹�����������ӧҹ (Start) �����㹡�á�˹������ instance �繵�Ƿ����㹡����Ҷ֧���ʹ��.
//void Start(): �ѧ��ѹ Start �ӧҹ�ѹ����ѧ�ҡ Awake �����㹡�õ�駤��������鹢ͧ�� �� ��˹���� Time.timeScale �� 1 (����������ҷӧҹ����), ��˹� gameOver �� false (���ѧ��診), ��� isGameStarted �� false (���ѧ��������).
//�͡�ҡ����ѧ�ӡ����Ŵ��� HighCoin �ҡ PlayerPrefs ����繤������­�٧�ش�������֧�������.
//��зӡ���ѻവ��ͤ����ʴ�������­ (CoinText) �������­�٧�ش(HighCoinText).
//void Update(): �ѧ��ѹ Update �ӧҹ�ء� ����ͧ��.
//��� gameOver �� true, ����ʴ� panel ���͡������������ش���� (Time.timeScale = 0).
//��Ҽ����蹷ӡ�� tap(㹷������ SwipeManager.tap), �зӡ���������(isGameStarted = true) ��з���¢�ͤ��� "Tap to Start" ����ҡ��͹�������.
//public void AddCoin(): �ѧ��ѹ���١���¡����ͼ�������ӡ��������­.
//�����ӹǹ����­(Coin += 1).
//�ѻവ��ͤ����ʴ�������­(CoinText).
//��Ҩӹǹ����­������ҡ���� HighCoin, �зӡ���ѻവ��� HighCoin ��кѹ�֡ŧ PlayerPrefs.