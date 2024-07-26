using Code.Configs;

namespace Code.UI.Order
{
    public interface IOrderView : IView
    {
        void OnOrder(RecipeConfig config);
    }
}