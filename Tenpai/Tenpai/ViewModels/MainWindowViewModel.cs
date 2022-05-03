using OpenCvSharp;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.Views;
using Unity;

namespace Tenpai.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        [Dependency]
        public IDialogService dialogService { get; set; }

        public ReactivePropertySlim<Tile> Tile0 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile1 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile2 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile3 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile4 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile5 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile6 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile7 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile8 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile9 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile10 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile11 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile12 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<Tile> Tile13 { get; set; } = new ReactivePropertySlim<Tile>();
        public ReactivePropertySlim<bool> IsArrangingTiles { get; } = new ReactivePropertySlim<bool>();
        private bool sortflag = false;

        public MainWindowViewModel()
        {
            SelectCommand.Subscribe(tp =>
            {
                IDialogResult dialogResult = null;
                dialogService.ShowDialog(nameof(TileLineup), (result) =>
                {
                    dialogResult = result;
                });
                if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                {
                    tp.TileType = dialogResult.Parameters.GetValue<Tile>("TileType");
                }
            })
            .AddTo(_disposables);
            Tile0.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile1.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile2.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile3.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile4.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile5.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile6.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile7.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile8.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile9.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile10.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile11.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile12.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            Tile13.Subscribe(_ =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            IsArrangingTiles.Subscribe(flag =>
            {
                SortIf();
            })
            .AddTo(_disposables);
        }

        private void SortIf()
        {
            if (sortflag)
                return;
            sortflag = true;
            if (IsArrangingTiles.Value)
                ArrangeTiles();
            sortflag = false;
        }

        private void ArrangeTiles()
        {
            var tiles = new[] { Tile0.Value, Tile1.Value, Tile2.Value, Tile3.Value, Tile4.Value, Tile5.Value, Tile6.Value, Tile7.Value, Tile8.Value, Tile9.Value, Tile10.Value, Tile11.Value, Tile12.Value, Tile13.Value }.ToList();
            tiles = tiles.Where(x => x != null).ToList();
            tiles.Sort();
            for (int i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                switch (i)
                {
                    case 0:
                        Tile0.Value = tile;
                        break;
                    case 1:
                        Tile1.Value = tile;
                        break;
                    case 2:
                        Tile2.Value = tile;
                        break;
                    case 3:
                        Tile3.Value = tile;
                        break;
                    case 4:
                        Tile4.Value = tile;
                        break;
                    case 5:
                        Tile5.Value = tile;
                        break;
                    case 6:
                        Tile6.Value = tile;
                        break;
                    case 7:
                        Tile7.Value = tile;
                        break;
                    case 8:
                        Tile8.Value = tile;
                        break;
                    case 9:
                        Tile9.Value = tile;
                        break;
                    case 10:
                        Tile10.Value = tile;
                        break;
                    case 11:
                        Tile11.Value = tile;
                        break;
                    case 12:
                        Tile12.Value = tile;
                        break;
                    case 13:
                        Tile13.Value = tile;
                        break;
                }
            }
        }

        public ReactiveCommand<TilePlaceholder> SelectCommand { get; } = new ReactiveCommand<TilePlaceholder>();

        private static unsafe void Paste(Mat target, Mat pasting, Rect rect)
        {
            Debug.Assert(pasting.Width == rect.Width);
            Debug.Assert(pasting.Height == rect.Height);
            Debug.Assert(target.Type() == pasting.Type());

            int yy = 0;
            for (int y = 0; y < target.Height; y++)
            {
                byte* p_t = (byte*)target.Ptr(y);
                if (y >= rect.Y && y < rect.Y + rect.Height)
                {
                    byte* p_p = (byte*)pasting.Ptr(yy);
                    int xx = 0;
                    for (int x = 0; x < target.Width; x++)
                    {
                        if (x >= rect.X && x < rect.X + rect.Width)
                        {
                            for (int c = 0; c < target.Channels(); c++)
                            {
                                *(p_t + x * target.Channels() + c) = *(p_p + xx * pasting.Channels() + c);
                            }
                            xx++;
                        }
                    }
                    yy++;
                }
            }
        }

        private Mat getHomography(KeyPoint[] kpsA, KeyPoint[] kpsB, Mat featuresA, Mat featuresB, DMatch[] matches, int reprojThresh)
        {
            //# convert the keypoints to numpy arrays
            if (matches.Length > 4)
            {
                List<Point2f> PtA = new List<Point2f>(matches.Length);
                List<Point2f> PtB = new List<Point2f>(matches.Length);
                foreach (var m in matches)
                {
                    KeyPoint kpsAI = kpsA[m.QueryIdx];
                    KeyPoint kpsBI = kpsB[m.TrainIdx];

                    PtA.Add(new Point2f(kpsAI.Pt.X, kpsAI.Pt.Y));
                    PtB.Add(new Point2f(kpsBI.Pt.X, kpsBI.Pt.Y));
                }
                InputArray ptsA = InputArray.Create(PtA);
                InputArray ptsB = InputArray.Create(PtB);

                //# estimate the homography between the sets of points
                //step 1: find the homography H with findHomography you will get a classic structure for homography
                Mat H = Cv2.FindHomography(ptsA, ptsB, HomographyMethods.Ransac, reprojThresh);

                Console.WriteLine("Homography:");
                for (var rowIndex = 0; rowIndex < H.Rows; rowIndex++)
                {
                    for (var colIndex = 0; colIndex < H.Cols; colIndex++)
                    {
                        double hVal = H.At<double>(rowIndex, colIndex);
                        Console.Write($"{hVal} ");
                    }
                    Console.WriteLine("");
                }

                return H;
            }
            else
                return null;
        }

        private unsafe static Mat Negate(Mat target)
        {
            var mat = target.Clone();

            int channels = mat.Channels();
            for (int y = 0; y < mat.Height; y++)
            {
                byte* p = (byte*)mat.Ptr(y);
                for (int x = 0; x < mat.Width; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        *(p + x * channels + c) = (byte)(255 - *(p + x * channels + c));
                    }
                }
            }
            return mat;
        }

        private unsafe static void Mask(Mat clientRectPool, Mat m, Mat white)
        {
            Debug.Assert(clientRectPool.Size() == m.Size());
            Debug.Assert(clientRectPool.Size() == white.Size());

            int channels = clientRectPool.Channels();
            for (int y = 0; y < clientRectPool.Height; y++)
            {
                byte* p_crp = (byte*)clientRectPool.Ptr(y);
                byte* p_m = (byte*)m.Ptr(y);
                byte* p_w = (byte*)white.Ptr(y);
                for (int x = 0; x < clientRectPool.Width; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        *(p_m + x * channels + c) = Math.Min(*(p_crp + x * channels + c), *(p_w + x * channels + c));
                    }
                }
            }
        }

        private unsafe bool AreEqual(Mat m, Mat target)
        {
            using (Mat temp = new Mat())
            {
                if (m.Size() != target.Size())
                    return false;
                if (m.Channels() != target.Channels())
                    return false;

                int channels = m.Channels();
                for (int y = 0; y < m.Height; y++)
                {
                    byte* p_m = (byte*)m.Ptr(y);
                    byte* p_target = (byte*)target.Ptr(y);
                    for (int x = 0; x < m.Width; x++)
                    {
                        for (int c = 0; c < channels; c++)
                        {
                            var a = *(p_m + x * channels + c);
                            var b = *(p_target + x * channels + c);
                            if (a != b)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private unsafe Mat Fill(Mat target, int fromInclusive, int toExclusive, bool vertical, Scalar fillColor)
        {
            var mat = new Mat(target, new Rect(0, 0, target.Width, target.Height));
            int channels = mat.Channels();
            byte* p;
            for (int y = 0; y < mat.Height; y++)
            {
                if (vertical)
                {
                    if (fromInclusive <= y && y < toExclusive)
                    {
                        p = (byte*)mat.Ptr(y);
                        for (int x = 0; x < mat.Width; x++)
                        {
                            *(p + x * channels + 0) = (byte)fillColor.Val0;
                            *(p + x * channels + 1) = (byte)fillColor.Val1;
                            *(p + x * channels + 2) = (byte)fillColor.Val2;
                        }
                    }
                }
                p = (byte*)mat.Ptr(y);
                for (int x = 0; x < mat.Width; x++)
                {
                    if (!vertical && fromInclusive <= x && x < toExclusive)
                    {
                        *(p + x * channels + 0) = (byte)fillColor.Val0;
                        *(p + x * channels + 1) = (byte)fillColor.Val1;
                        *(p + x * channels + 2) = (byte)fillColor.Val2;
                    }
                }
            }
            return mat;
        }

        private unsafe Mat Subtract(Mat previous, Mat roi)
        {
            Debug.Assert(roi.Channels() == previous.Channels());
            Debug.Assert(roi.Size() == previous.Size());
            Debug.Assert(roi.Depth() == previous.Depth());

            var ret = new Mat(roi.Rows, roi.Cols, roi.Type());
            int channels = roi.Channels();
            for (int y = 0; y < roi.Rows; y++)
            {
                byte* p_r = (byte*)ret.Ptr(y);
                byte* p_roi = (byte*)roi.Ptr(y);
                byte* p_p = (byte*)previous.Ptr(y);
                for (int x = 0; x < roi.Cols; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        byte result = (byte)(*(p_roi + x * channels + c) - *(p_p + x * channels + c));
                        *(p_r + x * channels + c) = result > 0 ? (byte)result : (byte)0;
                    }
                }
            }
            return ret;
        }

        private static TemplateCollection _Templates;

        private TemplateCollection Templates
        {
            get
            {
                if (_Templates != null)
                    return _Templates;
                var templateCollection = new TemplateCollection();
                templateCollection.Add(new Template("m1", "Assets/m1.png"));
                templateCollection.Add(new Template("m2", "Assets/m2.png"));
                templateCollection.Add(new Template("m3", "Assets/m3.png"));
                templateCollection.Add(new Template("m4", "Assets/m4.png"));
                templateCollection.Add(new Template("m5", "Assets/m5.png"));
                templateCollection.Add(new Template("m5r", "Assets/m5r.png"));
                templateCollection.Add(new Template("m6", "Assets/m6.png"));
                templateCollection.Add(new Template("m7", "Assets/m7.png"));
                templateCollection.Add(new Template("m8", "Assets/m8.png"));
                templateCollection.Add(new Template("m9", "Assets/m9.png"));
                templateCollection.Add(new Template("p1", "Assets/p1.png"));
                templateCollection.Add(new Template("p2", "Assets/p2.png"));
                templateCollection.Add(new Template("p3", "Assets/p3.png"));
                templateCollection.Add(new Template("p4", "Assets/p4.png"));
                templateCollection.Add(new Template("p5", "Assets/p5.png"));
                templateCollection.Add(new Template("p5r", "Assets/p5r.png"));
                templateCollection.Add(new Template("p6", "Assets/p6.png"));
                templateCollection.Add(new Template("p7", "Assets/p7.png"));
                templateCollection.Add(new Template("p8", "Assets/p8.png"));
                templateCollection.Add(new Template("p9", "Assets/p9.png"));
                templateCollection.Add(new Template("s1", "Assets/s1.png"));
                templateCollection.Add(new Template("s2", "Assets/s2.png"));
                templateCollection.Add(new Template("s3", "Assets/s3.png"));
                templateCollection.Add(new Template("s4", "Assets/s4.png"));
                templateCollection.Add(new Template("s5", "Assets/s5.png"));
                templateCollection.Add(new Template("s5r", "Assets/s5r.png"));
                templateCollection.Add(new Template("s6", "Assets/s6.png"));
                templateCollection.Add(new Template("s7", "Assets/s7.png"));
                templateCollection.Add(new Template("s8", "Assets/s8.png"));
                templateCollection.Add(new Template("s9", "Assets/s9.png"));
                templateCollection.Add(new Template("中", "Assets/中.png"));
                templateCollection.Add(new Template("北", "Assets/北.png"));
                templateCollection.Add(new Template("南", "Assets/南.png"));
                templateCollection.Add(new Template("東", "Assets/東.png"));
                templateCollection.Add(new Template("發", "Assets/發.png"));
                templateCollection.Add(new Template("白", "Assets/白.png"));
                templateCollection.Add(new Template("西", "Assets/西.png"));
                _Templates = templateCollection;
                return templateCollection;
            }
        }
    }
}
