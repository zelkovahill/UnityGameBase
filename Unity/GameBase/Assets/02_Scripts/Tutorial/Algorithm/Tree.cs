using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // 노드의 정의
    public class TreeNode
    {
        public int Data;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    // 이진 트리의 정의
    public class BinaryTree
    {
        // 루트 노드
        public TreeNode Root;

        public BinaryTree()
        {
            Root = null;
        }

        // 새로운 데이터의 삽입
        public void Insert(int data)
        {
            Root = InsertRec(Root, data);
        }


        // 이 트리의 경우 데이터가 부모 노드보다 크거나 같으면 오른쪽
        // 작으면 왼쪽에 넣는 구조
        private TreeNode InsertRec(TreeNode root, int data)
        {
            if (root == null)
            {
                root = new TreeNode(data);
                return root;
            }

            if (data < root.Data)
            {
                root.Left = InsertRec(root.Left, data);

            }
            else if (data >= root.Data)
            {
                root.Right = InsertRec(root.Right, data);
            }

            return root;
        }

        // Inorder 순회 및 출력
        public void InOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Debug.Log(node.Data.ToString());
                InOrderTraversal(node.Right);
            }
        }

        // Preorder 순회 및 출력
        public void PreOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                Debug.Log(node.Data.ToString());
                PreOrderTraversal(node.Left);
                PreOrderTraversal(node.Right);
            }
        }


        // Postorder 순회 및 출력
        public void PostOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left);
                PostOrderTraversal(node.Right);
                Debug.Log(node.Data.ToString());
            }
        }
    }
}
