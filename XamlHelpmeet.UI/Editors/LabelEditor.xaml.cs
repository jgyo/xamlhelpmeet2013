using System.Windows.Controls;

namespace XamlHelpmeet.UI.Editors
{
using NLog;

/// <summary>
/// Interaction logic for LabelEditor.xaml
/// </summary>
public partial class LabelEditor : UserControl
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    public LabelEditor()
    {
        logger.Trace("Entered LabelEditor()");

        InitializeComponent();
    }
}
}