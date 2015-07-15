﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;

namespace IntraLattice
{
    public class InputTools
    {
        // index represents the input position (first input is index == 0)
        public static void TopoSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
        {
            //instantiate  new value list
            var vallist = new Grasshopper.Kernel.Special.GH_ValueList();
            vallist.ListMode = Grasshopper.Kernel.Special.GH_ValueListMode.Cycle;
            vallist.CreateAttributes();

            //customise value list position
            float xCoord = (float)Component.Attributes.Pivot.X - 200;
            float yCoord = (float)Component.Attributes.Pivot.Y + index * 40 - offset;
            PointF cornerPt = new PointF(xCoord, yCoord);
            vallist.Attributes.Pivot = cornerPt;

            //populate value list with our own data
            vallist.ListItems.Clear();

            var item1 = new Grasshopper.Kernel.Special.GH_ValueListItem("Grid", "0");
            var item2 = new Grasshopper.Kernel.Special.GH_ValueListItem("X", "1");
            var item3 = new Grasshopper.Kernel.Special.GH_ValueListItem("Star", "2");
            var item4 = new Grasshopper.Kernel.Special.GH_ValueListItem("Cross", "3");
            var item5 = new Grasshopper.Kernel.Special.GH_ValueListItem("Cross2", "4");
            var item6 = new Grasshopper.Kernel.Special.GH_ValueListItem("Vintiles", "5");
            var item7 = new Grasshopper.Kernel.Special.GH_ValueListItem("Octahedral", "6");

            vallist.ListItems.Add(item1);
            vallist.ListItems.Add(item2);
            vallist.ListItems.Add(item3);
            vallist.ListItems.Add(item4);
            vallist.ListItems.Add(item5);
            vallist.ListItems.Add(item6);
            vallist.ListItems.Add(item7);
            vallist.ListItems.Add(item8);

            var items = new List<Grasshopper.Kernel.Special.GH_ValueListItem>();
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Grid", "0"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("X", "1"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Star", "2"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Cross", "3"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Cross2", "4"));

            vallist.ListItems.AddRange(items);

            // Until now, the slider is a hypothetical object.
            // This command makes it 'real' and adds it to the canvas.
            GrasshopperDocument.AddObject(vallist, false);

            //Connect the new slider to this component
            Component.Params.Input[index].AddSource(vallist);
            Component.Params.Input[index].CollectData();
        }

        // index represents the input position (first input is index == 0)
        public static void GradientSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
        {
            //instantiate  new value list
            var vallist = new Grasshopper.Kernel.Special.GH_ValueList();
            vallist.ListMode = Grasshopper.Kernel.Special.GH_ValueListMode.DropDown;
            vallist.CreateAttributes();

            //customise value list position
            float xCoord = (float)Component.Attributes.Pivot.X - 200;
            float yCoord = (float)Component.Attributes.Pivot.Y + index * 40 - offset;
            PointF cornerPt = new PointF(xCoord, yCoord);
            vallist.Attributes.Pivot = cornerPt;

            //populate value list with our own data
            vallist.ListItems.Clear();
            var items = new List<Grasshopper.Kernel.Special.GH_ValueListItem>();
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Linear (X)", "0"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Linear (Y)", "1"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Linear (Z)", "2"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Centered (X)", "3"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Centered (Y)", "4"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Centered (Z)", "5"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Cylindrical (X)", "6"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Cylindrical (Y)", "7"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Cylindrical (Z)", "8"));
            items.Add(new Grasshopper.Kernel.Special.GH_ValueListItem("Spherical", "9"));

            vallist.ListItems.AddRange(items);

            // Until now, the slider is a hypothetical object.
            // This command makes it 'real' and adds it to the canvas.
            GrasshopperDocument.AddObject(vallist, false);

            //Connect the new slider to this component
            Component.Params.Input[index].AddSource(vallist);
            Component.Params.Input[index].CollectData();
        }

        /*public static void BooleanSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
        {
            //instantiate  new value list
            var boollist = new Grasshopper.Kernel.Special.GH_BooleanToggle();
            boollist.CreateAttributes();

            //customise value list position
            float xCoord = (float)Component.Attributes.Pivot.X - 200;
            float yCoord = (float)Component.Attributes.Pivot.Y + index*40 - offset;
            PointF cornerPt = new PointF(xCoord, yCoord);
            boollist.Attributes.Pivot = cornerPt;
            
            // Until now, the slider is a hypothetical object.
            // This command makes it 'real' and adds it to the canvas.
            GrasshopperDocument.AddObject(boollist, false);
            //Connect the new slider to this component
            Component.Params.Input[index].AddSource(boollist);
            Component.Params.Input[index].CollectData();
            // Little hack, required because of how booleantoggle is rendered
            boollist.ExpireSolution(true);
        }

        public static void NumberSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset, int min, int max, bool isFloat)
        {
            //instantiate  new value list
            var numberSlider = new Grasshopper.Kernel.Special.GH_NumberSlider();
            numberSlider.Slider.Minimum = min;
            numberSlider.Slider.Maximum = max;
            if (isFloat)
                numberSlider.Slider.Type = Grasshopper.GUI.Base.GH_SliderAccuracy.Integer;
            else
                numberSlider.Slider.Type = Grasshopper.GUI.Base.GH_SliderAccuracy.Float;
            numberSlider.CreateAttributes();

            //customise value list position
            float xCoord = (float)Component.Attributes.Pivot.X - 200;
            float yCoord = (float)Component.Attributes.Pivot.Y + index * 40;
            PointF cornerPt = new PointF(xCoord, yCoord);
            numberSlider.Attributes.Pivot = cornerPt;

            // Until now, the slider is a hypothetical object.
            // This command makes it 'real' and adds it to the canvas.
            GrasshopperDocument.AddObject(numberSlider, false);

            //Connect the new slider to this component
            Component.Params.Input[index].AddSource(numberSlider);
            Component.Params.Input[index].CollectData();
        }*/


    }
}