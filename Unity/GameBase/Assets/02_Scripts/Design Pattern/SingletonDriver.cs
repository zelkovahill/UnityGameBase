using UnityEngine;

namespace DesignPattern.Singleton
{
    internal class SingletonDriver : MonoBehaviour
    {
        private void Update()
        {
            Singleton.Update();
        }
    }
}