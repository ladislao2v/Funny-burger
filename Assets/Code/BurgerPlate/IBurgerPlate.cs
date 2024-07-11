using Code.Ingredients;

namespace Code.BurgerPlate
{
    public interface IBurgerPlate
    {
        public bool IsEmpty { get; }
        
        void Add(Ingredient ingredient);
        void Clear();
    }
}