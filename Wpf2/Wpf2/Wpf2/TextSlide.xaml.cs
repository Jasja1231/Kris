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

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for Titleslide.xaml
    /// </summary>
    public partial class TextSlide : UserControl
    {
        public int index {get; set; }
        public void removeframes()
        {
            this.title_box.BorderThickness = new Thickness(0);
            this.text_box.BorderThickness = new Thickness(0);
            this.title_box.Focusable = false;
            this.text_box.Focusable = false;
        }
        public void addframes()
        {
            this.title_box.ClearValue(TextBox.BorderThicknessProperty);
            this.text_box.ClearValue(TextBox.BorderThicknessProperty);
            this.title_box.Focusable = true;
            this.text_box.Focusable = true;
        }
        public TextSlide()
        {
            InitializeComponent();
        }
    }
}
