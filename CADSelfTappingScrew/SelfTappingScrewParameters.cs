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
        /// Наличие шайбы
        /// </summary>
        private bool _washer = false;

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
                        ThrowExceptionWithAMessage(parameterName, 
                            dependentParameter, max, 
                            ParametersName.HeadDiameter);
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, max, 
                            ParametersName.HeadDiameter);
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
                    max, ParametersName.HeadHight);
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
                        ThrowExceptionWithAMessage(parameterName, min, max,
                            ParametersName.InternalThreadDiameter);
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, 
                            dependentParameter, 
                            ParametersName.InternalThreadDiameter);
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
                    ThrowExceptionWithAMessage(parameterName, dependentParameter1,
                        dependentParameter2, ParametersName.RodDiameter);
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
                        ThrowExceptionWithAMessage(parameterName, 
                            dependentParameter, max,
                            ParametersName.RodLength);
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, 
                            max, ParametersName.RodLength);
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
                    ThrowExceptionWithAMessage(parameterName, dependentParameter1,
                        dependentParameter2, ParametersName.ThreadDiameter);
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
                        ThrowExceptionWithAMessage(parameterName, min, max, 
                            ParametersName.ThreadLength);
                    }
                    else
                    {
                        ThrowExceptionWithAMessage(parameterName, min, 
                            dependentParameter, 
                            ParametersName.ThreadLength);
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
                        max, ParametersName.ThreadStep);
                }
            }
        }

        /// <summary>
        /// Свойство наличия шайбы
        /// </summary>
        public bool Washer { get; set; }

        /// <summary>
        /// Функция выбрасывания исключения с заданными параметрами
        /// </summary>
        /// <param name="parameterNameRus">Название параметра на русском</param>
        /// <param name="min">Минимальная граница</param>
        /// <param name="max">Максимальная граница</param>
        /// <param name="parameterName">Название параметра</param>
        private void ThrowExceptionWithAMessage(string parameterNameRus, 
            double min, double max, Enum parameterName)
        {
            string message = "Размер параметра \"" + parameterNameRus +
                             "\" должен быть между " + min + " и " + max + " мм!";
            throw new ArgumentException(message, parameterName.ToString());
        }
        
        //TODO: строковые ключи переделать на перечисления.
        //TODO: XML комментарии?
        /// <summary>
        /// Словарь максимальных значений
        /// </summary>
        public static readonly Dictionary<Enum, double> MaxValues = 
            new Dictionary<Enum, double>(8)
        {
            { ParametersName.ThreadDiameter, 9.9 },
            { ParametersName.InternalThreadDiameter, 7 },
            { ParametersName.RodLength, 100 },
            { ParametersName.ThreadStep, 4.5 },
            { ParametersName.HeadHight,  5 },
            { ParametersName.ThreadLength, 97 },
            { ParametersName.HeadDiameter, 18 },
            { ParametersName.RodDiameter, 9.9 }
        };

        /// <summary>
        /// Словарь минимальных значений
        /// </summary>
        public static readonly Dictionary<Enum, double> MinValues = 
            new Dictionary<Enum, double>(8)
        {
            { ParametersName.ThreadDiameter, 1.61 },
            { ParametersName.InternalThreadDiameter, 1.1 },
            { ParametersName.RodLength, 7 },
            { ParametersName.ThreadStep, 0.8 },
            { ParametersName.HeadHight,  0.96 },
            { ParametersName.ThreadLength, 3.8 },
            { ParametersName.HeadDiameter, 3 },
            { ParametersName.RodDiameter, 1.61 }
        };

        /// <summary>
        /// Словарь значений по-умолчанию
        /// </summary>
        public static readonly Dictionary<Enum, double> DefaultValues = 
            new Dictionary<Enum, double>(8)
        {
            { ParametersName.ThreadDiameter, 7.5},
            { ParametersName.InternalThreadDiameter, 5},
            { ParametersName.RodLength, 70},
            { ParametersName.ThreadStep, 2.5},
            { ParametersName.HeadHight,  4},
            { ParametersName.ThreadLength, 50 },
            { ParametersName.HeadDiameter, 12 },
            { ParametersName.RodDiameter, 7}
        };
    }
}
