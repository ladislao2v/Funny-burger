using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.SkinsService;
using Code.Skins;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public class ChefSkinsApplier : MonoBehaviour
    {
        [Header("Body skin")]
        [SerializeField] private SkinnedMeshRenderer _bodyMeshRenderer;
        
        [Header("Hat Skin")]
        [SerializeField] private List<HatSkin> _skinHatObjects;

        private ISkinsService _skinsService;

        [Inject]
        private void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }

        private void Start()
        {
            ApplyBodySkin(_skinsService.CurrentBodySkin);
            ApplyHatSkin(_skinsService.CurrentHatSkin);
        }

        private void OnEnable()
        {
            _skinsService.BodySkinChanged += ApplyBodySkin;
            _skinsService.HatSkinChanged += ApplyHatSkin;
        }

        private void OnDisable()
        {
            _skinsService.BodySkinChanged -= ApplyBodySkin;
            _skinsService.HatSkinChanged -= ApplyHatSkin;
        }

        private void ApplyBodySkin(BodySkinConfig skin) =>
            _bodyMeshRenderer.material = skin.SkinMaterial;

        private void ApplyHatSkin(HatSkinConfig skin)
        {
            if(_skinHatObjects.All(x => x.Id != skin.HatSkinId))
                throw new ArgumentException(nameof(skin.HatSkinId));
            
            foreach (var hat in _skinHatObjects) 
                hat.Skin.SetActive(false);
            
            _skinHatObjects.FirstOrDefault(x => x.Id == skin.HatSkinId)?.Skin.SetActive(true);
        }
    }

    [Serializable]
    public class HatSkin
    {
        [field: SerializeField] public HatSkinType Id { get; private set; }
        [field: SerializeField] public GameObject Skin { get; private set; }
    }
}