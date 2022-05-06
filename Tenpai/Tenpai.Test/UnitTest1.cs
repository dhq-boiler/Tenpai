using Moq;
using NUnit.Framework;
using Prism.Services.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.ViewModels;
using Tenpai.Views;

namespace Tenpai.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void 一萬をポン()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_1>();
            vm.Tile1.Value = Tile.CreateInstance<Character_1>();
            vm.PonCommand.Execute(new Call(Tile.CreateInstance<Character_1>(), EOpponent.Kamicha));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile4.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile5.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile6.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile7.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile8.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile9.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile10.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile11.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile12.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile13.Value, Has.Property("Display").EqualTo("X"));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Not.Null);
            Assert.That(vm.Tile3.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile4.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile5.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile6.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile7.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile8.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile9.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile10.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile11.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile12.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile13.Value, Has.Property("Rotate").Null);

        }

        private string V(int length)
        {
            var ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += "V";
            }
            return ret;
        }

        private string R(int length)
        {
            var ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += "R";
            }
            return ret;
        }

        [Test]
        public void 五萬をポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateInstance<Character_5>(), EOpponent.Kamicha));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile4.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile5.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile6.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile7.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile8.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile9.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile10.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile11.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile12.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile13.Value, Has.Property("Display").EqualTo("X"));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Not.Null);
            Assert.That(vm.Tile3.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile4.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile5.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile6.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile7.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile8.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile9.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile10.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile11.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile12.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile13.Value, Has.Property("Rotate").Null);
        }

        private string Underbar(int length)
        {
            var ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += "_";
            }
            return ret;
        }

        [Test]
        public void 上家から赤ドラをポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateRedInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateRedInstance<Character_5>(), EOpponent.Kamicha));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile4.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile5.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile6.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile7.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile8.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile9.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile10.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile11.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile12.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile13.Value, Has.Property("Display").EqualTo("X"));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Not.Null);
            Assert.That(vm.Tile3.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile4.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile5.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile6.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile7.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile8.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile9.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile10.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile11.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile12.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile13.Value, Has.Property("Rotate").Null);

            Assert.That(vm.Tile0.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile1.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile2.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile3.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile4.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile5.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile6.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile7.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile8.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile9.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5"));
        }

        [Test]
        public void 対面から赤ドラをポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateRedInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateRedInstance<Character_5>(), EOpponent.Toimen));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile4.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile5.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile6.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile7.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile8.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile9.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile10.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile11.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile12.Value, Has.Property("Display").EqualTo("X"));
            Assert.That(vm.Tile13.Value, Has.Property("Display").EqualTo("X"));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Not.Null);
            Assert.That(vm.Tile3.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile4.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile5.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile6.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile7.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile8.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile9.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile10.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile11.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile12.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile13.Value, Has.Property("Rotate").Null);

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5"));
        }
    }
}