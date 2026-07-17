using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create FeatureConfig", fileName = "FeatureConfig", order = 0)]
    public class FeatureConfig : Item
    {
        public override void Accept(IItemVisitor itemVisitor)
        {
            itemVisitor.Visit(this);
        }
    }
}