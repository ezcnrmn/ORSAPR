using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                 //TODO: RSDN
                double dependentParameter = (InternalThreadDiameter < ThreadDiameter) ? 
                    ThreadDiameter : InternalThreadDiameter;
                dependentParameter = (dependentParameter < RodDiameter) ? 
                    RodDiameter : dependentParameter;
                string parameterName = "Внутренний диаметр головки (D)";

                if (min <= value && value <= max && value > dependentParameter)
                {
                    _headDiameter = value;
                }
                else
                {
                    if (dependentParameter > min)
                    {
                        ThrowExceptionWithAMessage(parameterName, dependentParameter, max, "HeadDiameter");
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, max, "HeadDiameter");
                    }
                    
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
                string parameterName = "Высота головки (K)";

                if (min <= value && value <= max)
                {
                    _headHight = value;
                    return;
                }
                ThrowExceptionWithAMessage(parameterName, min, 
                    max, "HeadHight");
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
                double dependentParameter = (ThreadDiameter < HeadDiameter)? 
                    ThreadDiameter : HeadDiameter;
                dependentParameter = (dependentParameter < RodDiameter) ? 
                    dependentParameter : RodDiameter;
                string parameterName = "Внутренний диаметр резьбы (d2)";

                if (min <= value && value <= max && (value < dependentParameter || 
                                                     dependentParameter == 0))
                {
                    _internalThreadDiameter = value;
                }
                else
                {
                    if (dependentParameter > max)
                    {
                        ThrowExceptionWithAMessage(parameterName, min, max, "InternalThreadDiameter");
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, dependentParameter, "InternalThreadDiameter");
                    }
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
                double dependentParameter1 = (InternalThreadDiameter < min) ? min :
                    InternalThreadDiameter;
                double dependentParameter2 = (HeadDiameter < max && HeadDiameter != 0)?
                    HeadDiameter : max;
                string parameterName = "Общий диаметр стержня (d1)";

                if (dependentParameter1 < value && value < dependentParameter2)
                {
                    _rodDiameter = value;
                }
                else
                {
                    ThrowExceptionWithAMessage(parameterName, dependentParameter1, dependentParameter2, "RodDiameter");
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
                string parameterName = "Общая длина стержня (l)";

                if (min <= value && value <= max && value > dependentParameter)
                {
                    _rodLength = value;
                }
                else
                {
                    if (dependentParameter > min)
                    {
                        ThrowExceptionWithAMessage(parameterName, dependentParameter, max, "RodLength");
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, max, "RodLength");
                    }
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
                double dependentParameter1 = (InternalThreadDiameter < min)? min : 
                    InternalThreadDiameter;
                double dependentParameter2 = (HeadDiameter < max && HeadDiameter != 0)? 
                    HeadDiameter : max;
                string parameterName = "Диаметр резьбы (d)";

                if (dependentParameter1 < value && value < dependentParameter2 || 
                    dependentParameter2 == 0)
                {
                    _threadDiameter = value;
                }
                else
                {
                    ThrowExceptionWithAMessage(parameterName, dependentParameter1, dependentParameter2, "ThreadDiameter");
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
                double min = 3.8;
                double max = 97.0;
                double dependentParameter = RodLength;
                string parameterName = "Длина части стержня с резьбой (b)";

                if (min <= value && value <= max && (value < dependentParameter || 
                    dependentParameter == 0) && value >= dependentParameter * 0.3)
                {
                    _threadLength = value;
                }
                else
                {
                    if (dependentParameter > max)
                    {
                        ThrowExceptionWithAMessage(parameterName, min, max, "ThreadLength");
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, dependentParameter, "ThreadLength");
                    }
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
                string parameterName = "Шаг резьбы (P)";

                if (min <= value && value <= max)
                {
                    _threadStep = value;
                }
                else
                {
                    ThrowExceptionWithAMessage(parameterName, min, 
                        max, "ThreadStep");
                }
            }
        }


        private void ThrowExceptionWithAMessage(string parameterNameRus, 
            double min, double max, string parameterName)
        {
            string message = "Размер параметра \"" + parameterNameRus +
                             "\" должен быть между " + min + " и " + max + " мм!";
            throw new ArgumentException(message, parameterName);
        }
        
        //TODO: строковые ключи переделать на перечисления.
        //TODO: XML комментарии?
        public static readonly Dictionary<string, double> MaxValues = 
            new Dictionary<string, double>(8)
        {
            { "ThreadDiameter", 9.9 },
            { "InternalThreadDiameter", 7 },
            { "RodLength", 100 },
            { "ThreadStep", 4.5 },
            { "HeadHight",  5 },
            { "ThreadLength", 97 },
            { "HeadDiameter", 18 },
            { "RodDiameter", 9.9 }
        };

        public static readonly Dictionary<string, double> MinValues = 
            new Dictionary<string, double>(8)
        {
            { "ThreadDiameter", 1.61 },
            { "InternalThreadDiameter", 1.1 },
            { "RodLength", 7 },
            { "ThreadStep", 0.8 },
            { "HeadHight",  0.96 },
            { "ThreadLength", 3.8 },
            { "HeadDiameter", 3 },
            { "RodDiameter", 1.61 }
        };

        public static readonly Dictionary<string, double> DefaultValues = 
            new Dictionary<string, double>(8)
        {
            { "ThreadDiameter", 7.5},
            { "InternalThreadDiameter", 5},
            { "RodLength", 70},
            { "ThreadStep", 2.5},
            { "HeadHight",  4},
            { "ThreadLength", 50 },
            { "HeadDiameter", 12 },
            { "RodDiameter", 7}
        };
    }
}
