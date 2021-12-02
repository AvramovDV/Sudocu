using Zenject;

namespace Sudocu
{
    public class MainView : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameModel>().AsSingle();
            Container.Bind<GameScreenModel>().AsSingle();
            Container.Bind<SelectedCellModel>().AsSingle();

            Container.Bind<MenuController>().AsSingle();
            Container.Bind<LocalizationController>().AsSingle();
        }
    }
}
