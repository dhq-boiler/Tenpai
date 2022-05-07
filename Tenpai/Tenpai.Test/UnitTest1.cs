using Moq;
using NUnit.Framework;
using Prism.Services.Dialogs;
using System;
using System.Linq;
using System.Windows;
using Tenpai.Extensions;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.ViewModels;
using Tenpai.Views;
using Tenpai.Yaku.Meld.Detector;

namespace Tenpai.Test
{
    public class Tests
    {
        private string Str(string str, int length)
        {
            var ret = "";
            for (int i = 0; i < length; i++)
            {
                ret += str;
            }
            return ret;
        }

        private string V(int length)
        {
            return Str("V", length);
        }

        private string R(int length)
        {
            return Str("R", length);
        }

        private string Underbar(int length)
        {
            return Str("_", length);
        }

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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

        [Test]
        public void 上家から五萬をポン()
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

        [Test]
        public void 対面から五萬をポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateInstance<Character_5>(), EOpponent.Toimen));

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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

        [Test]
        public void 下家から五萬をポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateInstance<Character_5>(), EOpponent.Shimocha));

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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
        }

        [Test]
        public void 下家から赤ドラをポン()
        {
            var vm = new MainWindowViewModel();
            var mock = new Mock<IDialogService>();
            mock.Setup(x => x.ShowDialog(nameof(SelectRedTileOrNot), It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback((string name, IDialogParameters parameters, Action<IDialogResult> callback) => callback(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.CreateRedInstance<Character_5>() } })));
            vm.dialogService = mock.Object;
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.PonCommand.Execute(new Call(Tile.CreateRedInstance<Character_5>(), EOpponent.Shimocha));

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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5r"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Not.Null);
        }

        [Test]
        public void 上家からチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(called) && x.Tiles[1].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[2].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[0].Rotate != null && x.CallFrom == EOpponent.Kamicha);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Kamicha, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m4"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
        }

        [Test]
        public void 対面からチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[1].Equals(called) && x.Tiles[2].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[1].Rotate != null && x.CallFrom == EOpponent.Toimen);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Toimen, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m4"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
        }

        [Test]
        public void 下家からチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[1].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[2].Equals(called) && x.Tiles[2].Rotate != null && x.CallFrom == EOpponent.Shimocha);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Shimocha, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m4"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Not.Null);
        }


        [Test]
        public void 上家から赤ドラをチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateRedInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(called) && x.Tiles[1].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[2].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[0].Rotate != null && x.CallFrom == EOpponent.Kamicha);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Kamicha, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m4"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
        }

        [Test]
        public void 対面から赤ドラをチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateRedInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[1].Equals(called) && x.Tiles[2].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[1].Rotate != null && x.CallFrom == EOpponent.Toimen);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Toimen, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5r"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m4"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Not.Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
        }

        [Test]
        public void 下家から赤ドラをチー()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_3>();
            vm.Tile1.Value = Tile.CreateInstance<Character_4>();
            var called = Tile.CreateRedInstance<Character_5>();
            var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(called));
            var completedMelds = vm.ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(called));
            var selectedMeld = completedMelds.FirstOrDefault(x => x.Tiles[0].Equals(Tile.CreateInstance<Character_3>()) && x.Tiles[1].Equals(Tile.CreateInstance<Character_4>()) && x.Tiles[2].Equals(called) && x.Tiles[2].Rotate != null && x.CallFrom == EOpponent.Shimocha);
            vm.ChiCommand.Execute(new Call(called, EOpponent.Shimocha, selectedMeld));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m4"));
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
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m3"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m4"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5r"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Not.Null);
        }

        [Test]
        public void 一萬を暗カン()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_1>();
            vm.Tile1.Value = Tile.CreateInstance<Character_1>();
            vm.Tile2.Value = Tile.CreateInstance<Character_1>();
            vm.Tile3.Value = Tile.CreateInstance<Character_1>();
            var quads = MeldDetector.FindQuads(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.Tiles.Contains(vm.Tile0.Value));
            vm.AnkanCommand.Execute(new Call(Tile.CreateInstance<Character_1>(), quads.First()));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("m1"));
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

            Assert.That(vm.Tile0.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile1.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile2.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile3.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile4.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile5.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile6.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile7.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile8.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile9.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Null);
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

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m1"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Display").EqualTo("m1"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Rotate").Null);

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Rotate").Null);
        }

        [Test]
        public void 五萬を暗カン()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_5>();
            vm.Tile1.Value = Tile.CreateInstance<Character_5>();
            vm.Tile2.Value = Tile.CreateInstance<Character_5>();
            vm.Tile3.Value = Tile.CreateRedInstance<Character_5>();
            var quads = MeldDetector.FindQuads(vm.Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray());
            quads = quads.Where(x => x.Tiles.ContainsRedSuitedTileIncluding(vm.Tile0.Value)).ToArray();
            vm.AnkanCommand.Execute(new Call(quads.First()));

            Console.WriteLine(string.Join<Tile>(',', vm.Tiles.ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Rotate != null ? R(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));
            Console.WriteLine(string.Join<string>(',', vm.Tiles.Select(x => x.Visibility.Value == System.Windows.Visibility.Visible ? V(x.Display.Length) : Underbar(x.Display.Length)).ToArray()));

            Assert.That(vm.Tile0.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile1.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile2.Value, Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.Tile3.Value, Has.Property("Display").EqualTo("m5r"));
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

            Assert.That(vm.Tile0.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile1.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile2.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile3.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Collapsed));
            Assert.That(vm.Tile4.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile5.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile6.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile7.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile8.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile9.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile11.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile12.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile10.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));
            Assert.That(vm.Tile13.Value, Has.Property("Visibility").Property("Value").EqualTo(Visibility.Visible));

            Assert.That(vm.Tile0.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile1.Value, Has.Property("Rotate").Null);
            Assert.That(vm.Tile2.Value, Has.Property("Rotate").Null);
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
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Display").EqualTo("m5"));
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Display").EqualTo("m5r"));

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Rotate").Null);

            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[0], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[1], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[2], Has.Property("Rotate").Null);
            Assert.That(vm.SarashiHai.ElementAt(0).Tiles[3], Has.Property("Rotate").Null);
        }
    }
}