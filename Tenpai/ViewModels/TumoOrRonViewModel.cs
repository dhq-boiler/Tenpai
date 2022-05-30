using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Linq;
using System.Reactive.Disposables;

namespace Tenpai.ViewModels
{
    public class TumoOrRonViewModel : BindableBase, IDialogAware, IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        private bool disposedValue;

        public string Title => "上がりの選択";

        public event Action<IDialogResult> RequestClose;

        public ReactiveCommand TumoCommand { get; } = new ReactiveCommand();
        public ReactiveCommand RonCommand { get; } = new ReactiveCommand();
        public ReactiveCommand LayoutUpdatedCommand { get; } = new ReactiveCommand();
        public ReactivePropertySlim<double> Left { get; } = new ReactivePropertySlim<double>();
        public ReactivePropertySlim<double> Top { get; } = new ReactivePropertySlim<double>();

        public TumoOrRonViewModel()
        {
            TumoCommand.Subscribe(_ =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "AgariType", AgariType.Tsumo } }));
            })
            .AddTo(disposables);
            RonCommand.Subscribe(_ =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { { "AgariType", AgariType.Ron } }));
            })
            .AddTo(disposables);
            LayoutUpdatedCommand.Subscribe(_ =>
            {
                var windows = App.Current.Windows.OfType<DialogWindow>();
                var window = windows.SingleOrDefault(w => w.Title == Title && w.IsActive);
                if (window == null)
                    return;
                window.Left = Left.Value;
                window.Top = Top.Value;
            })
            .AddTo(disposables);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            disposables.Dispose();
            TumoCommand.Dispose();
            RonCommand.Dispose();
            LayoutUpdatedCommand.Dispose();
            Left.Dispose();
            Top.Dispose();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Left.Value = parameters.GetValue<double>("Left");
            Top.Value = parameters.GetValue<double>("Top");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposables.Dispose();
                }

                disposables = null;
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