using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProceduralWorldGenerator.Common.Controls
{
    public class TabItemEx : TabItem
    {
        static TabItemEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItemEx),
                new FrameworkPropertyMetadata(typeof(TabItemEx)));
        }

        public ICommand CloseTabCommand
        {
            get => (ICommand)GetValue(CloseTabCommandProperty);
            set => SetValue(CloseTabCommandProperty, value);
        }

        public object CloseTabCommandParameter
        {
            get => (object)GetValue(CloseTabCommandParameterProperty);
            set => SetValue(CloseTabCommandParameterProperty, value);
        }

        public static readonly DependencyProperty CloseTabCommandProperty =
            DependencyProperty.Register(nameof(CloseTabCommand), typeof(ICommand), typeof(TabItemEx),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CloseTabCommandParameterProperty =
            DependencyProperty.Register(nameof(CloseTabCommandParameter), typeof(object), typeof(TabItemEx),
                new PropertyMetadata(null));
    }
}