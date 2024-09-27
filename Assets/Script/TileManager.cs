using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 3;
    public int totalNumOfTiles = 8;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if (playerTransform == null) return;
        if (playerTransform.position.z - 35 >= zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }


    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}






//public GameObject[] tilePrefabs;: ��������ͧ����ͺਡ����ж١����Ἱ���(Tile) �������ö���͡��ҡ Unity Editor.
//public float zSpawn = 0;: ���˹� Z �������Ѻ��� spawn Ἱ��� (Tile) �á������鹷�� 0.
//public float tileLength = 30;: ������ǢͧἹ���(Tile) ��� Z.
//public int numberOfTiles = 3;: �ӹǹἹ���(Tiles) ���ж١���ҧ���仾����� �ѹ�������������.
//public int totalNumOfTiles = 8;: �ӹǹ�������ͧἹ���(Tiles) �������ö���ҧ��.
//private List<GameObject> activeTiles = new List<GameObject>();: ��ʵ�ͧ����ͺਡ������Ἱ���(Tile) ���١���ҧ�������ѧ���������.
//public Transform playerTransform;: �����ҧ�ԧ��ѧ Transform �ͧ������ (Player).
//void Start(): ������������, �зӡ�� spawn Ἱ��� (Tile) ����ӹǹ����˹� �����Ἱ����á�١ spawn �������� ���Ἱ������� �ж١ spawn �¡�������ҡ�������� tilePrefabs.
//void Update(): �ѧ��ѹ���ж١���¡㹷ء� ����ͧ��. �ѹ�е�Ǩ�ͺ��Ҷ�ҵ��˹觢ͧ�������դ�� Z �Թ���� zSpawn - (numberOfTiles * tileLength) +35 ����, �ʴ���Ҽ�������Ţ���ҡ�Թ� �֧�зӡ�� spawn Ἱ������� ��зӡ��źἹ�����ҷ��������.
//public void SpawnTile(int tileIndex): �ѧ��ѹ�����㹡�� spawn Ἱ��� (Tile) ���Ѻ���������� tileIndex �����к���ҵ�ͧ spawn Ἱ��誹Դ�. ��Шзӡ�����ҧἹ�������, ����ŧ���ʵ� activeTiles, �������͹ zSpawn 价ҧ��ҧ˹��.
//private void DeleteTile(): �ѧ��ѹ�����㹡��źἹ�����ҷ���ʴ����˹�Ҩ��͡� �·ӡ�÷��������ͺਡ�����ź�͡�ҡ��ʵ� activeTiles.