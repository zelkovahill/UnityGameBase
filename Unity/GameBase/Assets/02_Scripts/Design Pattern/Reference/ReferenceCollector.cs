using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern.Reference
{
    public class ReferenceCollector
    {
        private readonly Stack<IReference> _collector;

        /// <summary>
        /// 참조 유형
        /// </summary>
        public Type ClassType { private set; get; }


        /// <summary>
        /// 내부 캐시 총 개수
        /// </summary>
        public int Count
        {
            get => _collector.Count;
        }


        /// <summary>
        /// 총 외부 사용량
        /// </summary>
        public int SpawnCount { private set; get; }


        public ReferenceCollector(Type type, int capacity)
        {
            ClassType = type;

            // 캐시 풀 생성
            _collector = new Stack<IReference>(capacity);

            // 인터페이스가 상속되었는지 확인
            Type temp = type.GetInterface(nameof(IReference));

            if (temp == null)
            {
                throw new Exception($"{type.Name}은 IReference를 상속받지 않았습니다.");
            }
        }


        /// <summary>
        /// 참조 객체를 요청
        /// </summary>
        /// <returns></returns>
        public IReference Spawn()
        {
            IReference item;

            if (_collector.Count > 0)
            {
                item = _collector.Pop();
            }
            else
            {
                item = Activator.CreateInstance(ClassType) as IReference;
            }

            SpawnCount++;
            item.OnSpawn();
            return item;
        }


        /// <summary>
        /// 참조 객체 반환
        /// </summary>
        public void Release(IReference item)
        {
            if (item == null)
            {
                return;
            }

            if (item.GetType() != ClassType)
            {
                throw new Exception($"잘못된 타입 반환 요청 : {item.GetType().Name}");
            }

            if (_collector.Contains(item))
            {
                throw new Exception($"이미 반환된 객체 : {item.GetType().Name}");
            }

            SpawnCount--;
            _collector.Push(item);
        }


        /// <summary>
        /// 컬렉션을 비웁니다.
        /// </summary>
        public void Clear()
        {
            _collector.Clear();
            SpawnCount = 0;
        }

    }
}
