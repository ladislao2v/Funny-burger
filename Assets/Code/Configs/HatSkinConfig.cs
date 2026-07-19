using Code.Skins;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create HatSkinConfig", fileName = "HatSkinConfig", order = 0)]
    public class HatSkinConfig : SkinConfig
    {
        [field: SerializeField] public HatSkinType HatSkinId { get; private set; }
        public override void Accept(IItemVisitor itemVisitor)
        {
            itemVisitor.Visit(this);
        }
    }
}