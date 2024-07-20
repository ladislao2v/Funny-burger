﻿using Code.Services.Factories.PopupFactory;
using Code.UI.Popups;
using UnityEngine;

namespace Code.Services.PopupService
{
    public interface IPopupService
    {
        void ShowPopup(PopupType popupType);
    }

    public class PopupService : IPopupService
    {
        private readonly IPopupFactory _popupFactory;
        private readonly IPopupContainer _popupContainer;

        public PopupService(IPopupFactory popupFactory, IPopupContainer popupContainer)
        {
            _popupFactory = popupFactory;
            _popupContainer = popupContainer;
        }
        
        public async void ShowPopup(PopupType popupType)
        {
            Popup popup = 
                await _popupFactory.Create(popupType);

            _popupContainer.LinkPopup(popup);
        }
    }
}