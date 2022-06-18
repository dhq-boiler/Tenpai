using Moq;
using NUnit.Framework;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Pose;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Utils;
using Tenpai.ViewModels;

namespace Tenpai.Test
{
    [TestFixture]
    public class AutoDetectionTest
    {
        [Test]
        public void A()
        {
            var mainWindowVM = new Mock<MainWindowViewModel>();
            mainWindowVM.Object.dialogService = new Mock<IDialogService>().Object;
            var screenShotWindowMock = new Mock<ScreenShotWindow>();
            mainWindowVM.Object.ScreenShotSource.Value.Area = screenShotWindowMock.Object;
            mainWindowVM.Object.TestByOneImageCommand.Execute("img\\2022-06-18 (2).png");
            mainWindowVM.Object.TestByOneImageCommand.Execute("img\\2022-06-18 (3).png");
            mainWindowVM.Object.TestByOneImageCommand.Execute("img\\2022-06-18 (4).png");
        }
    }
}
