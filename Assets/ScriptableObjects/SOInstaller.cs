using UnityEngine;
using Zenject;

namespace Sudocu
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private LocalizationLibrary _localizationLibrary;

        public override void InstallBindings()
        {
            Container.Bind<LocalizationLibrary>().FromInstance(_localizationLibrary).AsSingle();
        }
    }
}
