using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create ChefConfig", fileName = "ChefConfig", order = 0)]
    public class ChefConfig : ScriptableObject
    {
        public float Speed;
        public float TaskTime = 5;
    }
}