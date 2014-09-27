using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XamlHelpmeet.UI.Utilities;

namespace XamlHelpmeet.UI.Editors
{
using NLog;

using YoderZone.Extensions.NLog;

/// <summary>
/// Interaction logic for DynamicFormComboBoxEditor.xaml
/// </summary>
public partial class DynamicFormComboBoxEditor : UserControl
{
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    public DynamicFormComboBoxEditor()
    {
        logger.Trace("Entered DynamicFormComboBoxEditor()");

        InitializeComponent();
    }

    private void cboBindingMode_Loaded(object sender, RoutedEventArgs e)
    {
        logger.Trace("Entered cboBindingMode_Loaded()");

        var cbo = sender as ComboBox;
        if (cbo.ItemsSource != null)
        {
            return;
        }
        cbo.ItemsSource = UIHelpers.GetSortedEnumNames(typeof(BindingMode));
    }
}
}
