using Code.Skins;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create BodySkinConfig", fileName = "BodySkinConfig", order = 0)]
    public class BodySkinConfig : SkinConfig
    {
        [field: SerializeField] public BodySkinType BodySkinId { get; private set; }
        [field: SerializeField] public Material SkinMaterial { get; private set; }
        public override void Accept(IItemVisitor itemVisitor)
        {
            itemVisitor.Visit(this);
        }
    }
}