using Code.Ingredients;
using Code.Services.PopupService;
using Code.UI.Popups;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.PopupFactory
{
    public interface IPopupFactory
    {
        UniTask<Popup> Create(PopupType popupType);
    }
}