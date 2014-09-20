// file:    Commands\NoUI\GroupIntoViewBox.cs
//
// summary: Implements the group into view box class
using System;

using EnvDTE80;
using EnvDTE;
using XamlHelpmeet.UI.Utilities;
using System.ComponentModel.Design;

namespace XamlHelpmeet.Commands.NoUI
{
using NLog;

/// <summary>
///     Group into view box.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Commands.CommandBase"/>
public class GroupIntoViewBox : CommandBase
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the GroupIntoViewBox class.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="id">The id.</param>
    public GroupIntoViewBox(DTE2 application, CommandID id)
    : base(application, id)
    {

        logger.Trace("Entered GroupIntoViewBox()");
        Caption = "Viewbox";
        CommandName = "GroupIntoViewBox";
        ToolTip = "Group selection into a Viewbox.";
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Determine if we can execute.
    /// </summary>
    /// <param name="executeOption">
    ///     The execute option.
    /// </param>
    /// <returns>
    ///     true if we can execute, otherwise false.
    /// </returns>
    public override bool CanExecute(vsCommandExecOption executeOption)
    {

        logger.Trace("Entered CanExecute()");
        return base.CanExecute(executeOption) && IsTextSelected();
    }

    /// <summary>
    ///     Executes this GroupIntoViewBox.
    /// </summary>
    public override void Execute()
    {

        logger.Trace("Entered Execute()");
        try
        {
            GroupInto("<Viewbox>\r\n", "</Viewbox>\r\n");
        }
        catch (Exception ex)
        {
            UIUtilities.ShowExceptionMessage("Group Into " + this.Caption, ex.Message);
            logger.Error("An exception was raised in Execute().", ex);
        }
    }

    /// <summary>
    ///     Gets the status.
    /// </summary>
    /// <returns>
    ///     The status.
    /// </returns>
    public override vsCommandStatus GetStatus()
    {

        logger.Trace("Entered GetStatus()");
        // This will add vsCommandStatusEnabled to vsCommandStatusSupported,
        // if IsTextSelected() returns true. Otherwise or'ing with
        // vsCommandStatusUnsupported leaves vsCommandStatusSupported
        // unchanged.
        // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
        return vsCommandStatus.vsCommandStatusSupported | (IsTextSelected() ?
                vsCommandStatus.vsCommandStatusEnabled :
                vsCommandStatus.vsCommandStatusUnsupported);
    }

    #endregion

}
}

