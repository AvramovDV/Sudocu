using UnityEngine;

namespace Sudocu
{
    public class MenuController
    {
        private GameScreenModel _gameScreenModel;

        public MenuController(GameScreenModel gameModel)
        {
            _gameScreenModel = gameModel;
            ChangeMenu(MenuType.MainMenu);
        }

        public void ChangeMenu(MenuType menu)
        {
            if (menu != MenuType.None)
            {
                _gameScreenModel.ChangeMenu(menu);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
