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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int index = 0;
        private List<Control> controls = new List<Control>();
        private List<Control> slides = new List<Control>();
        private bool in_presentation = false;
        private Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private SolidColorBrush get_brush()
        {
           return new SolidColorBrush(Color.FromRgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));

        }
        private void new_presentation(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
            slides.Clear();
        }
        private void show_presentation(object sender, RoutedEventArgs e)
        {
            in_presentation = true;

                if (panel.Children.Count==0)
                    MessageBox.Show(this,"error","error", MessageBoxButton.OK , MessageBoxImage.Error);
                else 
                {
                    this.WindowState = System.Windows.WindowState.Maximized;
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    this.Cursor = Cursors.None;
                    ////////////////////////////
                    foreach (Control c in panel.Children)
                        controls.Add(c);
                    panel.Children.Clear();
                    if (!(slides.Count == 0))
                    {
                        box.Child = slides.ElementAt(0);
                        box.Visibility = Visibility.Visible;
                        border.Visibility = Visibility.Visible;
                    }
                }
        }
        private void add_slide(object sender, RoutedEventArgs e)
        {
            Base b = new Base();
            Base copy = new Base();
            b.c.Background = get_brush();
            copy.c.Background = b.c.Background;
            panel.Children.Add(b);
            slides.Add(copy);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (in_presentation)
            {
                if (e.Key == Key.Right)
                {
                    if (index < slides.Count - 1)
                    {
                        box.Child = slides.ElementAt(++index);
                    }
                    else
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                        in_presentation = false;
                        box.Visibility = Visibility.Hidden;
                        border.Visibility = Visibility.Hidden;
                        index = 0;
                        this.Cursor = Cursors.Arrow;

                        foreach (Control control in controls)
                            panel.Children.Add(control);
                    }
                
                }

                if (e.Key == Key.Left)
                {
                    if (index > 0)
                    {
                        box.Child = slides.ElementAt(--index);
                    }
                      
                }

                if (e.Key == Key.Escape)
                {
                    box.Visibility = Visibility.Hidden;
                    border.Visibility = Visibility.Hidden;
                    this.WindowState = System.Windows.WindowState.Normal;
                    this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                    in_presentation = false;
                    index = 0;
                    this.Cursor = Cursors.Arrow;
                    foreach (Control control in controls)
                        panel.Children.Add(control);
                }  
            
            }
        }

        private void add_text_slide (object sender, RoutedEventArgs e)
        {
            TextSlide t = new TextSlide();
            TextSlide copy = new TextSlide();
            t.c.Background = get_brush();
            copy.c.Background = t.c.Background;
            t.text_box.Background = t.c.Background;
            t.title_box.Background = t.c.Background;
            copy.title_box.Background = t.c.Background;
            copy.text_box.Background = t.c.Background;
            t.index = copy.index = slides.Count;
            panel.Children.Add(t);
            slides.Add(copy);
        }

        private void add_title_slide(object sender, RoutedEventArgs e)
        {
            Title_slide t = new Title_slide();
            Title_slide copy = new Title_slide();
            t.c.Background = get_brush();
            copy.c.Background = t.c.Background;
            t.subtitle_box.Background = t.c.Background;
            t.title_box.Background = t.c.Background;
            copy.title_box.Background = t.c.Background;
            copy.subtitle_box.Background = t.c.Background;
            t.index = copy.index = slides.Count;
            panel.Children.Add(t);
            slides.Add(copy);
        }

        private void add_picture_slide(object sender, RoutedEventArgs e)
        {
            PictureSlide p = new PictureSlide();
            PictureSlide copy = new PictureSlide();
            p.c.Background = get_brush();
            copy.c.Background = p.c.Background;
            p.textbox.Background = p.c.Background;
            copy.textbox.Background = p.c.Background;
            p.index = copy.index = slides.Count;
            panel.Children.Add(p);
            slides.Add(copy);
        }



    }
}
