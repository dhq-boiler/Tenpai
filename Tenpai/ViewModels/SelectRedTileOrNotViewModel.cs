using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using Tenpai.Models.Tiles;

namespace Tenpai.ViewModels
{
    public class SelectRedTileOrNotViewModel : BindableBase, IDialogAware, IDisposable
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        private bool disposedValue;

        public string Title => "選択";

        public event Action<IDialogResult> RequestClose;

        public ReactivePropertySlim<Tile> Tile { get; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> RedTile { get; } = new ReactivePropertySlim<Tile>();
        public ReactiveCommand SelectCommand { get; } = new ReactiveCommand();
        public ReactiveCommand SelectRedCommand { get; } = new ReactiveCommand();

        public SelectRedTileOrNotViewModel()
        {
            SelectCommand.Subscribe(_ =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", Tile.Value } }));
            })
            .AddTo(_disposable);
            SelectRedCommand.Subscribe(_ =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "Result", RedTile.Value } }));
            })
            .AddTo(_disposable);
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
            Tile.Value = parameters.GetValue<Tile>("Tile");
            RedTile.Value = parameters.GetValue<Tile>("RedTile");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _disposable.Dispose();
                    Tile.Dispose();
                    RedTile.Dispose();
                    SelectCommand.Dispose();
                    SelectRedCommand.Dispose();
                }

                _disposable = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}