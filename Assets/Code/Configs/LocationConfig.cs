using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create LocationConfig", fileName = "LocationConfig", order = 0)]
    public class LocationConfig : Item
    {
        [field: SerializeField] public string Description {get; private set;}
        public override void Accept(IItemVisitor itemVisitor)
        {
            itemVisitor.Visit(this);
        }
    }
}