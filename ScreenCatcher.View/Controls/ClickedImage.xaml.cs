using System.Windows;
using System.Windows.Input;

namespace ScreenCatcher.View.Controls
{
    public partial class ClickedImage
    {
        public ClickedImage()
        {
            InitializeComponent();

            Subscribe();
        }

        private void Subscribe()
        {
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseLeave += OnMouseLeave;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            IsPressed = true;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (IsPressed)
                ClickCommand.Execute(null);
            IsPressed = false;
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            IsPressed = false;
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(ClickedImage));

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(ClickedImage));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(ClickedImage));

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }
    }
}