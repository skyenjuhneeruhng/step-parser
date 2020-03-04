// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace StepParser.Items
{
    public enum StepItemType
    {
        Product,
        ProductDefinition,
        ProductDefinitionShape,
        ProductDefinitionFormationWithSpecifiedSource,
        ShapeDefinitionRepresentation,
        ShapeRepresentationRelationship,
        ShapeRepresentation,
        AdvancedBrepShapeRepresentation,
        ManiFoldSolidBrep,
        ClosedShell,
        AdvancedFace,
        AxisPlacement2D,
        AxisPlacement3D,
        BSplineCurveWithKnots,
        CartesianPoint,
        Circle,
        CylindricalSurface,
        Direction,
        EdgeCurve,
        EdgeLoop,
        Ellipse,
        FaceBound,
        FaceOuterBound,
        Line,
        OrientedEdge,
        Plane,
        Vector,
        VertexPoint,
        StyledItem,
        PresentationStyleAssignment,
        SurfaceStyleUsage,
        SurfaceSideStyle,
        SurfaceStyleFillArea,
        FillAreaStyle,
        FillAreaStyleColour,
        ColourRGB,
        NextAssemblyUsageOccurrence
    }
    
    internal static class StepItemTypeExtensions
    {
        public const string ProductText = "PRODUCT";
        public const string ProductDefinitionText = "PRODUCT_DEFINITION";
        public const string ProductDefinitionShapeText = "PRODUCT_DEFINITION_SHAPE";
        public const string ProductDefinitionFormationWithSpecifiedSourceText = "PRODUCT_DEFINITION_FORMATION_WITH_SPECIFIED_SOURCE";
        public const string ShapeDefinitionRepresentationText = "SHAPE_DEFINITION_REPRESENTATION";
        public const string ShapeRepresentationRelationshipText = "SHAPE_REPRESENTATION_RELATIONSHIP";
        public const string ShapeRepresentationText = "SHAPE_REPRESENTATION";
        public const string AdvancedBrepShapeRepresentationText = "ADVANCED_BREP_SHAPE_REPRESENTATION";
        public const string ManiFoldSolidBrepText = "MANIFOLD_SOLID_BREP";
        public const string ClosedShellText = "CLOSED_SHELL";
        public const string AdvancedFaceText = "ADVANCED_FACE";
        public const string Axis2Placement2DText = "AXIS2_PLACEMENT_2D";
        public const string Axis2Placement3DText = "AXIS2_PLACEMENT_3D";
        public const string BSplineCurveWithKnotsText = "B_SPLINE_CURVE_WITH_KNOTS";
        public const string CartesianPointText = "CARTESIAN_POINT";
        public const string CircleText = "CIRCLE";
        public const string CylindricalSurfaceText = "CYLINDRICAL_SURFACE";
        public const string DirectionText = "DIRECTION";
        public const string EdgeCurveText = "EDGE_CURVE";
        public const string EdgeLoopText = "EDGE_LOOP";
        public const string EllipseText = "ELLIPSE";
        public const string FaceBoundText = "FACE_BOUND";
        public const string FaceOuterBoundText = "FACE_OUTER_BOUND";
        public const string LineText = "LINE";
        public const string OrientedEdgeText = "ORIENTED_EDGE";
        public const string PlaneText = "PLANE";
        public const string VectorText = "VECTOR";
        public const string VertexPointText = "VERTEX_POINT";
        public const string StyledItemText = "STYLED_ITEM";
        public const string PresentationStyleAssignmentText = "PRESENTATION_STYLE_ASSIGNMENT";
        public const string SurfaceStyleUsageText = "SURFACE_STYLE_USAGE";
        public const string SurfaceSideStyleText = "SURFACE_SIDE_STYLE";
        public const string SurfaceStyleFillAreaText = "SURFACE_STYLE_FILL_AREA";
        public const string FillAreaStyleText = "FILL_AREA_STYLE";
        public const string FillAreaStyleColourText = "FILL_AREA_STYLE_COLOUR";
        public const string ColourRGBText = "COLOUR_RGB";
        public const string NextAssemblyUsageOccurrenceText = "NEXT_ASSEMBLY_USAGE_OCCURRENCE";


        public static string GetItemTypeString(this StepItemType type)
        {
            switch (type)
            {
                case StepItemType.Product:
                    return ProductText;
                case StepItemType.ProductDefinition:
                    return ProductDefinitionText;
                case StepItemType.ProductDefinitionShape:
                    return ProductDefinitionShapeText;
                case StepItemType.ProductDefinitionFormationWithSpecifiedSource:
                    return ProductDefinitionFormationWithSpecifiedSourceText;
                case StepItemType.ShapeDefinitionRepresentation:
                    return ShapeDefinitionRepresentationText;
                case StepItemType.ShapeRepresentationRelationship:
                    return ShapeRepresentationRelationshipText;
                case StepItemType.ShapeRepresentation:
                    return ShapeRepresentationText;
                case StepItemType.AdvancedBrepShapeRepresentation:
                    return AdvancedBrepShapeRepresentationText;
                case StepItemType.ManiFoldSolidBrep:
                    return ManiFoldSolidBrepText;
                case StepItemType.ClosedShell:
                    return ClosedShellText;
                case StepItemType.AdvancedFace:
                    return AdvancedFaceText;
                case StepItemType.AxisPlacement2D:
                    return Axis2Placement2DText;
                case StepItemType.AxisPlacement3D:
                    return Axis2Placement3DText;
                case StepItemType.BSplineCurveWithKnots:
                    return BSplineCurveWithKnotsText;
                case StepItemType.CartesianPoint:
                    return CartesianPointText;
                case StepItemType.Circle:
                    return CircleText;
                case StepItemType.CylindricalSurface:
                    return CylindricalSurfaceText;
                case StepItemType.Direction:
                    return DirectionText;
                case StepItemType.EdgeCurve:
                    return EdgeCurveText;
                case StepItemType.EdgeLoop:
                    return EdgeLoopText;
                case StepItemType.Ellipse:
                    return EllipseText;
                case StepItemType.FaceBound:
                    return FaceBoundText;
                case StepItemType.FaceOuterBound:
                    return FaceOuterBoundText;
                case StepItemType.Line:
                    return LineText;
                case StepItemType.OrientedEdge:
                    return OrientedEdgeText;
                case StepItemType.Plane:
                    return PlaneText;
                case StepItemType.Vector:
                    return VectorText;
                case StepItemType.VertexPoint:
                    return VertexPointText;
                case StepItemType.StyledItem:
                    return StyledItemText;
                case StepItemType.PresentationStyleAssignment:
                    return PresentationStyleAssignmentText;
                case StepItemType.SurfaceStyleUsage:
                    return SurfaceStyleUsageText;
                case StepItemType.SurfaceSideStyle:
                    return SurfaceSideStyleText;
                case StepItemType.SurfaceStyleFillArea:
                    return SurfaceStyleFillAreaText;
                case StepItemType.FillAreaStyle:
                    return FillAreaStyleText;
                case StepItemType.FillAreaStyleColour:
                    return FillAreaStyleColourText;
                case StepItemType.ColourRGB:
                    return ColourRGBText;
                case StepItemType.NextAssemblyUsageOccurrence:
                    return NextAssemblyUsageOccurrenceText;
                default:
                    throw new InvalidOperationException("Unexpected item type " + type);
            }
        }

        public static string GetItemTypeElementString(this StepItemType type)
        {
            return ToCamel(GetItemTypeString(type));
        }

        private static string ToCamel(string input)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(input.ToLower().Replace("_", " ")).Replace(" ", "");
        }
    }
}
