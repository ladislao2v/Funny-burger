using Code.Services.LevelRewardService;
using Code.Services.ShopService;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create GemConfig", fileName = "GemConfig", order = 0)]
    public sealed class GemConfig : ScriptableObject, IItem
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _logo;
        [SerializeField] private int _requiredLevel;
        [SerializeField] private int _price;

        public Sprite Logo => _logo;
        public string Name => _name;
        public int RequiredLevel => _requiredLevel;
        public int Price => _price;

        public void Accept(IItemVisitor itemVisitor) => 
            itemVisitor.Visit(this);
    }
}