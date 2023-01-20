using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int TileSize = 20;

    RaycastHit2D hit;

    //Node[,] NodeMap = new Node[TileSize, TileSize]; // 노드 공간
    static int wallcount = 0;


    [SerializeField]
    private GameObject Node, PlayerPrefab; //생성

    GameObject[,] NodeMap;



    Vector2 hitpos, FirstNodePos,EndNodePos; // 생성지점에서 
    [SerializeField]
    GameObject Relase; 

    GameObject StartNode, CurNode, EndNode; // 시작 , 현재 , 끝나는곳

    List<GameObject> OpenList, CloseList; // 오픈리스트 : 검색 가능성이 있는 노드 , 이미 검색이 끝난 지점들의 집합

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        List<GameObject> OpenList = new List<GameObject>();
        List<GameObject> CloseList = new List<GameObject>();

        for (int i = 0; i < TileSize; i++)
        {
            for (int j = 0; j < TileSize; j++)
            {
                int Lotto = 0;
                NodeMap[i, j] = Instantiate(Node, new Vector3(i, j, 0), Quaternion.identity);
                NodeMap[i, j].GetComponent<Node>().X = i;
                NodeMap[i, j].GetComponent<Node>().Y = j;
                if (Random.Range(0, 6) == Lotto) //벽 생성
                {
                    NodeMap[i, j].GetComponent<SpriteRenderer>().material.color = Color.black;
                    NodeMap[i, j].GetComponent<Node>().isWall = true;
                    wallcount++;


                }


            }
        }
        Debug.Log($"{wallcount}");
    }

    private void playerSpawn() // 플레이어
    {
        if (Input.GetMouseButtonDown(1))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            hitpos = hit.transform.position;
            GameObject Player = Instantiate(PlayerPrefab, hitpos, Quaternion.identity);
            StartNode = hit.transform.gameObject; // 스타트노드 클릭한 친구가 스타드 노트로됨 

        }
    }

    private void EndNodeChange() // 도착지점
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            hit.transform.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            EndNode = hit.transform.gameObject;


        }
    }

    private void Update()
    {
        playerSpawn(); //스타드노드 
        EndNodeChange(); // 마지막노드
    }

    

    private void OpenListAdd(int Horizontal,int vertical)
    {
        if (Horizontal >= 0 && Horizontal < TileSize && vertical >= 0 && vertical < TileSize && 
            !NodeMap[Horizontal, vertical].GetComponent<Node>().isWall && !CloseList.Contains(NodeMap[Horizontal, vertical]))
        {
            GameObject Friend = NodeMap[Horizontal, vertical];   // 주변노드 이웃으로 넣어주기
            int MoveSize = CurNode.GetComponent<Node>().G + (CurNode.GetComponent<Node>().X - Horizontal == 0 || CurNode.GetComponent<Node>().Y - vertical == 0 ? 10 : 14);
            // G시작거리 , 현재 노드에서 검색한 노드의 위치값 빼서 0이면 이동량 10 대각선은 14 현재 노드
            // 현재 노드 (0,0) 검색한 노드가  (1,0)  - 1 X축은 false이지만 Y축은 true이므로 이동량은 10이다  직선이동 검색한 노드에  G :0  H :10  F :10 
            // 현재 노드 (0,0) 검색한 노드가  (1,1)  이면 둘다 -1 -1 0이 아니므로 이동량은 14 대각선  G :0  H :14  F :14 
            // 

        }
    }



}

