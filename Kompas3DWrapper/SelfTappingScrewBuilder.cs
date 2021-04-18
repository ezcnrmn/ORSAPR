using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADSelfTappingScrew;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace KompasWrapper
{
    public class SelfTappingScrewBuilder
    {
        public void BuildSelfTappingScrew(Kompas3DWrapper kompas3DWrapper, SelfTappingScrewParameters selfTappingScrewParameters)
        {
            ksEntity planeXoy = (ksEntity)kompas3DWrapper.iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksEntity iSketch = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_sketch);

            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
            iDefinitionSketch.SetPlane(planeXoy);
            iSketch.Create();

            DrawHexagon(kompas3DWrapper, selfTappingScrewParameters.HeadDiameter, iDefinitionSketch);

            Extrusion(kompas3DWrapper, selfTappingScrewParameters.HeadHight, iSketch);

            ksEntity planeYoz = (ksEntity)kompas3DWrapper.iPart.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ);
            ksEntity iSketch2 = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_sketch);
            
            ksSketchDefinition iDefinitionSketch2 = (ksSketchDefinition)iSketch2.GetDefinition();
            iDefinitionSketch2.SetPlane(planeYoz);
            iSketch2.Create();
            
            // расчеты длины уклона
            double angle20 = 20 * Math.PI / 180;
            double angle70 = 70 * Math.PI / 180;
            double slopeLength =
                (selfTappingScrewParameters.RodDiameter - selfTappingScrewParameters.InternalThreadDiameter) *
                Math.Sin(angle70) / Math.Sin(angle20);
            // расчет длины кончика
            double angle15 = 15 * Math.PI / 180;
            double angle75 = 75 * Math.PI / 180;
            double tipLength = selfTappingScrewParameters.InternalThreadDiameter / 2 * Math.Sin(angle75) /
                               Math.Sin(angle15);
            DrawRod(kompas3DWrapper, selfTappingScrewParameters, iDefinitionSketch2, slopeLength, tipLength);
            
            RotationOperation(kompas3DWrapper, iSketch2, iDefinitionSketch2);

            // расчет длины и радиуса кончика
            double tipLength2 = tipLength * 2 / 3;
            double tipRadius = tipLength2 * Math.Sin(angle15) / Math.Sin(angle75);
            MakeThread(kompas3DWrapper, selfTappingScrewParameters, slopeLength, tipLength, tipLength2 / 2, tipRadius);
        }

private void DrawTriangle(Kompas3DWrapper kompas3DWrapper, SelfTappingScrewParameters parameters,
            ksSketchDefinition iDefinitionSketch, double tipLength)
        {
            ksDocument2D rodDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            // расчет параметров треугольничка
            double triangleBase = parameters.ThreadStep / 2;
            
            rodDocument2D.ksLineSeg(parameters.InternalThreadDiameter / 2, parameters.RodLength - tipLength,
                parameters.ThreadDiameter / 2, parameters.RodLength - tipLength - triangleBase / 2, 1);
            rodDocument2D.ksLineSeg(parameters.ThreadDiameter / 2, parameters.RodLength - tipLength - triangleBase / 2,
                parameters.InternalThreadDiameter / 2, parameters.RodLength - tipLength - triangleBase, 1);
            rodDocument2D.ksLineSeg(parameters.InternalThreadDiameter / 2,
                parameters.RodLength - tipLength - triangleBase, parameters.InternalThreadDiameter / 2,
                parameters.RodLength - tipLength, 1);

            iDefinitionSketch.EndEdit();
        }
        
        private void MakeThread(Kompas3DWrapper kompas3DWrapper, SelfTappingScrewParameters parameters, double slopeLength, double tipLength, double tipLength2, double tipRadius)
        {
            // рисуем треугольник
            ksEntity planeXoz = (ksEntity)kompas3DWrapper.iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksEntity iTriangleSketch = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_sketch);

            ksSketchDefinition iDefinitionTriangleSketch = (ksSketchDefinition)iTriangleSketch.GetDefinition();
            iDefinitionTriangleSketch.SetPlane(planeXoz);
            iTriangleSketch.Create();

            DrawTriangle(kompas3DWrapper, parameters, iDefinitionTriangleSketch, tipLength);
            
            // создаем цилиндрическую спираль
            ksEntity cylindricSpiral = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_cylindricSpiral);
            ksCylindricSpiralDefinition iCylindricSpiralDefinition = (ksCylindricSpiralDefinition)cylindricSpiral.GetDefinition();
            iCylindricSpiralDefinition.diamType = 0;
            iCylindricSpiralDefinition.buildDir = true;
            iCylindricSpiralDefinition.diam = parameters.InternalThreadDiameter;
            iCylindricSpiralDefinition.buildMode = 1;
            iCylindricSpiralDefinition.step = parameters.ThreadStep;
            iCylindricSpiralDefinition.height = parameters.ThreadLength - slopeLength / 3 - tipLength;
            
            // плоскость XOY
            ksEntity planeXoy = (ksEntity)kompas3DWrapper.iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            
            // плоскость смещенная от XOY
            ksEntity offsetPlane = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition offsetPlaneDefinition = (ksPlaneOffsetDefinition) offsetPlane.GetDefinition();
            
            offsetPlaneDefinition.SetPlane(planeXoy);
            offsetPlaneDefinition.direction = false;
            offsetPlaneDefinition.offset = parameters.RodLength - tipLength;
            
            offsetPlane.Create();
            iCylindricSpiralDefinition.SetPlane(offsetPlane);
            cylindricSpiral.Create();
            
            // пускаем по траектории
            Evolution(kompas3DWrapper, iTriangleSketch, cylindricSpiral);
            
            // создаем коническую спираль Объект модели (Интерфейсы ksEntity и IEntity)
            ksEntity conicSpiral = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_conicSpiral);
            ksConicSpiralDefinition iConicSpiralDefinition = (ksConicSpiralDefinition)conicSpiral.GetDefinition();
            iConicSpiralDefinition.buildDir = false;
            iConicSpiralDefinition.initialDiamType = 0;
            iConicSpiralDefinition.initialDiam = parameters.InternalThreadDiameter;
            iConicSpiralDefinition.buildMode = 1;
            iConicSpiralDefinition.step = parameters.ThreadStep;
            iConicSpiralDefinition.terminalDiamType = 0;
            iConicSpiralDefinition.terminalDiam = tipRadius;
            iConicSpiralDefinition.height = tipLength - tipLength2;

            iConicSpiralDefinition.SetPlane(offsetPlane);
            conicSpiral.Create();

            // пускаем по траектории
            Evolution(kompas3DWrapper, iTriangleSketch, conicSpiral);
        }

        public void Evolution(Kompas3DWrapper kompas3DWrapper, ksEntity sketch, ksEntity path)
        {
            ksEntity entityEvolution = kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_bossEvolution);
            ksBossEvolutionDefinition iRotateDefinition = (ksBossEvolutionDefinition)entityEvolution.GetDefinition();
            iRotateDefinition.SetThinParam(false);
            iRotateDefinition.SetSketch(sketch);

            var iArray = iRotateDefinition.PathPartArray();
            iArray.Add(path);
            entityEvolution.Create();
        }
        
        private void RotationOperation(Kompas3DWrapper kompas3DWrapper, ksEntity entitySketch, ksSketchDefinition definitionSketch)
        {
            var sketchAxis = (ksDocument2D)definitionSketch.BeginEdit();
            sketchAxis.ksLineSeg(0, 0, 10, 0, 3);
            definitionSketch.EndEdit();

            ksEntity entityRotate = kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_bossRotated);
            // интерфейс базовой операции вращения
            ksBossRotatedDefinition iRotateDefinition = (ksBossRotatedDefinition)entityRotate.GetDefinition(); 

            //ksRotatedParam rotproperty = (ksRotatedParam)rotateDef.RotatedParam();
            //rotproperty.toroidShape = true;
            //rotproperty.direction = (short)Direction_Type.dtBoth;

            iRotateDefinition.SetThinParam(false);
            iRotateDefinition.SetSketch(entitySketch);  // эскиз операции вращения
            entityRotate.Create();              // создать операцию
        }

        private void DrawRod(Kompas3DWrapper kompas3DWrapper, SelfTappingScrewParameters parameters,
            ksSketchDefinition iDefinitionSketch, double slopeLength, double tipLength) 
        {
            ksDocument2D rodDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            // внутренняя линия эскиза ножки
            rodDocument2D.ksLineSeg(0.0, 0.0, parameters.RodLength, 0.0, 1);
            // линия эскиза стыка с головкой
            rodDocument2D.ksLineSeg(0.0, 0.0, 0.0, parameters.RodDiameter / 2, 1);
            // линия длины утолщения
            rodDocument2D.ksLineSeg(0.0, parameters.RodDiameter / 2, parameters.RodLength - parameters.ThreadLength,
                parameters.RodDiameter / 2, 1);
            // линия уклона
            rodDocument2D.ksLineSeg(parameters.RodLength - parameters.ThreadLength, parameters.RodDiameter / 2,
                parameters.RodLength - parameters.ThreadLength + slopeLength, parameters.InternalThreadDiameter / 2, 1);
            // линия длины резьбы
            rodDocument2D.ksLineSeg(parameters.RodLength - parameters.ThreadLength + slopeLength,
                parameters.InternalThreadDiameter / 2, parameters.RodLength - tipLength,
                parameters.InternalThreadDiameter / 2, 1);
            // линия кончика (можно будеть заменить на кривую)
            rodDocument2D.ksLineSeg(parameters.RodLength - tipLength, parameters.InternalThreadDiameter / 2,
                parameters.RodLength, 0.0, 1);
            iDefinitionSketch.EndEdit();
        }

        private void DrawHexagon(Kompas3DWrapper kompas3DWrapper, double diameter, ksSketchDefinition iDefinitionSketch)
        {
            var hexagon = (ksRegularPolygonParam) kompas3DWrapper.kompas.GetParamStruct(
                    (short) StructType2DEnum.ko_RegularPolygonParam);
            
            hexagon.ang = 0;
            hexagon.count = 6;
            hexagon.describe = true;
            hexagon.radius = diameter / 2;
            hexagon.style = 1;
            hexagon.xc = 0;
            hexagon.yc = 0;

            ksDocument2D hexDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();

            hexDocument2D.ksRegularPolygon(hexagon);

            iDefinitionSketch.EndEdit();
        }

        private void Extrusion(Kompas3DWrapper kompas3DWrapper, double depth, ksEntity iSketch)
        {
            ksEntity extrusion = kompas3DWrapper.iPart.NewEntity(24);
            ksBaseExtrusionDefinition extrusionDefinition = extrusion.GetDefinition();
            extrusionDefinition.SetSideParam(true, 0, depth);
            extrusionDefinition.SetSketch(iSketch);
            extrusion.Create();
        }
    }
}
