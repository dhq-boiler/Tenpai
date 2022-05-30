using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tenpai.Models.Tiles;

namespace Tenpai.Views
{
    /// <summary>
    /// ReadOnlyTilePlaceholder.xaml の相互作用ロジック
    /// </summary>
    public partial class ReadOnlyTilePlaceholder : UserControl
    {
        public ReadOnlyTilePlaceholder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register("TileType", typeof(Tile), typeof(ReadOnlyTilePlaceholder), new PropertyMetadata(new PropertyChangedCallback((sender, args) =>
        {
            var uc = (ReadOnlyTilePlaceholder)sender;
            if (args.NewValue as string != string.Empty && args.NewValue is Dummy)
            {
                uc.TileEmptyVisibility = Visibility.Visible;
            }
            else if (args.NewValue as string != string.Empty)
            {
                uc.TileEmptyVisibility = Visibility.Hidden;
            }
            else
            {
                uc.TileEmptyVisibility = Visibility.Visible;
            }
        })));

        public Tile TileType
        {
            get { return (Tile)GetValue(TileTypeProperty); }
            set { SetValue(TileTypeProperty, value); }
        }

        public static readonly DependencyProperty TileEmptyVisibilityProperty = DependencyProperty.Register("TileEmptyVisibility", typeof(Visibility), typeof(ReadOnlyTilePlaceholder));

        public Visibility TileEmptyVisibility
        {
            get { return (Visibility)GetValue(TileEmptyVisibilityProperty); }
            set { SetValue(TileEmptyVisibilityProperty, value); }
        }

        public static readonly DependencyProperty BackVisibilityProperty = DependencyProperty.Register("BackVisibility", typeof(Visibility), typeof(ReadOnlyTilePlaceholder), new PropertyMetadata(Visibility.Collapsed));

        public Visibility BackVisibility
        {
            get { return (Visibility)GetValue(BackVisibilityProperty); }
            set { SetValue(BackVisibilityProperty, value); }
        }
    }
}
