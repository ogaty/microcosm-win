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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace microcosm
{
    /// <summary>
    /// ColorPick.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPick : Window
    {
        public ColorPick()
        {
            InitializeComponent();
        }

        private void _colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Console.WriteLine(_colorPicker.SelectedColorText);
            Console.WriteLine(_colorPicker.SelectedColor?.A.ToString());
            Console.WriteLine(_colorPicker.SelectedColor?.R.ToString());
            Console.WriteLine(_colorPicker.SelectedColor?.G.ToString());
            Console.WriteLine(_colorPicker.SelectedColor?.B.ToString());

            ColorPicker c = (ColorPicker)sender;
            Console.WriteLine(c.SelectedColorText);
        }
    }
}
