using RPSLS.Classes;
using System;
using System.Windows;

namespace RPSLS.Views
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region ScaleValue Depdency Property

        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            return mainWindow?.OnCoerceScaleValue((double)value) ?? value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            mainWindow?.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {
        }

        public double ScaleValue
        {
            get => (double)GetValue(ScaleValueProperty);
            set => SetValue(ScaleValueProperty, value);
        }

        #endregion ScaleValue Depdency Property

        /// <summary>Calculates the scale for the Window.</summary>
        internal void CalculateScale()
        {
            double yScale = ActualHeight / GameState.CurrentPageHeight;
            double xScale = ActualWidth / GameState.CurrentPageWidth;
            double value = Math.Min(xScale, yScale) * 0.8;
            if (value > 3)
                value = 3;
            else if (value < 1)
                value = 1;
            ScaleValue = (double)OnCoerceScaleValue(WindowMain, value);
        }

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            GameState.MainWindow = this;
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e) => CalculateScale();

        private void MainFrame_OnSizeChanged(object sender, SizeChangedEventArgs e) => CalculateScale();

        #endregion Window-Manipulation Methods
    }
}