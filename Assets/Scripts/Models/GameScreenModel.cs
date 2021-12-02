using System;

namespace Sudocu
{
    public class GameScreenModel
    {
        public event Action<MenuType> ChangeMenuEvent = (menuType) => { };

        public void ChangeMenu(MenuType menu)
        {
            ChangeMenuEvent.Invoke(menu);
        }
    }
}
