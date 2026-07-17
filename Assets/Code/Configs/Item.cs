using Code.Services.LevelRewardService;
using UnityEngine;

namespace Code.Configs
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Logo { get; private set; }

        public abstract void Accept(IItemVisitor itemVisitor);
    }
}