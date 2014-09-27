// file:    Commands\UI\ControlDefaultsCommand.cs
//
// summary: Implements the control defaults command class

using EnvDTE80;

using XamlHelpmeet.UI;
using System.ComponentModel.Design;

namespace XamlHelpmeet.Commands.UI
{
using NLog;

using YoderZone.Extensions.NLog;

/// <summary>
///     Control defaults command.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Commands.CommandBase"/>
public class ControlDefaultsCommand : CommandBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the ControlDefaultsCommand class.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="id">The id.</param>
    public ControlDefaultsCommand(DTE2 application, CommandID id)
    : base(application, id)
    {

        logger.Trace("Entered ControlDefaultsCommand()");
        Caption = "Set Control Defaults";
        CommandName = "SetControlDefaultsCommand";
        ToolTip = "Set control defaults for controls created by this software.";
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Executes this ControlDefaultsCommand.
    /// </summary>
    public override void Execute()
    {

        logger.Trace("Entered Execute()");
        var obj = new UIControlDefaultsWindow();
        obj.ShowDialog();
        obj = null;
    }

    #endregion
}
}
