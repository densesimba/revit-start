using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;

namespace eConFaire.RevitBuilder.Intro.UI
{
    [Transaction(TransactionMode.Manual)]
    public class Join : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            using (Transaction tr = new Transaction(document, "creare Joins"))
            {
                tr.Start();

                if ( !(App.joinPair[0].FinishesId.Count == 0))
                {
                    JoinFunction(document);
                }
                else
                {
                    TaskDialog.Show("Eroare Join Parter", " Nu sunt adaugati");
                }

                if (!(App.joinPair[0].FinishIdTerasa.Count == 0 ))
                {
                    JoinFlatInterior(document);
                    JoinFlatExterior(document);
                }
                else
                {
                    TaskDialog.Show(" Eroare Join Terasa", " Terasa nu este Adaugata");
                }

                tr.Commit();
            }
            return Result.Succeeded;
        }


        //public void JoinTerasaExteriori(Document document)
        //{
        //    Wall nextwall;
        //    int i = 0;
        //    foreach (var pExteriorTerasa in App.TerasaWallsExterior)
        //    {

        //        if (i == 4)
        //        {
        //            nextwall = App.ExteriorWalls[0];
        //        }
        //        else
        //        {
        //            nextwall = App.ExteriorWalls[i];
        //        }

        //        if (!JoinGeometryUtils.AreElementsJoined(document, pExteriorTerasa, nextwall))
        //        {
        //            try
        //            {
        //                JoinGeometryUtils.JoinGeometry(document, pExteriorTerasa, nextwall);
        //            }
        //            catch (Exception e)
        //            {

        //                Debug.WriteLine($"prob la TERASA exterior {e} ");
        //                Debug.WriteLine("\nNO SE PUEDE\n");
        //            }
        //        }
        //        i++;
        //    }
        //}

        public void JoinFlatExterior(Document document)
        {
            foreach (var pair in App.joinPair)
            {
                var ExtFinishFlat = document.GetElement(pair.FinishIdTerasa[1]) as Wall;
                var ExtFinishRoom = document.GetElement(pair.FinishesId[1]);
                if (ExtFinishFlat != null
                    && ExtFinishRoom != null
                    && !JoinGeometryUtils.AreElementsJoined(document, ExtFinishFlat, ExtFinishRoom))
                {
                    JoinGeometryUtils.JoinGeometry(document,  ExtFinishFlat, ExtFinishRoom);
                    Debug.WriteLine("inish terasa EXTERIOR join completed");
                }
            }
        }

        public void JoinFlatInterior(Document document)
        {
            int i = 1;
            Wall nextFinish;
            foreach (var pair in App.joinPair)
            {
                
                var intFinish = document.GetElement(pair.FinishIdTerasa[0]) as Wall;
                if (i == 4)
                {
                     nextFinish = document.GetElement(App.joinPair[0].FinishIdTerasa[0]) as Wall;
                }
                else
                {
                    nextFinish = document.GetElement(App.joinPair[i].FinishIdTerasa[0]) as Wall;
                }
               

                if (intFinish != null && nextFinish != null
                    && !JoinGeometryUtils.AreElementsJoined(document, intFinish, nextFinish))
                {
                    try
                    {
                        JoinGeometryUtils.JoinGeometry(document, intFinish, nextFinish);
                        Debug.WriteLine("\nfinish terasa INTERIOR join completed");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"prob la finish terasa INTERIOR {e} ");

                    }
                    i++;
                }
            }
        }
        //public void JoinTerasaInteriori(Document document)
        //{

        //    Wall nextwall;
        //    int i = 0;



        //    foreach (var val in App.TerasaWallsInterior)
        //    {
        //        var currentvalue = App.TerasaWallsInterior[i];
        //        i++;
        //        if (i == 4)
        //        {
        //            nextwall = App.TerasaWallsInterior[0];
        //        }
        //        else
        //        {
        //            nextwall = App.TerasaWallsInterior[i];
        //        }


        //        if (!JoinGeometryUtils.AreElementsJoined(document, currentvalue, nextwall))
        //        {
        //            try
        //            {
        //                JoinGeometryUtils.JoinGeometry(document, currentvalue, nextwall);
        //                Debug.WriteLine("terasa intfinish join completed");
        //            }
        //            catch (Exception e)
        //            {

        //                Debug.WriteLine($"prob la exterior {e} ");
        //                Debug.WriteLine("NO SE PUEDE");
        //            }
        //        }

        //    }
        //}

        public void JoinFunction(Document document)
        {
            foreach (var pair in App.joinPair)
            {
                var currentWall = document.GetElement(pair.WallId) as Wall;
                var intFinish = document.GetElement(pair.FinishesId[0]) as Wall;
                var extFinish = document.GetElement(pair.FinishesId[1]) as Wall;

                if (currentWall != null
                    && intFinish != null
                    && !JoinGeometryUtils.AreElementsJoined(document, currentWall, intFinish))
                {
                    JoinGeometryUtils.JoinGeometry(document, currentWall, intFinish);
                    Debug.WriteLine("intFinnish join completed");
                }
                if (currentWall != null
                    && extFinish != null
                    && !JoinGeometryUtils.AreElementsJoined(document, currentWall, extFinish))
                {
                    try
                    {
                        JoinGeometryUtils.JoinGeometry(document, currentWall, extFinish);
                        Debug.WriteLine("extFinnish join completed");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"prob la exterior {e} ");
                    }
                }
            }
        }
    }
}

