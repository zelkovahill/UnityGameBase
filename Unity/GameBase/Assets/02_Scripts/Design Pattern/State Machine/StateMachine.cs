using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace DesignPattern.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<string, System.Object> _blackboard = new Dictionary<string, object>(100);   // 상태 머신의 데이터 저장소
        private readonly Dictionary<string, IStateNode> _nodes = new Dictionary<string, IStateNode>(100);       // 등록된 상태 노드들
        private IStateNode _curNode;    // 현재 상태
        private IStateNode _preNode;    // 이전 상태


        /// <summary>
        /// 상태 머신을 소유하는 객체
        /// </summary>
        public System.Object Owner { private set; get; }


        /// <summary>
        /// 현재 실행 중인 상태 노드
        /// </summary>
        public string CurrentNode => _curNode != null ? _curNode.GetType().FullName : string.Empty;


        /// <summary>
        /// 이전 상태 노드
        /// </summary>
        public string PreviousNode => _preNode != null ? _preNode.GetType().FullName : string.Empty;


        private StateMachine() { }

        public StateMachine(System.Object owner)
        {
            Owner = owner;
        }

        public void Update()
        {
            if (_curNode != null)
            {
                _curNode.OnUpdate();    // 현재 상태에서 업데이트 처리
            }
        }

        /// <summary>
        /// 상태 머신 시작
        /// </summary>
        public void Run<TNode>() where TNode : IStateNode
        {
            var nodeType = typeof(TNode);
            var nodeName = nodeType.FullName;
            Run(nodeName);
        }
        public void Run(Type entryNode)
        {
            var nodeName = entryNode.FullName;
            Run(nodeName);
        }
        public void Run(string entryNode)
        {
            _curNode = TryGetNode(entryNode);
            _preNode = _curNode;

            if (_curNode == null)
                throw new Exception($"Not found entry node: {entryNode}");

            _curNode.OnEnter();
        }


        /// <summary>
        /// 노드 추가 및 상태 변경
        /// </summary>
        public void AddNode<TNode>() where TNode : IStateNode, new()
        {
            TNode stateNode = new TNode();
            AddNode(stateNode);
        }
        public void AddNode(IStateNode stateNode)
        {
            if (stateNode == null)
                throw new ArgumentNullException();

            var nodeType = stateNode.GetType();
            var nodeName = nodeType.FullName;

            if (_nodes.ContainsKey(nodeName) == false)
            {
                stateNode.OnCreate(this);           // 상태 노드가 생성될 때 호출F
                _nodes.Add(nodeName, stateNode);    // 상태 노드 추가F
            }
            else
            {
                throw new Exception($"이미 등록된 노드 : {nodeName}");
            }
        }


        /// <summary>
        /// 상태 전환
        /// </summary>
        public void ChangeState<TNode>() where TNode : IStateNode
        {
            var nodeType = typeof(TNode);
            var nodeName = nodeType.FullName;
            ChangeState(nodeName);
        }
        public void ChangeState(Type nodeType)
        {
            var nodeName = nodeType.FullName;
            ChangeState(nodeName);
        }
        public void ChangeState(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
                throw new ArgumentNullException();

            IStateNode node = TryGetNode(nodeName);
            if (node == null)
            {
                throw new Exception($"노드를 찾을 수 없음 : {nodeName}");
            }

            Debug.Log($"상태 전환: {_curNode.GetType().FullName} -> {node.GetType().FullName}");
            _preNode = _curNode;
            _curNode.OnExit();
            _curNode = node;
            _curNode.OnEnter();
        }


        public void SetBlackboardValue(string key, System.Object value)
        {
            if (_blackboard.ContainsKey(key) == false)
            {
                _blackboard.Add(key, value);
            }
            else
            {
                _blackboard[key] = value;
            }

        }

        public System.Object GetBlackboardValue(string key)
        {
            if (_blackboard.TryGetValue(key, out System.Object value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"블랙보드에 없는 키 : {key}");
                return null;
            }
        }


        /// <summary>
        /// 노드 조회
        /// </summary>
        private IStateNode TryGetNode(string nodeName)
        {
            _nodes.TryGetValue(nodeName, out IStateNode result);
            return result;
        }
    }
}
