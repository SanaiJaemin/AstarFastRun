using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int TileSize = 20;

    RaycastHit2D hit;

    //Node[,] NodeMap = new Node[TileSize, TileSize]; // ��� ����


    [SerializeField]
    private GameObject Node, PlayerPrefab;
   

    Vector2 Pos;

    Vector2 hitpos;


    Node StartNode, CurNode, EndNode; // ���� , ���� , �����°�

    List<Node> OpenList, CloseList; // ���¸���Ʈ : �˻� ���ɼ��� �ִ� ��� , �̹� �˻��� ���� �������� ����

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
                if (Random.Range(0, 6) == Lotto) //�� ����
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
            StartNode.X = hit.transform.GetComponent<Node>().X; // ��ŸƮ��� Ŭ���� ģ���� ��Ÿ�� ��Ʈ�ε�
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
