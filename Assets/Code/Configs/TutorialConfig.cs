using System;
using System.Collections.Generic;
using Code.Ingredients;
using Code.Services.LocalizationService;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create TutorialConfig", fileName = "TutorialConfig", order = 0)]
    public sealed class TutorialConfig : ScriptableObject
    {
        [field: Header("Order")]
        [field: SerializeField] public RecipeConfig Recipe { get; private set; }
        [field: SerializeField] public List<IngredientType> WrongBurger { get; private set; }
        
        [field: Header("Boxes available for purchase during tutorial")]
        [field: SerializeField] public List<IngredientType> Boxes { get; private set; }

        [field: Header("Hints")]
        [field: SerializeField] public TutorialHintConfig WelcomeHint { get; private set; }
        [field: SerializeField] public TutorialHintConfig TrashHint { get; private set; }
        [field: SerializeField] public TutorialHintConfig TableHint { get; private set; }
        [field: SerializeField] public TutorialHintConfig LoyaltyHint { get; private set; }
        [field: SerializeField] public TutorialHintConfig LetsGoHint{ get; private set; }
    }

    [Serializable]
    public sealed class TutorialHintConfig
    {
        [field: SerializeField] public TranslateData Title { get; private set; }
        [field: SerializeField] public TranslateData Description { get; private set; }
    }
}
