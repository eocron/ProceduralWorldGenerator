﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Common.Controls.Labeled.LabeledComboBoxInput>;
namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledComboBoxInput : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = RegisterProperty(x => x.LabelText).Default("Empty label");
        public string LabelText   {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        
        public static readonly DependencyProperty ItemSourceProperty = RegisterProperty(x => x.ItemSource)
            .UpdateSource(UpdateSourceTrigger.PropertyChanged);
        public IEnumerable ItemSource
        {
            get => (IEnumerable)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        
        public static readonly DependencyProperty SelectedValueProperty = RegisterProperty(x => x.SelectedValue)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged);
        public object SelectedValue
        {
            get => (Enum)GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }


        public LabeledComboBoxInput()
        {
            InitializeComponent();
            Root.DataContext = this;
        }
    }
}