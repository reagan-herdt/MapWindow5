﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregateShapesTool.cs" company="MapWindow OSS Team - www.mapwindow.org">
//   MapWindow OSS Team - 2015
// </copyright>
// <summary>
//   Defines the AggregateShapes tool.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using MW5.Plugins.Enums;
using MW5.Plugins.Interfaces;
using MW5.Tools.Model;

namespace MW5.Tools.Tools.Geoprocessing.VectorGeometryTools
{
    [GisTool(GroupKeys.VectorGeometryTools)]
    public class AggregateShapesTool : GisTool
    {
        [Input("Field index", 1)]
        [DefaultValue(1)]
        public int FieldIndex { get; set; }

        [Input("Input layer", 0)]
        public VectorLayerInfo InputLayer { get; set; }

        [Input("Save results as", 4)]
        [DefaultValue(@"d:\aggregate.shp")]
        public OutputLayerInfo Output { get; set; }

        /// <summary>
        /// Gets name of the tool.
        /// </summary>
        public override string Name
        {
            get { return "Aggregate shapes by attribute"; }
        }

        /// <summary>
        /// AggregateShapes doesn't supports cancelling in the ocx
        /// </summary>
        public override bool SupportsCancel
        {
            get { return false; }
        }

        /// <summary>
        /// Gets description of the tool.
        /// </summary>
        public override string Description
        {
            get { return "Creates a new shapefile by creating multi-part shapes from shapes with the same value of specified attribute."; }
        }

        /// <summary>
        /// Provide execution logic for the tool.
        /// </summary>
        public override bool Run(ITaskHandle task)
        {
            var fs = InputLayer.Datasource.AggregateShapes(InputLayer.SelectedOnly, FieldIndex);

            if (fs != null)
            {
                HandleOutput(fs, Output);

                return true;
            }

            return false;
        }
    }
}