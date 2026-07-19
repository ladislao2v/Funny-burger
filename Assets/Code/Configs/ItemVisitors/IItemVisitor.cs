
namespace Code.Configs
{
    public interface IItemVisitor
    {
        void Visit(RecipeConfig recipeConfig);
        void Visit(GemConfig gemConfig);
        void Visit(CoinConfig coinConfig);
        void Visit(LocationConfig locationConfig);
        void Visit(FeatureConfig featureConfig);
        void Visit(BodySkinConfig bodySkinConfig);
        void Visit(HatSkinConfig hatSkinConfig);
    }
}