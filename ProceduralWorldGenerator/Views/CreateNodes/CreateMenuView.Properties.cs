using System.Windows;
using System.Windows.Input;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<
    ProceduralWorldGenerator.Views.CreateNodes.CreateMenuView>;

namespace ProceduralWorldGenerator.Views.CreateNodes
{
    public partial class CreateMenuView
    {
        public ICommand CancelButtonCommand
        {
            get => (ICommand)GetValue(CancelButtonCommandProperty);
            set => SetValue(CancelButtonCommandProperty, value);
        }

        public ICommand OkButtonCommand
        {
            get => (ICommand)GetValue(OkButtonCommandProperty);
            set => SetValue(OkButtonCommandProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = RegisterProperty(x => x.Title);
        public static readonly DependencyProperty OkButtonCommandProperty = RegisterProperty(x => x.OkButtonCommand);

        public static readonly DependencyProperty CancelButtonCommandProperty =
            RegisterProperty(x => x.CancelButtonCommand);
    }
}