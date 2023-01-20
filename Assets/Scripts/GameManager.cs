using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int TileSize = 20;

    RaycastHit2D hit;

    //Node[,] NodeMap = new Node[TileSize, TileSize]; // ��� ����
    static int wallcount = 0;


    [SerializeField]
    private GameObject Node, PlayerPrefab; //����

    GameObject[,] NodeMap;



    Vector2 hitpos, FirstNodePos,EndNodePos; // ������������ 
    [SerializeField]
    GameObject Relase; 

    GameObject StartNode, CurNode, EndNode; // ���� , ���� , �����°�

    List<GameObject> OpenList, CloseList; // ���¸���Ʈ : �˻� ���ɼ��� �ִ� ��� , �̹� �˻��� ���� �������� ����

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
                if (Random.Range(0, 6) == Lotto) //�� ����
                {
                    NodeMap[i, j].GetComponent<SpriteRenderer>().material.color = Color.black;
                    NodeMap[i, j].GetComponent<Node>().isWall = true;
                    wallcount++;


                }


            }
        }
        Debug.Log($"{wallcount}");
    }

    private void playerSpawn() // �÷��̾�
    {
        if (Input.GetMouseButtonDown(1))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            hitpos = hit.transform.position;
            GameObject Player = Instantiate(PlayerPrefab, hitpos, Quaternion.identity);
            StartNode = hit.transform.gameObject; // ��ŸƮ��� Ŭ���� ģ���� ��Ÿ�� ��Ʈ�ε� 

        }
    }

    private void EndNodeChange() // ��������
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
        playerSpawn(); //��Ÿ���� 
        EndNodeChange(); // ���������
    }

    

    private void OpenListAdd(int Horizontal,int vertical)
    {
        if (Horizontal >= 0 && Horizontal < TileSize && vertical >= 0 && vertical < TileSize && 
            !NodeMap[Horizontal, vertical].GetComponent<Node>().isWall && !CloseList.Contains(NodeMap[Horizontal, vertical]))
        {
            GameObject Friend = NodeMap[Horizontal, vertical];   // �ֺ���� �̿����� �־��ֱ�
            int MoveSize = CurNode.GetComponent<Node>().G + (CurNode.GetComponent<Node>().X - Horizontal == 0 || CurNode.GetComponent<Node>().Y - vertical == 0 ? 10 : 14);
            // G���۰Ÿ� , ���� ��忡�� �˻��� ����� ��ġ�� ���� 0�̸� �̵��� 10 �밢���� 14 ���� ���
            // ���� ��� (0,0) �˻��� ��尡  (1,0)  - 1 X���� false������ Y���� true�̹Ƿ� �̵����� 10�̴�  �����̵� �˻��� ��忡  G :0  H :10  F :10 
            // ���� ��� (0,0) �˻��� ��尡  (1,1)  �̸� �Ѵ� -1 -1 0�� �ƴϹǷ� �̵����� 14 �밢��  G :0  H :14  F :14 
            // 

        }
    }



}

