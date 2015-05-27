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
using Microsoft.Win32;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for PictureSlide.xaml
    /// </summary>
    public partial class PictureSlide : UserControl
    {
        public int index { get; set; }
        public PictureSlide()
        {
            InitializeComponent();
        }

        public void removeframes ()
        {
            this.textbox.BorderThickness = new Thickness(0);
            this.image.Focusable = false;
            this.textbox.Focusable = false; 
        }
        public void addframes()
        {
            this.textbox.ClearValue(TextBox.BorderThicknessProperty);
            this.image.Focusable = true;
            this.textbox.Focusable = true; 
        }
        private void c_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Title = "Select a picture";
                    op.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg";
                    if (op.ShowDialog() == true)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(op.FileName);
                        bitmap.EndInit();
                        this.image.Source = bitmap;
                    }
                }

        }
    }
}
