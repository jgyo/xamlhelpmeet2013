﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XamlHelpmeet.UI.CreateBusinessForm;

namespace XamlHelpmeet.UI.Editors
{
using NLog;

using YoderZone.Extensions.NLog;

/// <summary>
/// Interaction logic for ImageEditor.xaml
/// </summary>
public partial class ImageEditor : UserControl
{
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    public ImageEditor()
    {
        logger.Trace("Entered ImageEditor()");

        InitializeComponent();
    }

    private void ImageEditor_Loaded(object sender, RoutedEventArgs e)
    {
        logger.Trace("Entered ImageEditor_Loaded()");

        var binding = new Binding()
        {
            Path = new PropertyPath("BindingPath"),
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };

        if (CreateBusinessFormWindow.ClassEntity == null ||
                CreateBusinessFormWindow.ClassEntity.PropertyInformation.Count == 0)
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