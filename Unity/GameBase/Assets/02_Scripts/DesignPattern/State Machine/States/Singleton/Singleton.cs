using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Singleton
{
    public static class Singleton
    {
        /// <summary>
        /// 싱글톤 객체와 우선순위를 함께 관리
        /// </summary>
        private class Wrapper
        {
            public int Priority { private set; get; }   // 살행 우선순위
            public ISingleton Singleton { private set; get; }   // 싱글톤 객체

            public Wrapper(ISingleton module, int priority)
            {
                Singleton = module;
                Priority = priority;
            }
        }


        // 싱글 톤 시스템 상태 관리 변수
        private static bool _isInitialized = false; // 초기화 여부
        private static GameObject _driver = null;   // 싱글톤 시스템의 GameObject
        private static readonly List<Wrapper> _wrappers = new List<Wrapper>(100);   // 싱글톤 리스트
        private static MonoBehaviour _behaviour = null; // MonoBehaviour 드라이버
        private static bool _isDirty = false;   // 우선순위 재정렬 필요 여부


        /// <summary>
        /// 싱글톤 시스템 초기화
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized)
            {
                throw new System.Exception($"{nameof(Singleton)} 시스템은 이미 초기화 되었습니다.");
            }

            if (_isInitialized == false)
            {
                // GameObject 생성 및 초기화
                _isInitialized = true;
                _driver = new UnityEngine.GameObject($"[{nameof(Singleton)}]");
                _behaviour = _driver.AddComponent<SingletonDriver>(); // 드라이버 추가
                UnityEngine.Object.DontDestroyOnLoad(_driver); // 씬 전환 시 파괴 방지
                Debug.Log($"{nameof(Singleton)} 시스템 초기화 완료");
            }
        }

        /// <summary>
        /// 싱글톤 시스템 종료 및 모든 객체 삭제
        /// </summary>
        public static void Destroy()
        {
            if (_isInitialized)
            {
                DestroyAll();   // 모든 싱글톤 객체 제거

                _isInitialized = false;

                if (_driver != null)
                {
                    GameObject.Destroy(_driver);    // GameObject 파괴
                }

                Debug.Log($"{nameof(Singleton)} 시스템 종료");
            }
        }

        /// <summary>
        /// 싱글톤 업데이트 (우선순위 정렬 및 Update 호출)
        /// </summary>
        internal static void Update()
        {
            if (_isDirty)   // 우선순위 재정렬
            {
                _isDirty = false;
                _wrappers.Sort((left, right) =>
                {
                    if (left.Priority > right.Priority)
                    {
                        return -1;
                    }
                    else if (left.Priority == right.Priority)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                });
            }

            // 모든 싱글톤 모듈의 Update 호출
            for (int i = 0; i < _wrappers.Count; i++)
            {
                _wrappers[i].Singleton.OnUpdate();
            }
        }

        /// <summary>
        /// 특정 싱글톤 객체 가져오기
        /// </summary>
        public static T GetSingleton<T>() where T : class, ISingleton
        {
            System.Type type = typeof(T);

            for (int i = 0; i < _wrappers.Count; i++)
            {
                if (_wrappers[i].Singleton.GetType() == type)
                {
                    return _wrappers[i].Singleton as T;
                }
            }

            Debug.LogError($"{type} 싱글톤 객체가 존재하지 않습니다.");
            return null;
        }


        /// <summary>
        /// 싱그톤 존재 여부 확인
        /// </summary>
        public static bool Contains<T>() where T : class, ISingleton
        {
            System.Type type = typeof(T);

            for (int i = 0; i < _wrappers.Count; i++)
            {
                if (_wrappers[i].Singleton.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// 싱글톤 객체 생성
        /// </summary>
        public static T CreateSingleton<T>(int priority = 0) where T : class, ISingleton
        {
            return CreateSingleton<T>(null, priority);
        }

        public static T CreateSingleton<T>(System.Object createParam, int priority = 0) where T : class, ISingleton
        {
            if (priority < 0)
            {
                throw new System.Exception("우선순위는 0 이상이어야 합니다.");
            }

            if (Contains<T>())
            {
                throw new System.Exception($"{typeof(T)} 싱글톤 객체는 이미 생성되었습니다.");
            }

            // 우선순위 기본값 설정
            if (priority == 0)
            {
                int minPriority = GetMinPriority();
                priority = --minPriority;
            }

            T module = Activator.CreateInstance<T>();   // 싱글톤 객체 생성
            Wrapper wrapper = new Wrapper(module, priority);    // Wrapper 생성
            wrapper.Singleton.OnCreate(createParam);    // 초기화
            _wrappers.Add(wrapper);
            _isDirty = true;    // 정렬 플래그 설정
            return module;
        }


        /// <summary>
        /// 특정 싱글톤 객체 삭제
        /// </summary>
        public static bool DestroySingleton<T>() where T : class, ISingleton
        {
            var type = typeof(T);
            for (int i = 0; i < _wrappers.Count; i++)
            {
                if (_wrappers[i].Singleton.GetType() == type)
                {
                    _wrappers[i].Singleton.OnDestroy(); // 종료 처리
                    _wrappers.RemoveAt(i);  // 리스트에서 제거F
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 코루틴 실행
        /// </summary>
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            return _behaviour.StartCoroutine(routine);
        }
        public static Coroutine StartCoroutine(string methodName)
        {
            return _behaviour.StartCoroutine(methodName);
        }


        /// <summary>
        /// 코루틴 중지
        /// </summary>
        public static void StopCoroutine(IEnumerator coroutine)
        {
            _behaviour.StopCoroutine(coroutine);
        }
        public static void StopCoroutine(Coroutine coroutine)
        {
            _behaviour.StopCoroutine(coroutine);
        }


        /// <summary>
        /// 모든 코루틴 중지
        /// </summary>
        public static void StopAllCoroutines()
        {
            _behaviour.StopAllCoroutines();
        }


        /// <summary>
        /// 우선순위가 가장 낮은 값 반환
        /// </summary>
        /// <returns></returns>
        private static int GetMinPriority()
        {
            int minPriority = 0;

            for (int i = 0; i < _wrappers.Count; i++)
            {
                if (_wrappers[i].Priority < minPriority)
                {
                    minPriority = _wrappers[i].Priority;
                }
            }
            return minPriority; // 최소 우선순위 반환
        }


        /// <summary>
        /// 모든 싱글톤 객체 제거
        /// </summary>
        private static void DestroyAll()
        {
            for (int i = 0; i < _wrappers.Count; i++)
            {
                _wrappers[i].Singleton.OnDestroy();
            }

            _wrappers.Clear();
        }

    }
}
