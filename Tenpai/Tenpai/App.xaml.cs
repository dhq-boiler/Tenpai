using Tenpai.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tenpai
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry cr)
        {
            cr.RegisterDialog<Views.TileLineup, ViewModels.TileLineupViewModel>();
            cr.RegisterDialog<Views.SelectRedTileOrNot, ViewModels.SelectRedTileOrNotViewModel>();
        }
    }
}
