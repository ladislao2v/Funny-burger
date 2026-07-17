using System;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create CoinConfig", fileName = "CoinConfig", order = 0)]
    public sealed class CoinConfig : Item
    {
        [field: SerializeField] public int Count { get; private set; }

        public override void Accept(IItemVisitor itemVisitor) => 
            itemVisitor.Visit(this);
    }
}