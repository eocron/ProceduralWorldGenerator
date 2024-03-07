using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Nodify;
using ProceduralWorldGenerator.ViewModels;

namespace ProceduralWorldGenerator.Views
{
    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseLeftButtonDownEvent,
                new MouseButtonEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(ItemContainer), ItemContainer.DragStartedEvent,
                new RoutedEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseRightButtonUpEvent,
                new MouseButtonEventHandler(OpenOperationsMenu));
        }

        private void CloseOperationsMenu(object sender, RoutedEventArgs e)
        {
            var itemContainer = sender as ItemContainer;
            var editor = sender as NodifyEditor ?? itemContainer?.Editor;

            if (!e.Handled && editor?.DataContext is GeneratorViewModel calculator) calculator.OperationsMenu.Close();
        }

        private void OnDropNode(object sender, DragEventArgs e)
        {
            if (e.Source is NodifyEditor editor && editor.DataContext is GeneratorViewModel calculator
                                                && e.Data.GetData(typeof(GeneratorPreviewNodeViewModel)) is
                                                    GeneratorPreviewNodeViewModel
                                                    operation)
            {
                var location = editor.GetLocationInsideEditor(e);
                calculator.PendingCreateNodeMenu.Location = location;
                calculator.PendingCreateNodeMenu.Preview = operation;
                calculator.CreateNodeCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void OnNodeDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                ((FrameworkElement)sender).DataContext is GeneratorPreviewNodeViewModel operation)
            {
                var data = new DataObject(typeof(GeneratorPreviewNodeViewModel), operation);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }

        private void OpenOperationsMenu(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && e.OriginalSource is NodifyEditor editor && !editor.IsPanning &&
                editor.DataContext is GeneratorViewModel calculator)
            {
                e.Handled = true;
                calculator.OperationsMenu.OpenAt(editor.MouseLocation);
            }
        }
    }
}