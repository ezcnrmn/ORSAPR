using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADSelfTappingScrew
{
    /// <summary>
    /// Параметры самореза
    /// </summary>
    public class SelfTappingScrewParameters
    {
        /// <summary>
        /// Внутренний диаметр головки 
        /// </summary>
        private double _headDiameter;

        /// <summary>
        /// Высота головки
        /// </summary>
        private double _headHight;

        /// <summary>
        /// Внутренний диаметер резьбы
        /// </summary>
        private double _internalThreadDiameter;

        /// <summary>
        /// Общий диаметр стержня 
        /// </summary>
        private double _rodDiameter;

        /// <summary>
        /// Общая длина стержня 
        /// </summary>
        private double _rodLength;

        /// <summary>
        /// Диаметр резьбы 
        /// </summary>
        private double _threadDiameter;

        /// <summary>
        /// Длина части стержня с резьбой 
        /// </summary>
        private double _threadLength;

        /// <summary>
        /// Шаг резьбы 
        /// </summary>
        private double _threadStep;

        /// <summary>
        /// Свойство внутреннего диаметра головки 
        /// </summary>
        public double HeadDiameter
        {
            get => _headDiameter;

            set
            {
                double min = 3.0;
                double max = 18.0;
                double dependentParameter =
                    (InternalThreadDiameter < ThreadDiameter) ? ThreadDiameter : InternalThreadDiameter;
                dependentParameter = (dependentParameter < RodDiameter) ? RodDiameter : dependentParameter;
                string parameterName = "Внутренний диаметр головки";

                if (min <= value && value <= max && value > dependentParameter)
                {
                    _headDiameter = value;
                }
                else
                {
                    string message;
                    if (dependentParameter > min)
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть строго больше " +
                                  dependentParameter + " мм и не больше " + max + " мм!";
                    }
                    else
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                  " мм и не больше " + max + " мм!";
                    }
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство высоты головки
        /// </summary>
        public double HeadHight
        {
            get => _headHight;

            set
            {
                double min = 0.96;
                double max = 5.0;
                string parameterName = "Высота головки";

                if (min <= value && value <= max)
                {
                    _headHight = value;
                }
                else
                {
                    string message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                     " мм и не больше " + max + " мм!";
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство внутреннего диаметера резьбы
        /// </summary>
        public double InternalThreadDiameter
        {
            get => _internalThreadDiameter;

            set
            {
                double min = 1.1;
                double max = 7.0;
                double dependentParameter = (ThreadDiameter < HeadDiameter)? ThreadDiameter : HeadDiameter;
                dependentParameter = (dependentParameter < RodDiameter) ? dependentParameter : RodDiameter;
                string parameterName = "Внутренний диаметр резьбы";

                if (min <= value && value <= max && (value < dependentParameter || dependentParameter == 0))
                {
                    _internalThreadDiameter = value;
                }
                else
                {
                    string message;
                    if (dependentParameter > max)
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                  " мм и не больше " + max + " мм!";
                    }
                    else
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                  " мм и строго меньше " + dependentParameter + " мм!";
                    }
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство общего диаметра стержня 
        /// </summary>
        public double RodDiameter
        {
            get => _rodDiameter;

            set
            {
                double min = 1.6;
                double max = 10.0;
                double dependentParameter1 = (InternalThreadDiameter < min) ? min : InternalThreadDiameter;
                double dependentParameter2 = (HeadDiameter < max && HeadDiameter != 0) ? HeadDiameter : max;
                string parameterName = "Общий диаметр стержня";

                if (dependentParameter1 < value && value < dependentParameter2)
                {
                    _rodDiameter = value;
                }
                else
                {
                    string message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " +
                                     dependentParameter1 + " мм и не больше " + dependentParameter2 + " мм!";
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство общей длины стержня 
        /// </summary>
        public double RodLength
        {
            get => _rodLength;

            set
            {
                double min = 7.0;
                double max = 100.0;
                double dependentParameter = ThreadDiameter;
                string parameterName = "Общая длина стержня";

                if (min <= value && value <= max && value > dependentParameter)
                {
                    _rodLength = value;
                }
                else
                {
                    string message;
                    if (dependentParameter > min)
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть строго больше " +
                                  dependentParameter + " мм и не больше " + max + " мм!";
                    }
                    else
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                  " мм и не больше " + max + " мм!";
                    }
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство диаметра резьбы 
        /// </summary>
        public double ThreadDiameter
        {
            get => _threadDiameter;

            set
            {
                double min = 1.6;
                double max = 10.0;
                double dependentParameter1 = (InternalThreadDiameter < min)? min : InternalThreadDiameter;
                double dependentParameter2 = (HeadDiameter < max && HeadDiameter != 0)? HeadDiameter : max;
                string parameterName = "Диаметр резьбы";

                if (dependentParameter1 < value && value < dependentParameter2 || dependentParameter2 == 0)
                {
                    _threadDiameter = value;
                }
                else
                {
                    string message = "Размер параметра \"" + parameterName + "\" должен быть строго больше " +
                                     dependentParameter1 + " мм и строго меньше " + dependentParameter2 + " мм!";
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство длины части стержня с резьбой 
        /// </summary>
        public double ThreadLength
        {
            get => _threadLength;

            set
            {
                double min = 3.0;
                double max = 97.0;
                double dependentParameter = RodLength;
                string parameterName = "Длина части стержня с резьбой";

                if (min <= value && value <= max && (value < dependentParameter || dependentParameter == 0))
                {
                    _threadLength = value;
                }
                else
                {
                    string message;
                    if (dependentParameter > max)
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                         " мм и не больше " + max + " мм!";
                    }
                    else
                    {
                        message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                         " мм и строго меньше " + dependentParameter + " мм!";
                    }
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>
        /// Свойство шага резьбы
        /// </summary>
        public double ThreadStep
        {
            get => _threadStep;

            set
            {
                double min = 0.8;
                double max = 4.5;
                string parameterName = "Шаг резьбы";

                if (min <= value && value <= max)
                {
                    _threadStep = value;
                }
                else
                {
                    string message = "Размер параметра \"" + parameterName + "\" должен быть не меньше " + min +
                                     " мм и не больше " + max + " мм!";
                    throw new ArgumentException(message);
                }
            }
        }
    }
}
