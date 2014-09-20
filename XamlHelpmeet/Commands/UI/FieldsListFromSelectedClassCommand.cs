﻿using EnvDTE;
using EnvDTE80;
using System;
using System.ComponentModel.Design;
using XamlHelpmeet.UI;
using XamlHelpmeet.UI.Utilities;
using XamlHelpmeet.Utility;

namespace XamlHelpmeet.Commands.UI
{
using NLog;

/// <summary>
///     Fields list from selected class command.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Commands.CommandBase"/>
public class FieldsListFromSelectedClassCommand : CommandBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the FieldsListFromSelectedClassCommand class.
    /// </summary>
    /// <param name="application">
    ///     The application.
    /// </param>
    /// <param name="id">
    ///     The id.
    /// </param>
    public FieldsListFromSelectedClassCommand(DTE2 application, CommandID id)
    : base(application, id)
    {

        logger.Trace("Entered FieldsListFromSelectedClassCommand()");
        Caption = "Fields List For Selected Class";
        CommandName = "FieldsListFromSelectedClassCommand";
        ToolTip = "Show fields list for selected class";
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Executes this FieldsListFromSelectedClassCommand.
    /// </summary>
    /// <seealso cref="M:XamlHelpmeet.Commands.CommandBase.Execute()"/>
    public override void Execute()
    {

        logger.Trace("Entered Execute()");
        try
        {
            var remoteTypeReflector = new RemoteTypeReflector();
            var classEntity = remoteTypeReflector.GetClassEntityFromSelectedClass(
                                  Application.SelectedItems.Item(1).ProjectItem.ContainingProject, Caption);

            if (classEntity != null && classEntity.Success)
            {
                var obj = new FieldsListWindow(classEntity);
                obj.Topmost = true;
                obj.Show();
            }
        }
        catch (Exception ex)
        {
            UIUtilities.ShowExceptionMessage(this.Caption, ex.Message);
            logger.Error("An exception was raised in Execute().", ex);
        }
    }

    /// <summary>
    ///     Gets the status of the FieldsListFromSelectedClassCommand command.
    /// </summary>
    /// <remarks>
    ///     FieldsListFromSelectedClassCommand is always supported. If text is selected, the
    ///     command is enabled.
    /// </remarks>
    /// <seealso cref="M:XamlHelpmeet.Commands.CommandBase.GetStatus()"/>
    public override vsCommandStatus GetStatus()
    {

        logger.Trace("Entered GetStatus()");
        // This command is supported and always enabled.
        return vsCommandStatus.vsCommandStatusSupported |
               vsCommandStatus.vsCommandStatusEnabled;
    }

    #endregion
}
}