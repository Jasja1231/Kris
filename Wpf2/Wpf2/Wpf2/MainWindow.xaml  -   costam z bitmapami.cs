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
using System.IO;
using System.Drawing;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int index = 0;
        private List<UserControl> controls = new List<UserControl>();
        private List<Image> images = new List<Image>();
        //private List<Control> slides = new List<Control>();
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
            controls.Clear();
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
                  /*  controls.Clear();
                    foreach (UserControl c in panel.Children)
                        controls.Add(c);
                    panel.Children.Clear(); */
                    foreach (UserControl c in panel.Children)
                        images.Add(get_image(c));

                    if (!(controls.Count == 0))
                    {
                        box.Child = images.ElementAt(0);
                        box.Visibility = Visibility.Visible;
                        border.Visibility = Visibility.Visible;
                    }
                }
        }

        private static BitmapSource CaptureScreen(Visual target, double dpiX, double dpiY)
        {
            if (target == null)
            {
                return null;
            }
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                                            (int)(bounds.Height * dpiY / 96.0),
                                                            dpiX,
                                                            dpiY,
                                                            PixelFormats.Pbgra32);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);
            return rtb;
        }

        private Image get_image(UserControl control)
        {
         /*   control.Measure(new Size(550, 400)); // adjust this to your needs
            control.Arrange(new Rect(0, 0, 550, 400)); // adjust this to your needs
            RenderTargetBitmap rtb = new RenderTargetBitmap(550, 400, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(control);
            Image image = new Image();
            image.Source=rtb;
            image.Visibility = Visibility.Visible; */
            BitmapSource b = CaptureScreen(control, 550, 400);
            Image image = new Image();
            image.Source = b;
            return image;    
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (in_presentation)
            {
                box.Focus();
                if (e.Key == Key.Right)
                {
                    if (index < images.Count - 1)
                    {
                        box.Child = images.ElementAt(++index);
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

                        box.Child = null;
                        foreach (UserControl control in controls)
                            panel.Children.Add(control);
                    }
                
                }

                if (e.Key == Key.Left)
                {
                    if (index > 0)
                    {
                        box.Child = controls.ElementAt(--index);
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

                    Control c = box.Child as Control;
                    box.Child = null;
                    foreach (UserControl control in controls)
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
          //  t.index = copy.index = slides.Count;
            panel.Children.Add(t);
          //  slides.Add(copy);
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
       //     t.index = copy.index = slides.Count;
            panel.Children.Add(t);
         //   slides.Add(copy);
        }

        private void add_picture_slide(object sender, RoutedEventArgs e)
        {
            PictureSlide p = new PictureSlide();
            PictureSlide copy = new PictureSlide();
            p.c.Background = get_brush();
            copy.c.Background = p.c.Background;
            p.textbox.Background = p.c.Background;
            copy.textbox.Background = p.c.Background;
          //  p.index = copy.index = slides.Count;
            panel.Children.Add(p);
           // slides.Add(copy);
        }

        private void add_slide(object sender, RoutedEventArgs e)
        {
            Base b = new Base();
            Base copy = new Base();
            b.c.Background = get_brush();
            copy.c.Background = b.c.Background;
            panel.Children.Add(b);
            //  slides.Add(copy);
        }

    }
}
