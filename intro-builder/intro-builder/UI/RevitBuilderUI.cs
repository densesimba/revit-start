using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]
    public class RevitBuilderUI : IExternalCommand
    {
        public static string userAddinDirectory;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
          
            TaskDialog.Show("Sall", "Hello world!");
            Form1 fr = new Form1();
            fr.ShowDialog();
            return Result.Succeeded;
        }

        public static void AddUiButtons(UIControlledApplication app)
        {
            AddNewTab("Intro", app);
            RibbonPanel panel = AddNewPanel("Intro","Panelul Denisei",app);

            _ = AddNewButton("Apasa ", typeof(RevitBuilderUI).FullName,panel );


        }

        /// <summary>
        /// Add a new Tab in Revit menu.
        /// </summary>
        /// <param name="tabName">The name of the tab which we want to add.</param>
        /// <param name="app">The name of the current app.</param>
        public static void AddNewTab(string tabName, UIControlledApplication app)
        {
            app.CreateRibbonTab(tabName);
        }
        public static PushButtonData AddNewButton(string buttonName, string className, RibbonPanel panelName)
        {
            PushButtonData button = new PushButtonData(buttonName, buttonName, System.Reflection.Assembly.GetExecutingAssembly().Location, className);
            panelName.AddItem(button);

            return button;
        }
        public static RibbonPanel AddNewPanel(string tabName, string panelName, UIControlledApplication app)
        {
           userAddinDirectory = app.ControlledApplication.CurrentUserAddinsLocation;
            RibbonPanel panel = app.CreateRibbonPanel(tabName, panelName);

            return panel;
        }



    }
}
