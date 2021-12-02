using UnityEngine;
using Zenject;

namespace Sudocu
{
    public class BaseMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private MenuType _menu;

        [Inject] private MenuController _menuController;
        [Inject] private GameScreenModel _gameScreenModel;

        private bool _isActive = true;

        protected virtual void OnEnable()
        {
            _gameScreenModel.ChangeMenuEvent += OnMenuChanged;
        }

        protected virtual void OnDisable()
        {
            _gameScreenModel.ChangeMenuEvent -= OnMenuChanged;
        }

        protected void ChangeMenu(MenuType menuType)
        {
            _menuController.ChangeMenu(menuType);
        }

        private void OnMenuChanged(MenuType menu)
        {
            if (menu == _menu && !_isActive)
            {
                SetOn();
            }
            else if (menu != _menu && _isActive)
            {
                SetOff();
            }
        }

        private void SetOn()
        {
            _canvasGroup.SetOn();
            _isActive = true;
        }

        private void SetOff()
        {
            _canvasGroup.SetOff();
            _isActive = false;
        }
    }
}
