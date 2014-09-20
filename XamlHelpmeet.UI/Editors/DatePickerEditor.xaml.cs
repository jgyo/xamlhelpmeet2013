﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XamlHelpmeet.UI.CreateBusinessForm;

namespace XamlHelpmeet.UI.Editors
{
using NLog;

/// <summary>
/// Interaction logic for DatePickerEditor.xaml
/// </summary>
public partial class DatePickerEditor : UserControl
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    public DatePickerEditor()
    {
        logger.Trace("Entered DatePickerEditor()");

        InitializeComponent();
    }

    private void DatePickerEditor_Loaded(object sender, RoutedEventArgs e)
    {
        logger.Trace("Entered DatePickerEditor_Loaded()");

        var binding = new Binding();
        binding.Path = new PropertyPath("BindingPath");
        binding.Mode = BindingMode.TwoWay;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        if (CreateBusinessFormWindow.ClassEntity == null ||
                CreateBusinessFormWindow.ClassEntity.PropertyInformation.Count==0)
        {
            txtBindingPath.Visibility = System.Windows.Visibility.Visible;
            cboBindingPath.Visibility = System.Windows.Visibility.Collapsed;
            txtBindingPath.SetBinding(TextBox.TextProperty, binding);
        }
        else
        {
            txtBindingPath.Visibility = System.Windows.Visibility.Collapsed;
            cboBindingPath.Visibility = System.Windows.Visibility.Visible;
            cboBindingPath.SetBinding(ComboBox.SelectedValueProperty, binding);
            cboBindingPath.ItemsSource =
                CreateBusinessFormWindow.ClassEntity.PropertyInformation;
        }
    }
}
}
