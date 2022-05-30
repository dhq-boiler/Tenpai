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
    /// SelectRedTileOrNot.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectRedTileOrNot : UserControl
    {
        public SelectRedTileOrNot()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RedTileProperty = DependencyProperty.Register("RedTile", typeof(Tile), typeof(SelectRedTileOrNot));

        public Tile RedTile
        {
            get { return (Tile)GetValue(RedTileProperty); }
            set { SetValue(RedTileProperty, value); }
        }

        public static readonly DependencyProperty TileProperty = DependencyProperty.Register("Tile", typeof(Tile), typeof(SelectRedTileOrNot));

        public Tile Tile
        {
            get { return (Tile)GetValue(TileProperty); }
            set { SetValue(TileProperty, value); }
        }



        public static readonly DependencyProperty SelectRedCommandProperty = DependencyProperty.Register("SelectRedCommand", typeof(ICommand), typeof(SelectRedTileOrNot));

        public ICommand SelectRedCommand
        {
            get { return (ICommand)GetValue(SelectRedCommandProperty); }
            set { SetValue(SelectRedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register("SelectCommand", typeof(ICommand), typeof(SelectRedTileOrNot));

        public ICommand SelectCommand
        {
            get { return (ICommand)GetValue(SelectCommandProperty); }
            set { SetValue(SelectCommandProperty, value); }
        }
    }
}
