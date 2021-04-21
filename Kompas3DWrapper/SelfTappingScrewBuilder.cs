using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADSelfTappingScrew;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using KompasAPI7;

namespace KompasWrapper
{
    /// <summary>
    /// Класс для отрисовки самореза
    /// </summary>
    public class SelfTappingScrewBuilder
    {
        /// <summary>
        /// Функция постройки самореза
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="selfTappingScrewParameters">Параметры самореза</param>
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
            double angle20Radians = 20 * Math.PI / 180;
            double angle70Radians = 70 * Math.PI / 180;

            double slopeLength =
                (selfTappingScrewParameters.RodDiameter - selfTappingScrewParameters.InternalThreadDiameter) *
                Math.Sin(angle70Radians) / Math.Sin(angle20Radians);

            // расчет длины кончика
            double angle15Radians = 15 * Math.PI / 180;
            double angle75Radians = 75 * Math.PI / 180;
            double tipLength = selfTappingScrewParameters.InternalThreadDiameter / 2 * Math.Sin(angle75Radians) /
                               Math.Sin(angle15Radians);
            DrawRod(selfTappingScrewParameters, iDefinitionSketch2, slopeLength, tipLength);
            
            RotationOperation(kompas3DWrapper, iSketch2, iDefinitionSketch2);

            // расчет длины и радиуса кончика
            double tipLength2 = tipLength * 2 / 3;
            double tipRadius = tipLength2 * Math.Sin(angle15Radians) / Math.Sin(angle75Radians);
            MakeThread(kompas3DWrapper, selfTappingScrewParameters, slopeLength, tipLength, tipLength2 / 2, tipRadius);
        }

        /// <summary>
        /// Функция отрисовки чертежа зубца резьбы
        /// </summary>
        /// <param name="parameters">Параметры самореза</param>
        /// <param name="iDefinitionSketch">Параметры эскиза</param>
        /// <param name="tipLength">Длина кончика</param>
        private void DrawTriangle(SelfTappingScrewParameters parameters, ksSketchDefinition iDefinitionSketch, double tipLength)
        {
            ksDocument2D rodDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            // расчет параметров треугольничка
            double triangleBase = parameters.ThreadStep / 2;
            
            rodDocument2D.ksLineSeg(parameters.InternalThreadDiameter / 2, 
                parameters.RodLength - tipLength,
                parameters.ThreadDiameter / 2, 
                parameters.RodLength - tipLength - triangleBase / 2, 1);
            rodDocument2D.ksLineSeg(parameters.ThreadDiameter / 2, parameters.RodLength - tipLength - triangleBase / 2,
                parameters.InternalThreadDiameter / 2, parameters.RodLength - tipLength - triangleBase, 1);
            rodDocument2D.ksLineSeg(parameters.InternalThreadDiameter / 2,
                parameters.RodLength - tipLength - triangleBase, parameters.InternalThreadDiameter / 2,
                parameters.RodLength - tipLength, 1);

            iDefinitionSketch.EndEdit();
        }

        /// <summary>
        /// Функция постройки резьбы
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="parameters">Параметры самореза</param>
        /// <param name="slopeLength">Длина уклона ножки самореза</param>
        /// <param name="tipLength">Длина кончика</param>
        /// <param name="tipLength2">Длина резьбы на конце самореза</param>
        /// <param name="tipRadius">Конечный радиус резьбы</param>
        private void MakeThread(Kompas3DWrapper kompas3DWrapper, SelfTappingScrewParameters parameters,
            double slopeLength, double tipLength, double tipLength2, double tipRadius)
        {
            // рисуем треугольник
            ksEntity planeXoz = (ksEntity)kompas3DWrapper.iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksEntity iTriangleSketch = (ksEntity)kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_sketch);

            ksSketchDefinition iDefinitionTriangleSketch = (ksSketchDefinition)iTriangleSketch.GetDefinition();
            iDefinitionTriangleSketch.SetPlane(planeXoz);
            iTriangleSketch.hidden = true;
            iTriangleSketch.Create();
            
            DrawTriangle(parameters, iDefinitionTriangleSketch, tipLength);
            
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

            // скрытие вспомогательных объектов
            offsetPlane.hidden = true;
            offsetPlane.Update();
            conicSpiral.hidden = true;
            conicSpiral.Update();
            cylindricSpiral.hidden = true;
            cylindricSpiral.Update();
        }

        /// <summary>
        /// Функция кинематического приклеивания
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="sketch">Чертеж объекта</param>
        /// <param name="path">Траектория объекта</param>
        public void Evolution(Kompas3DWrapper kompas3DWrapper, ksEntity sketch, ksEntity path)
        {
            ksEntity entityEvolution = kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_bossEvolution);
            ksBossEvolutionDefinition iRotateDefinition = (ksBossEvolutionDefinition)entityEvolution.GetDefinition();
            iRotateDefinition.SetThinParam(false);
            iRotateDefinition.SetSketch(sketch);

            var pathArray = iRotateDefinition.PathPartArray();
            pathArray.Add(path);
            entityEvolution.Create();
        }

        /// <summary>
        /// Фукнция вращения вокруг оси OZ
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="entitySketch">Чертеж объекта</param>
        /// <param name="definitionSketch">Параметры чертежа</param>
        private void RotationOperation(Kompas3DWrapper kompas3DWrapper, ksEntity entitySketch, ksSketchDefinition definitionSketch)
        {
            var sketchAxis = (ksDocument2D)definitionSketch.BeginEdit();
            sketchAxis.ksLineSeg(0, 0, 10, 0, 3);
            definitionSketch.EndEdit();

            ksEntity entityRotate = kompas3DWrapper.iPart.NewEntity((short)Obj3dType.o3d_bossRotated);
            // интерфейс базовой операции вращения
            ksBossRotatedDefinition iRotateDefinition = (ksBossRotatedDefinition)entityRotate.GetDefinition();

            iRotateDefinition.SetThinParam(false);
            iRotateDefinition.SetSketch(entitySketch);
            entityRotate.Create();
        }

        /// <summary>
        /// Функция отрисовки чертежа ножки
        /// </summary>
        /// <param name="parameters">Параметры самореза</param>
        /// <param name="iDefinitionSketch">Параметры чертежа</param>
        /// <param name="slopeLength">Длина уклона ножки</param>
        /// <param name="tipLength">Длина кончика</param>
        private void DrawRod(SelfTappingScrewParameters parameters, ksSketchDefinition iDefinitionSketch,
            double slopeLength, double tipLength) 
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
            // rodDocument2D.ksLineSeg(parameters.RodLength - tipLength, parameters.InternalThreadDiameter / 2, parameters.RodLength, 0.0, 1);
            rodDocument2D.ksArcBy3Points(parameters.RodLength - tipLength, parameters.InternalThreadDiameter / 2,
                parameters.RodLength - (tipLength * 0.9), parameters.InternalThreadDiameter / 2.07, parameters.RodLength,
                0.0, 1);

            iDefinitionSketch.EndEdit();
        }

        /// <summary>
        /// Функция отрисовки шестиугольника
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="diameter">Диаметр шестиугольника</param>
        /// <param name="iDefinitionSketch">Параметры чертежа</param>
        private void DrawHexagon(Kompas3DWrapper kompas3DWrapper, double diameter, ksSketchDefinition iDefinitionSketch)
        {
            var polygonParam = (ksRegularPolygonParam) kompas3DWrapper.kompas.GetParamStruct(
                    (short) StructType2DEnum.ko_RegularPolygonParam);
            
            polygonParam.ang = 0;
            polygonParam.xc = 0;
            polygonParam.yc = 0;
            polygonParam.count = 6;
            polygonParam.describe = true;
            polygonParam.radius = diameter / 2;
            polygonParam.style = 1;

            ksDocument2D hexDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();

            hexDocument2D.ksRegularPolygon(polygonParam);

            iDefinitionSketch.EndEdit();
        }

        /// <summary>
        /// Функция выдавливания
        /// </summary>
        /// <param name="kompas3DWrapper">Класс в котором вызван Компас-3D</param>
        /// <param name="depth">Глубина выдавливания</param>
        /// <param name="iSketch">Чертеж объекта</param>
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