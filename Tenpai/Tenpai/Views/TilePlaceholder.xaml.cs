using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tenpai.Views
{
    /// <summary>
    /// TilePlaceholder.xaml の相互作用ロジック
    /// </summary>
    public partial class TilePlaceholder : UserControl
    {
        public TilePlaceholder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register("TileType", typeof(string), typeof(TilePlaceholder), new PropertyMetadata(new PropertyChangedCallback((sender, args) =>
        {
            var uc = (TilePlaceholder)sender;
            if (args.NewValue as string != string.Empty)
            {
                uc.TileEmptyVisibility = Visibility.Hidden;
            }
            else
            {
                uc.TileEmptyVisibility = Visibility.Visible;
            }
        })));

        public string TileType
        {
            get { return (string)GetValue(TileTypeProperty); }
            set { SetValue(TileTypeProperty, value); }
        }

        public static readonly DependencyProperty TileEmptyVisibilityProperty = DependencyProperty.Register("TileEmptyVisibility", typeof(Visibility), typeof(TilePlaceholder));

        public Visibility TileEmptyVisibility
        {
            get { return (Visibility)GetValue(TileEmptyVisibilityProperty); }
            set { SetValue(TileEmptyVisibilityProperty, value); }
        }

        public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register("SelectCommand", typeof(ICommand), typeof(TilePlaceholder));

        public ICommand SelectCommand
        {
            get { return (ICommand)GetValue(SelectCommandProperty); }
            set { SetValue(SelectCommandProperty, value); }
        }
    }
}
