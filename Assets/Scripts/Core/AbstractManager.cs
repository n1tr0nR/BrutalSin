using System.Linq;
using UnityEngine;

namespace Core 
{
    public class AbstractManager<T> : MonoBehaviour where T: MonoBehaviour
    {
        private const bool KeepAlive = true;
        private static T _instance = null;
        
        public static bool IsInstanceAlive => _instance != null;
        
        public static T Instance {
            get {
                if (_instance != null) return _instance;
                
                _instance = FindObjectsByType<T>(FindObjectsSortMode.None).First();
                
                if (_instance != null) return _instance;
                var singletonObj = new GameObject
                {
                    name = typeof(T).ToString()
                };
                _instance = singletonObj.AddComponent<T>();
                return _instance; 
            }
        }
        
        public virtual void Awake(){
            if (_instance != null){
                Destroy(gameObject);
                return;
            }
            _instance = GetComponent<T>();
            
            if(KeepAlive){
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
