using System.Collections.ObjectModel;
using System.Windows;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Common.Validators.VariableNameValidatorView>;
namespace ProceduralWorldGenerator.Common.Validators
{
    public class VariableNameValidatorView : DependencyObject
    {
        public static readonly DependencyProperty BlackListProperty = 
            RegisterProperty(x => x.BlackList);
        public ObservableCollection<string> BlackList {
            get => (ObservableCollection<string>)GetValue(BlackListProperty);
            set => SetValue(BlackListProperty, value);
        }
    }
}