using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [SerializeField]
    public class Node : MonoBehaviour
    {
        public Node(bool _iswall, int _x, int _y)
        {
            isWall = _iswall; // ��Ȯ��

        }

        public bool isWall;
        public int X, Y, G, H; // G: �������� �̵��Ÿ�
                               // H: ��ֹ� �����ϰ� ��ǥ�Ÿ�
                               // F: G + H �Ÿ� �̰� ���� ������ ��ǥ�� ����

    public Node ParetNode;

        public int F { get { return G + H; } }


    }

