// file:    Commands\UI\CreateFormListViewDataGridFromSelectedClassCommand.cs
//
// summary: Implements the create form list view data grid from selected class command class
using System;
using System.ComponentModel.Design;

using EnvDTE;
using EnvDTE80;

using XamlHelpmeet.UI.DynamicForm;
using XamlHelpmeet.UI.Utilities;
using XamlHelpmeet.Utility;

namespace XamlHelpmeet.Commands.UI
{
using NLog;

using YoderZone.Extensions.NLog;

/// <summary>
///     Create form list view data grid from selected class command.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Commands.CommandBase"/>
public class CreateFormListViewDataGridFromSelectedClassCommand :
    CommandBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    /// <summary>
    /// Initializes a new instance of the
    /// CreateFormListViewDataGridFromSelectedClassCommand class.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="id">The id.</param>
    public CreateFormListViewDataGridFromSelectedClassCommand(
        DTE2 application, CommandID id)
    : base(application, id)
    {

        logger.Trace("Entered CreateFormListViewDataGridFromSelectedClassCommand()");
        Caption = "Create Form, ListView or DataGrid From Selected Class";
        CommandName = "CreateFormListViewDataGridFromSelectedClassCommand";
        ToolTip = "Create Form, ListView or DataGrid From Selected Class";
    }

    #region Methods

    /// <summary>
    ///     Executes this CreateFormListViewDataGridFromSelectedClassCommand.
    /// </summary>
    public override void Execute()
    {

        logger.Trace("Entered Execute()");
        try
        {
            var remoteTypeReflector = new RemoteTypeReflector();
            var classEntity = remoteTypeReflector.
                              GetClassEntityFromSelectedClass(Application.
                                      SelectedItems.
                                      Item(1).ProjectItem.
                                      ContainingProject,
                                      Caption);

            if (classEntity == null)
            { return; }

            var obj = new CreateBusinessFormFromClassWindow(classEntity);
            var result = obj.ShowDialog();

            if (result ?? false)
            {
                try
                {
                    System.Windows.Clipboard.Clear();
                    System.Windows.Clipboard.SetText(obj.BusinessForm);
                }
                catch //(Exception ex)
                {
                    // Had to do this to avoid useless exception message
                    // when running this code in a VPC, since Vista &
                    // VPC and the Clipboard don't play nice together
                    // sometimes.
                    // the operation works, you just get an exception
                    // for no reason.
                }
            }
        }
        catch (Exception ex)
        {
            UIUtilities.ShowExceptionMessage(this.Caption, ex.Message);
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
        if (Application.ActiveDocument == null ||
                !(Application.ActiveDocument.Name.EndsWith("vb") ||
                  Application.ActiveDocument.Name.EndsWith("cs"))
           )
            return vsCommandStatus.vsCommandStatusUnsupported &
                   vsCommandStatus.vsCommandStatusInvisible;

        return vsCommandStatus.vsCommandStatusEnabled |
               vsCommandStatus.vsCommandStatusSupported;
    }

    #endregion Methods
}
}