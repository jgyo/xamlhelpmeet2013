using System.Windows.Controls;

namespace XamlHelpmeet.UI.Editors
{
using NLog;

/// <summary>
/// Interaction logic for DynamicFormDatePickerEditor.xaml
/// </summary>
public partial class DynamicFormDatePickerEditor : UserControl
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    public DynamicFormDatePickerEditor()
    {
        logger.Trace("Entered DynamicFormDatePickerEditor()");

        InitializeComponent();
    }
}
}