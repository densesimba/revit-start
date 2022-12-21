using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

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
            return Result.Succeeded;
        }
        public static void AddUiButtons(UIControlledApplication app)
        {
            AddNewTab("Intro", app);
            RibbonPanel panel = AddNewPanel("Intro", "Panelul Denisei", app);

            _ = AddNewButton("Creare pereți", typeof(CreateWalls).FullName, panel);
            _ = AddNewButton("creare finisaj", typeof(CreateFinish).FullName, panel);
            _ = AddNewButton("Creare podea", typeof(CreateFloor).FullName, panel);
            _ = AddNewButton("Join pereți&finisaje", typeof(Join).FullName, panel);
            _ = AddNewButton("Creare uși/ferestre", typeof(CreateDoorsWindows).FullName, panel);
            _ = AddNewButton("Creare tavan", typeof(CreateCeiling).FullName, panel);
            _ = AddNewButton("Creare șarpantă", typeof(CreateRoof).FullName, panel);
            _ = AddNewButton("Creare terasă", typeof(CreateFlatRoof).FullName, panel);

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

