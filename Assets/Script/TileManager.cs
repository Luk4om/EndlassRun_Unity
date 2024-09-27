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






//public GameObject[] tilePrefabs;: อาร์เรย์ของเกมอ็อบเจกต์ที่จะถูกใช้เป็นแผนที่(Tile) ที่สามารถเลือกได้จาก Unity Editor.
//public float zSpawn = 0;: ตำแหน่ง Z ที่สำหรับการ spawn แผนที่ (Tile) แรกเริ่มต้นที่ 0.
//public float tileLength = 30;: ความยาวของแผนที่(Tile) ในแนว Z.
//public int numberOfTiles = 3;: จำนวนแผนที่(Tiles) ที่จะถูกสร้างขึ้นไปพร้อมๆ กันในเมื่อเริ่มเกม.
//public int totalNumOfTiles = 8;: จำนวนทั้งหมดของแผนที่(Tiles) ที่สามารถสร้างได้.
//private List<GameObject> activeTiles = new List<GameObject>();: ลิสต์ของเกมอ็อบเจกต์ที่เป็นแผนที่(Tile) ที่ถูกสร้างขึ้นและยังคงอยู่ในเกม.
//public Transform playerTransform;: การอ้างอิงไปยัง Transform ของผู้เล่น (Player).
//void Start(): เมื่อเริ่มเกม, จะทำการ spawn แผนที่ (Tile) ตามจำนวนที่กำหนด โดยให้แผนที่แรกถูก spawn ด้วยเสมอ และแผนที่อื่นๆ จะถูก spawn โดยการสุ่มจากอาร์เรย์ tilePrefabs.
//void Update(): ฟังก์ชันนี้จะถูกเรียกในทุกๆ เฟรมของเกม. มันจะตรวจสอบว่าถ้าตำแหน่งของผู้เล่นมีค่า Z เกินกว่า zSpawn - (numberOfTiles * tileLength) +35 แล้ว, แสดงว่าผู้เล่นไปไกลขึ้นมากเกินไป จึงจะทำการ spawn แผนที่ใหม่ และทำการลบแผนที่เก่าที่มีอยู่.
//public void SpawnTile(int tileIndex): ฟังก์ชันนี้ใช้ในการ spawn แผนที่ (Tile) โดยรับพารามิเตอร์ tileIndex เพื่อระบุว่าต้อง spawn แผนที่ชนิดใด. และจะทำการสร้างแผนที่ใหม่, เพิ่มลงในลิสต์ activeTiles, และเลื่อน zSpawn ไปทางข้างหน้า.
//private void DeleteTile(): ฟังก์ชันนี้ใช้ในการลบแผนที่เก่าที่แสดงที่หน้าจอออกไป โดยทำการทำลายเกมอ็อบเจกต์และลบออกจากลิสต์ activeTiles.