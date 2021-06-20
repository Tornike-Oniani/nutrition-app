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
    /// Interaction logic for CircularProgress.xaml
    /// </summary>
    public partial class CircularProgress : UserControl
    {
        private static readonly DependencyProperty ProgressProperty =
    DependencyProperty.Register("Progress", typeof(double), typeof(CircularProgress), new PropertyMetadata(0.0));

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        private static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(CircularProgress), new PropertyMetadata(null));

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(int), typeof(CircularProgress), new PropertyMetadata(15));

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public int Angle 
        { 
            get 
            { 
                if (Progress > 100) { return 360; }

                return (int)(Progress * 360 / 100); 
            } 
        }

        public int InsideThickness { get { return (int)Math.Floor((double)(Thickness / 2)); } }
        public double InsideWidth { get { return this.ActualWidth * 70 / 100; } }
        public double InsideHeight { get { return this.ActualHeight * 70 / 100; } }

        public CircularProgress()
        {
            InitializeComponent();
        }
    }
}
