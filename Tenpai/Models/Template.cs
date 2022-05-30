using OpenCvSharp;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models
{
    public class Template : IDisposable
    {
        private bool disposedValue;

        public ReactivePropertySlim<string> Name { get; } = new ReactivePropertySlim<string>();
        public ReactivePropertySlim<Mat> Mat { get; } = new ReactivePropertySlim<Mat>();

        public Template(string name, string path)
        {
            Name.Value = name;
            Mat.Value = new Mat(path);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Name.Dispose();
                    Mat.Dispose();
                }

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
