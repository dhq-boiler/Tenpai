using Tenpai.Utils;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Threading;

namespace Tenpai.Models
{
    public class ScreenShotSource : AnalyzingSource
    {
        private DispatcherTimer _timer;
        private ScreenShotArea _Area;

        public class MatEventArgs : EventArgs
        {
            public Bitmap Bitmap { get; set; }
            public Mat Mat { get; set; }
        }

        public delegate void MyEventHandler<T>(T args);
        public event MyEventHandler<MatEventArgs> Tick;

        public ScreenShotSource(string name)
            : base(name)
        {
            HowToUpdate = new StaticUpdater(this);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1.0 / 30.0); //30fps
            _timer.Tick += timer_Tick;
        }

        public ScreenShotArea Area
        {
            get { return _Area; }
            set { SetProperty<ScreenShotArea>(ref _Area, value, "Area"); }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateImage();
            if (Mat != null)
            {
                Tick(new MatEventArgs() { Mat = Mat.Clone(), Bitmap = Mat.ToBitmap() });
            }
        }

        public override void Load(Mat mat)
        {
            base.Load(mat);
            Tick(new MatEventArgs() { Mat = mat.Clone(), Bitmap = Mat.ToBitmap() });
        }

        public override void UpdateImage()
        {
            if (!Area.IsReady) return;
            try
            {
                using (var b = Area.GetScreenShot())
                {
                    Mat = b.ToMat();
                    if (HowToUpdate is StaticUpdater
                        || HowToUpdate.InUse)
                    {
                        UpdateDisplay();
                    }
                }
            }
            catch (Win32Exception e)
            {
                Trace.WriteLine(e);
            }
            catch (ArgumentException e)
            {
                Trace.WriteLine(e);
            }
        }

        public override bool UpdateOnce
        {
            get { return HowToUpdate is StaticUpdater; }
        }

        public override void Activate()
        {
            if (_timer != null)
            {
                if (!_timer.IsEnabled)
                    _timer.Start();
            }
        }

        public override void Deactivate()
        {
            if (_timer != null)
            {
                if (_timer.IsEnabled)
                    _timer.Stop();
            }
        }
    }
}
