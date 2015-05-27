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
using System.Windows.Media.Animation;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int index = 0;
        private List<UserControl> controls = new List<UserControl>();
        //private List<Control> slides = new List<Control>();
        private bool in_presentation = false;
        private Random r = new Random();
        DoubleAnimation move = new DoubleAnimation();
        public MainWindow()
        {
            InitializeComponent();
            move.From = 0;
            move.To = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            move.Duration= new Duration(TimeSpan.FromSeconds(1));
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
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
            this.Focus();
                if (panel.Children.Count==0)
                    MessageBox.Show(this,"error","error", MessageBoxButton.OK , MessageBoxImage.Error);
                else 
                {
                    this.Topmost = true;
                    this.WindowStyle = System.Windows.WindowStyle.None;
                    this.WindowState = System.Windows.WindowState.Maximized;
                    this.Cursor = Cursors.None;
                    ////////////////////////////
                    controls.Clear();
                    foreach (UserControl c in panel.Children)
                    {

                        if (c is PictureSlide)
                        {
                            PictureSlide p = c as PictureSlide;
                            p.removeframes();
                            controls.Add(p);
                            p.IsHitTestVisible = false;
                        }
                        if (c is TextSlide)
                        {
                            TextSlide t = c as TextSlide;
                            t.IsHitTestVisible = false;
                            t.removeframes();
                            controls.Add(t);
                        }
                        if (c is Title_slide)
                        {
                            Title_slide t = c as Title_slide;
                            t.removeframes();
                            t.IsHitTestVisible = false;
                            controls.Add(t);
                        }
                        if (c is Base)
                        {
                            controls.Add(c);
                            c.IsHitTestVisible = false;
                        }
                    }

                    panel.Children.Clear();
                    if (!(controls.Count == 0))
                    {
                        box.Child = controls.ElementAt(0);
                        box.Visibility = Visibility.Visible;
                        border.Visibility = Visibility.Visible;
                    }
                }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (in_presentation)
            {
                box.Focus();

                if (e.Key == Key.Right)
                {
                    if (index < controls.Count - 1)
                    {
                        box.Child = controls.ElementAt(++index);
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
                        {
                            if (control is PictureSlide)
                            {
                                PictureSlide p = control as PictureSlide;
                                p.addframes();
                                panel.Children.Add(p);
                                p.IsHitTestVisible = true;
                            }
                            if (control is TextSlide)
                            {
                                TextSlide t = control as TextSlide;
                                t.IsHitTestVisible = true;
                                t.addframes();
                                panel.Children.Add(t);
                            }
                            if (control is Title_slide)
                            {
                                Title_slide t = control as Title_slide;
                                t.addframes();
                                t.IsHitTestVisible = true;
                                panel.Children.Add(t);
                            }
                            if (control is Base)
                            {
                                panel.Children.Add(control);
                                control.IsHitTestVisible = true;
                            }
                        }
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
                    {
                        if (control is PictureSlide)
                        {
                            PictureSlide p = control as PictureSlide;
                            p.addframes();
                            panel.Children.Add(p);
                            p.IsHitTestVisible = true;
                        }
                        if (control is TextSlide)
                        {
                            TextSlide t = control as TextSlide;
                            t.IsHitTestVisible = true;
                            t.addframes();
                            panel.Children.Add(t);
                        }
                        if (control is Title_slide)
                        {
                            Title_slide t = control as Title_slide;
                            t.addframes();
                            t.IsHitTestVisible = true;
                            panel.Children.Add(t);
                        }
                        if (control is Base)
                        {
                            panel.Children.Add(control);
                            control.IsHitTestVisible = true;
                        }
                    }
                }  
            
            }
        }

        private void add_text_slide (object sender, RoutedEventArgs e)
        {
            TextSlide t = new TextSlide();
            t.c.Background = get_brush();
            t.text_box.Background = t.c.Background;
            t.title_box.Background = t.c.Background;
            panel.Children.Add(t);
        }

        private void add_title_slide(object sender, RoutedEventArgs e)
        {
            Title_slide t = new Title_slide();
            t.c.Background = get_brush();
            t.subtitle_box.Background = t.c.Background;
            t.title_box.Background = t.c.Background;
            panel.Children.Add(t);
        }

        private void add_picture_slide(object sender, RoutedEventArgs e)
        {
            PictureSlide p = new PictureSlide();
            p.c.Background = get_brush();
            p.textbox.Background = p.c.Background;
            panel.Children.Add(p);
        }

        private void add_slide(object sender, RoutedEventArgs e)
        {
            Base b = new Base();
            b.c.Background = get_brush();
            panel.Children.Add(b);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (in_presentation)
            {
                box.Focus();
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (index < controls.Count - 1)
                    {
                        box.Child = controls.ElementAt(++index);
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
                        {
                            if (control is PictureSlide)
                            {
                                PictureSlide p = control as PictureSlide;
                                p.addframes();
                                panel.Children.Add(p);
                                p.IsHitTestVisible = true;
                            }
                            if (control is TextSlide)
                            {
                                TextSlide t = control as TextSlide;
                                t.IsHitTestVisible = true;
                                t.addframes();
                                panel.Children.Add(t);
                            }
                            if (control is Title_slide)
                            {
                                Title_slide t = control as Title_slide;
                                t.addframes();
                                t.IsHitTestVisible = true;
                                panel.Children.Add(t);
                            }
                            if (control is Base)
                            {
                                panel.Children.Add(control);
                                control.IsHitTestVisible = true;
                            }
                        }
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        box.Child = controls.ElementAt(--index);
                    }
                      
                }


            }

        }


    }
}
