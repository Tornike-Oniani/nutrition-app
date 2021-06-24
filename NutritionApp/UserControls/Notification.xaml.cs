using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NutritionApp.UserControls
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        private static readonly DependencyProperty ColorProperty =
    DependencyProperty.Register("Color", typeof(Brush), typeof(Notification), new PropertyMetadata(null));

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static readonly DependencyProperty MessageProperty =
    DependencyProperty.Register("Message", typeof(string), typeof(Notification), new PropertyMetadata(null));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private static readonly DependencyProperty CommandProperty =
    DependencyProperty.Register("Command", typeof(ICommand), typeof(Notification), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public Notification()
        {
            InitializeComponent();
        }
    }
}
