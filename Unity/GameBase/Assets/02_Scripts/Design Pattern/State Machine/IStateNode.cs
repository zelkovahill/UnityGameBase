namespace DesignPattern.StateMachine
{
    public interface IStateNode
    {
        void OnCreate(StateMachine machine);    // 상태 노드 생성 시 호출
        void OnEnter();                         // 상태 진입 시 호출
        void OnUpdate();                        // 상태 업데이트 시 호출
        void OnExit();                          // 상태 종료 시 호출
    }
}