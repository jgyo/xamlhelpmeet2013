﻿// file:    Commands\NoUI\GroupIntoBorderNoChildRoot.cs
//
// summary: Implements the group into border no child root class
using System;

using EnvDTE80;
using EnvDTE;
using XamlHelpmeet.UI.Utilities;
using System.Globalization;
using System.ComponentModel.Design;
using XamlHelpmeet.Utility;

namespace XamlHelpmeet.Commands.NoUI
{
using NLog;

/// <summary>
///     Group into border no child root.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Commands.CommandBase"/>
public class GroupIntoBorderNoChildRoot : CommandBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the GroupIntoBorderNoChildRoot class.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="id">The id.</param>
    public GroupIntoBorderNoChildRoot(DTE2 application, CommandID id)
    : base(application, id)
    {
        logger.Trace("Entered GroupIntoBorderNoChildRoot()");

        Caption = "Border without Child Root";
        CommandName = "GroupIntoBorderNoChildRoot";
        ToolTip = "Group selection into a border without an additional root child being added.";
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

        return base.CanExecute(executeOption) && !IsTextSelected();
    }

    /// <summary>
    ///     Executes this GroupIntoBorderNoChildRoot.
    /// </summary>
    public override void Execute()
    {
        logger.Trace("Entered Execute()");

        try
        {
            var selectedCodeBlock = Application.ActiveDocument.Selection as
                                    TextSelection;
            if (selectedCodeBlock.SelectNode()==false)
            {
                UIUtilities.ShowExceptionMessage("You must select a control",
                                                 "Your selection must begin and end with both control tags.");
                return;
            }

            GroupInto("<Border>", "</Border>");
        }
        catch (Exception ex)
        {
            UIUtilities.ShowExceptionMessage(String.Format(CultureInfo.CurrentCulture,
                                             "Group Into {0}", Caption), ex.Message);
            logger.Debug("An exception was raised in GroupIntoBorderNoChildRoot::Execute.",
                         ex);
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
        return vsCommandStatus.vsCommandStatusSupported |
               (IsTextSelected() ?
                vsCommandStatus.vsCommandStatusEnabled :
                vsCommandStatus.vsCommandStatusUnsupported);
    }

    #endregion

}
}
