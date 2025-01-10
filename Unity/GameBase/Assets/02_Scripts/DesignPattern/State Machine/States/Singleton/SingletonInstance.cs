namespace DesignPattern.Singleton
{
    /// <summary>
    /// 제네릭 기반의 싱글톤 패턴 구현을 위한 추상 클래스
    /// </summary>
    /// <typeparam name="T">싱글턴으로 사용할 클래스 타입</typeparam>
    public abstract class SingletonInstance<T> where T : class, ISingleton
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new System.Exception($"{typeof(T)} 싱글턴 인스턴스가 아직 생성되지 않았습니다.");
                }
                return _instance;
            }
        }


        /// <summary>
        /// 생성자 : 싱글톤 객체의 중복 생성을 방지, 인스턴스 초기화
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        protected SingletonInstance()
        {
            if (_instance != null)
            {
                throw new System.Exception($"{typeof(T)} 싱글톤 객체는 이미 생성되었습니다.");
            }
            _instance = this as T;
        }

        /// <summary>
        /// 싱글톤 인스턴스 제거
        /// </summary>
        protected void DetroyInstance()
        {
            _instance = null;
        }


    }
}