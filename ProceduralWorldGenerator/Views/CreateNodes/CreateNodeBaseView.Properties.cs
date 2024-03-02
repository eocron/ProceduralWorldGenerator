﻿using System.Windows;
using System.Windows.Input;

namespace ProceduralWorldGenerator.Views.CreateNodes
{
    public partial class  CreateNodeBaseView
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(CreateNodeBaseView), new PropertyMetadata(null));
        public static readonly DependencyProperty OkButtonCommandProperty = DependencyProperty.Register(nameof(OkButtonCommand), typeof(ICommand), typeof(CreateNodeBaseView), new PropertyMetadata(null));
        public static readonly DependencyProperty CancelButtonCommandProperty = DependencyProperty.Register(nameof(CancelButtonCommand), typeof(ICommand), typeof(CreateNodeBaseView), new PropertyMetadata(null));
        
        public string Title        
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public ICommand OkButtonCommand         
        {
            get => (ICommand)GetValue(OkButtonCommandProperty);
            set => SetValue(OkButtonCommandProperty, value);
        }
        
        public ICommand CancelButtonCommand         
        {
            get => (ICommand)GetValue(CancelButtonCommandProperty);
            set => SetValue(CancelButtonCommandProperty, value);
        }
    }
}