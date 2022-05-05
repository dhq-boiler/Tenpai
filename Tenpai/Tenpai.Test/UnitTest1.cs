using NUnit.Framework;
using System.Windows.Media;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.ViewModels;

namespace Tenpai.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var vm = new MainWindowViewModel();
            vm.Tile0.Value = Tile.CreateInstance<Character_1>();
            vm.Tile1.Value = Tile.CreateInstance<Character_1>();
            vm.PonCommand.Execute(new Call(Tile.CreateInstance<Character_1>(), EOpponent.Kamicha));

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
    }
}