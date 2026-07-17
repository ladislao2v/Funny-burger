using System;
using System.Collections.Generic;
using System.Linq;
using Code.Ingredients;
using Code.Triggers.Box;
using UnityEngine;

namespace Code.Effects.Box
{
    public class BoxIngredientView : MonoBehaviour
    {
        [SerializeField] private IngredientBoxTrigger _box;
        [SerializeField] private BoxViewLink[] _viewsLinks;
        
        private Dictionary<IngredientType, GameObject> _viewByType;

        private GameObject CurrentView => _viewByType[_box.IngredientType];

        private void Awake()
        {
            foreach (var link in _viewsLinks) 
                link.View.SetActive(false);

            _viewByType = _viewsLinks.ToDictionary(x => x.Type, x => x.View);
        }

        public void ShowView() => CurrentView.SetActive(true);
        public void HideView() => CurrentView.SetActive(false);
    }

    [Serializable]
    public class BoxViewLink
    {
        [field: SerializeField] public IngredientType Type { get; private set; }
        [field: SerializeField] public GameObject View { get; private set; }
    }
}