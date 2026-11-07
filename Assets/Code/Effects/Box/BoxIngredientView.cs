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
        private Dictionary<IngredientType, GameObject> _previewByType;

        private GameObject CurrentView => _viewByType[_box.IngredientType];
        private GameObject CurrentPreview => _previewByType[_box.IngredientType];

        private void Awake()
        {
            foreach (var link in _viewsLinks) 
                link.View.SetActive(false);

            _viewByType = _viewsLinks.ToDictionary(x => x.Type, x => x.View);
            _previewByType = _viewsLinks.ToDictionary(x => x.Type, x => x.Preview);
        }

        public void ShowView()
        {
            CurrentView.SetActive(true);
            CurrentPreview.SetActive(false);
        }

        public void HideView()
        {
            CurrentView.SetActive(false);
            CurrentPreview.SetActive(true);
        }
    }

    [Serializable]
    public class BoxViewLink
    {
        [field: SerializeField] public IngredientType Type { get; private set; }
        [field: SerializeField] public GameObject View { get; private set; }
        [field: SerializeField] public GameObject Preview { get; private set; }
    }
}