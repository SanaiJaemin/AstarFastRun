using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [SerializeField]
    public class Node : MonoBehaviour
    {
        public Node(bool _iswall, int _x, int _y)
        {
            isWall = _iswall; // 벽확인

        }

        public bool isWall;
        public int X, Y, G, H; // G: 시작으로 이동거리
                               // H: 장애물 무시하고 목표거리
                               // F: G + H 거리 이게 가장 작은게 목표에 근접

    public Node ParetNode;

        public int F { get { return G + H; } }


    }

