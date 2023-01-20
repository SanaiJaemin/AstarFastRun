using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int TileSize = 20;

    RaycastHit2D hit;

    //Node[,] NodeMap = new Node[TileSize, TileSize]; // 노드 공간


    [SerializeField]
    private GameObject Node, PlayerPrefab;
   

    Vector2 Pos;

    Vector2 hitpos;


    Node StartNode, CurNode, EndNode; // 시작 , 현재 , 끝나는곳

    List<Node> OpenList, CloseList; // 오픈리스트 : 검색 가능성이 있는 노드 , 이미 검색이 끝난 지점들의 집합

    private void OnEnable()
    {
        for (int i = 0;  i < TileSize; i++ )
        {
            for(int j = 0; j < TileSize; j++)
            {
                int Lotto = 0;
                GameObject[,] NodeMap = new GameObject[TileSize, TileSize];
                NodeMap[i, j] = Instantiate(Node, new Vector3(i, j, 0), Quaternion.identity);
                NodeMap[i, j].GetComponent<Node>().X = i;
                NodeMap[i, j].GetComponent<Node>().Y = j;
                if (Random.Range(0, 6) == Lotto) //벽 생성
                {
                    NodeMap[i, j].GetComponent<SpriteRenderer>().material.color = Color.black;
                    NodeMap[i, j].GetComponent<Node>().isWall = true;
                }


            }
        }
    }

    private void playerSpawn()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            hitpos = hit.transform.position;
            GameObject Player = Instantiate(PlayerPrefab, hitpos, Quaternion.identity);
            StartNode.X = hit.transform.GetComponent<Node>().X; // 스타트노드 클릭한 친구가 스타드 노트로됨
            StartNode.X = hit.transform.GetComponent<Node>().Y;


        }
    }

    private void EndNodeChange()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            hit.transform.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            


        }
    }

    private void Update()
    {
        playerSpawn();
        EndNodeChange();
    }
}
