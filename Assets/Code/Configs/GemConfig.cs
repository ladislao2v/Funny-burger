using Code.Services.LevelRewardService;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create GemConfig", fileName = "GemConfig", order = 0)]
    public sealed class GemConfig : Item
    {
        [field: SerializeField] public int Count { get; private set; }

        public override void Accept(IItemVisitor itemVisitor) => 
            itemVisitor.Visit(this);
    }
}