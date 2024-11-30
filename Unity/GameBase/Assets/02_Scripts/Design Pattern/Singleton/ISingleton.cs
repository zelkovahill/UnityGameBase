namespace DesignPattern.Singleton
{
    public interface ISingleton
    {
        /// <summary>
        /// 싱글톤 객체 생성 시 호출
        /// </summary>
        /// <param name="createParam">객체 생성 시 필요한 초기화 데이터를 전달 받음</param>
        void OnCreate(System.Object createParam);

        /// <summary>
        /// 싱글턴 객체를 업데이트할 때 호출
        /// </summary>
        void OnUpdate();


        /// <summary>
        /// 싱글톤 객체가 솔멸될 때 호출
        /// </summary>
        void OnDestroy();
    }
}