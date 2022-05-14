using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Windows;
using Tenpai.Models.Tiles;

namespace Tenpai.ViewModels
{
    public class TileLineupViewModel : IDialogAware
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public string Title => "牌の選択";

        public event Action<IDialogResult> RequestClose;

        public ReactiveCommand m1Command { get; } = new ReactiveCommand();
        public ReactiveCommand m2Command { get; } = new ReactiveCommand();
        public ReactiveCommand m3Command { get; } = new ReactiveCommand();
        public ReactiveCommand m4Command { get; } = new ReactiveCommand();
        public ReactiveCommand m5Command { get; } = new ReactiveCommand();
        public ReactiveCommand m5rCommand { get; } = new ReactiveCommand();
        public ReactiveCommand m6Command { get; } = new ReactiveCommand();
        public ReactiveCommand m7Command { get; } = new ReactiveCommand();
        public ReactiveCommand m8Command { get; } = new ReactiveCommand();
        public ReactiveCommand m9Command { get; } = new ReactiveCommand();
        public ReactiveCommand s1Command { get; } = new ReactiveCommand();
        public ReactiveCommand s2Command { get; } = new ReactiveCommand();
        public ReactiveCommand s3Command { get; } = new ReactiveCommand();
        public ReactiveCommand s4Command { get; } = new ReactiveCommand();
        public ReactiveCommand s5Command { get; } = new ReactiveCommand();
        public ReactiveCommand s5rCommand { get; } = new ReactiveCommand();
        public ReactiveCommand s6Command { get; } = new ReactiveCommand();
        public ReactiveCommand s7Command { get; } = new ReactiveCommand();
        public ReactiveCommand s8Command { get; } = new ReactiveCommand();
        public ReactiveCommand s9Command { get; } = new ReactiveCommand();
        public ReactiveCommand p1Command { get; } = new ReactiveCommand();
        public ReactiveCommand p2Command { get; } = new ReactiveCommand();
        public ReactiveCommand p3Command { get; } = new ReactiveCommand();
        public ReactiveCommand p4Command { get; } = new ReactiveCommand();
        public ReactiveCommand p5Command { get; } = new ReactiveCommand();
        public ReactiveCommand p5rCommand { get; } = new ReactiveCommand();
        public ReactiveCommand p6Command { get; } = new ReactiveCommand();
        public ReactiveCommand p7Command { get; } = new ReactiveCommand();
        public ReactiveCommand p8Command { get; } = new ReactiveCommand();
        public ReactiveCommand p9Command { get; } = new ReactiveCommand();
        public ReactiveCommand EastCommand { get; } = new ReactiveCommand();
        public ReactiveCommand SouthCommand { get; } = new ReactiveCommand();
        public ReactiveCommand WestCommand { get; } = new ReactiveCommand();
        public ReactiveCommand NorthCommand { get; } = new ReactiveCommand();
        public ReactiveCommand WhiteCommand { get; } = new ReactiveCommand();
        public ReactiveCommand GreenCommand { get; } = new ReactiveCommand();
        public ReactiveCommand RedCommand { get; } = new ReactiveCommand();
        public ReactiveCommand DeleteCommand { get; } = new ReactiveCommand();

        public TileLineupViewModel()
        {
            m1Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_1>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m2Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_2>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m3Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_3>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m4Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_4>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m5Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m5rCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateRedInstance<Character_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m6Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_6>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m7Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_7>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m8Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_8>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            m9Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Character_9>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s1Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_1>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s2Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_2>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s3Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_3>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s4Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_4>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s5Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s5rCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateRedInstance<Bamboo_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s6Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_6>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s7Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_7>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s8Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_8>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            s9Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Bamboo_9>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p1Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_1>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p2Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_2>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p3Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_3>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p4Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_4>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p5Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p5rCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateRedInstance<Dot_5>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p6Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_6>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p7Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_7>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p8Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_8>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            p9Command.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dot_9>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            EastCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<East>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            SouthCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<South>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            WestCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<West>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            NorthCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<North>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            WhiteCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<White>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            GreenCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Green>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            RedCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Red>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
            DeleteCommand.Subscribe(() =>
            {
                var dialogResult = new DialogResult(ButtonResult.OK);
                dialogResult.Parameters.Add("TileType", Tile.CreateInstance<Dummy>(Visibility.Visible, null));
                RequestClose.Invoke(dialogResult);
            })
            .AddTo(_disposables);
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}