using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern.Reference
{
    /// <summary>
    /// 타입 참조 풀
    /// </summary>
    public static class Reference
    {
        private static readonly Dictionary<Type, ReferenceCollector> _collectors = new Dictionary<Type, ReferenceCollector>();


        /// <summary>
        /// 객체 풀 초기 용량
        /// </summary>
        public static int InitCapacity { get; set; } = 100;


        /// <summary>
        /// 객체 풀의 수
        /// </summary>
        public static int Count
        {
            get
            {
                return _collectors.Count;
            }
        }

        /// <summary>
        /// 모든 객체 풀을 비움
        /// <summary>
        public static void ClearAll()
        {
            _collectors.Clear();
        }

        public static IReference Spawn(Type type)
        {
            if (_collectors.ContainsKey(type) == false)
            {
                _collectors.Add(type, new ReferenceCollector(type, InitCapacity));
            }

            return _collectors[type].Spawn();
        }

        /// <summary>
        /// 참조 객체를 요청
        /// </summary>
        public static T Spawn<T>() where T : class, IReference, new()
        {
            Type type = typeof(T);
            return Spawn(type) as T;
        }


        /// <summary>
        /// 참조 객체를 반환
        /// </summary>
        public static void Release(IReference item)
        {
            Type type = item.GetType();

            if (_collectors.ContainsKey(type) == false)
            {
                _collectors.Add(type, new ReferenceCollector(type, InitCapacity));
            }

            _collectors[type].Release(item);
        }


        /// <summary>
        /// 리스트 형태의 객체들을 일괄 반환
        /// </summary>
        public static void Release<T>(List<T> items) where T : class, IReference, new()
        {
            Type type = typeof(T);

            if (_collectors.ContainsKey(type) == false)
            {
                _collectors.Add(type, new ReferenceCollector(type, InitCapacity));
            }

            for (int i = 0; i < items.Count; i++)
            {
                _collectors[type].Release(items[i]);
            }

        }

        /// <summary>
        /// 배열 형태의 객체들을 일괄 반환
        /// </summary>
        public static void Release<T>(T[] items) where T : class, IReference, new()
        {
            Type type = typeof(T);

            if (_collectors.ContainsKey(type) == false)
            {
                _collectors.Add(type, new ReferenceCollector(type, InitCapacity));
            }

            for (int i = 0; i < items.Length; i++)
            {
                _collectors[type].Release(items[i]);
            }
        }

        #region 디버깅 전용 메서드
        internal static Dictionary<Type, ReferenceCollector> GetAllCollectors
        {
            get { return _collectors; }
        }
        #endregion
    }
}

