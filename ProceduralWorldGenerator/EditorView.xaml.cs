﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Nodify;
using ProceduralWorldGenerator.ViewModels;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator
{
    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseLeftButtonDownEvent, new MouseButtonEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(ItemContainer), ItemContainer.DragStartedEvent, new RoutedEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseRightButtonUpEvent, new MouseButtonEventHandler(OpenOperationsMenu));
        }

        private void OpenOperationsMenu(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && e.OriginalSource is NodifyEditor editor && !editor.IsPanning && editor.DataContext is GeneratorViewModel calculator)
            {
                e.Handled = true;
                calculator.OperationsMenu.OpenAt(editor.MouseLocation);
            }
        }

        private void CloseOperationsMenu(object sender, RoutedEventArgs e)
        {
            var itemContainer = sender as ItemContainer;
            var editor = sender as NodifyEditor ?? itemContainer?.Editor;

            if (!e.Handled && editor?.DataContext is GeneratorViewModel calculator)
            {
                calculator.OperationsMenu.Close();
            }
        }
        
        private void OnDropNode(object sender, DragEventArgs e)
        {
            if(e.Source is NodifyEditor editor && editor.DataContext is GeneratorViewModel calculator
                && e.Data.GetData(typeof(NodeViewModelBase)) is NodeViewModelBase operation)
            {
                var location = editor.GetLocationInsideEditor(e);
                calculator.CreateDimensionOperationMenu.OperationViewModelProvider =
                    () => NodePreviewProvider.CreateNodeViewModel(operation);
                calculator.CreateDimensionOperationMenu.OpenAt(location);

                e.Handled = true;
            }
        }

        private void OnNodeDrag(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && ((FrameworkElement)sender).DataContext is NodeViewModelBase operation)
            { 
                var data = new DataObject(typeof(NodeViewModelBase), operation);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }
    }
}
