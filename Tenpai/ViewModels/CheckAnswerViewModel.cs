using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.ViewModels
{
    public class CheckAnswerViewModel : IDialogAware
    {
        public string Title => "答え合わせ";

        public event Action<IDialogResult> RequestClose;

        public ReactivePropertySlim<bool> Judge { get; } = new ReactivePropertySlim<bool>();

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Judge.Value = parameters.GetValue<bool>("Judge");
        }
    }
}
