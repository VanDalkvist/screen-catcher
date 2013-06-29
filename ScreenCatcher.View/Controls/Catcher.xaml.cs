using System.Windows;

using ScreenCatcher.ViewModel;

namespace ScreenCatcher.View.Controls
{
    public partial class Catcher
    {
        public Catcher()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HadCaughtProperty =
            DependencyProperty.Register("HadCaught", typeof(string), typeof(Catcher), new PropertyMetadata(HadCaughtChanged));
        
        public bool? HadCaught
        {
            get { return (bool?)GetValue(HadCaughtProperty); }
            set { SetValue(HadCaughtProperty, value); }
        }

        private static void HadCaughtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fileName = (string)e.NewValue;
            if (!string.IsNullOrEmpty(fileName))
                Notify(fileName);
        }

        private static void Notify(string fileName)
        {
            new Notification
            {
                DataContext = new NotificationsViewModel(fileName)
            }.Show();
        }
    }
}