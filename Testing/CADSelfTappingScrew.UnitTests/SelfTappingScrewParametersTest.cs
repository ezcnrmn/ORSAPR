using System;
using NUnit.Framework;

namespace CADSelfTappingScrew.UnitTests
{
    [TestFixture]
    public class SelfTappingScrewParametersTest
    {
        [TestCase(TestName = "Тест на корректное значение Диаметра головки")]
        public void HeadDiameter_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            var expectedValue = sourceValue;

            //Act
            parameters.HeadDiameter = sourceValue;
            var actualValue = parameters.HeadDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра головки")]
        public void HeadDiameter_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.HeadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра головки")]
        public void HeadDiameter_OutOfDependentParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.ThreadDiameter = 5;
            
            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.HeadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Высоты головки")]
        public void HeadHight_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 3;
            var expectedValue = sourceValue;

            //Act
            parameters.HeadHight = sourceValue;
            var actualValue = parameters.HeadHight;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Высоты головки")]
        public void HeadHight_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.HeadHight = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Внутреннего диаметра резьбы")]
        public void InternalThreadDiameter_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            var expectedValue = sourceValue;

            //Act
            parameters.InternalThreadDiameter = sourceValue;
            var actualValue = parameters.InternalThreadDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Внутреннего диаметра резьбы")]
        public void InternalThreadDiameter_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.InternalThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Внутреннего диаметра резьбы")]
        public void InternalThreadDiameter_OutOfDependentParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.ThreadDiameter = 5;
            parameters.HeadDiameter = 5.1;
            parameters.RodDiameter = 5;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.InternalThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Внутреннего диаметра резьбы")]
        public void InternalThreadDiameter_OutOfMaxParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 8;
            parameters.ThreadDiameter = 8;
            parameters.HeadDiameter = 9;
            parameters.RodDiameter = 8;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.InternalThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Диаметра резьбы")]
        public void RodDiameter_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            var expectedValue = sourceValue;

            //Act
            parameters.RodDiameter = sourceValue;
            var actualValue = parameters.RodDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void RodDiameter_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.RodDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void RodDiameter_OutOfDependentParameter1Range_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.InternalThreadDiameter = 5;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.RodDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void RodDiameter_OutOfDependentParameter2Range_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.HeadDiameter = 5;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.RodDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Длины резьбы")]
        public void RodLength_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 9;
            var expectedValue = sourceValue;

            //Act
            parameters.RodLength = sourceValue;
            var actualValue = parameters.RodLength;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Длины резьбы")]
        public void RodLength_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.RodLength = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Длины резьбы")]
        public void RodLength_OutOfDependentParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 9;
            parameters.ThreadDiameter = 9;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.RodLength = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Диаметра резьбы")]
        public void ThreadDiameter_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            var expectedValue = sourceValue;

            //Act
            parameters.ThreadDiameter = sourceValue;
            var actualValue = parameters.ThreadDiameter;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void ThreadDiameter_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void ThreadDiameter_OutOfDependentParameter1Range_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.InternalThreadDiameter = 5;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Диаметра резьбы")]
        public void ThreadDiameter_OutOfDependentParameter2Range_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 5;
            parameters.HeadDiameter = 5;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadDiameter = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Длины резьбы")]
        public void ThreadLength_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 50;
            var expectedValue = sourceValue;

            //Act
            parameters.ThreadLength = sourceValue;
            var actualValue = parameters.ThreadLength;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Длины резьбы")]
        public void ThreadLength_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadLength = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Длины резьбы")]
        public void ThreadLength_OutOfDependentParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 50;
            parameters.RodLength = 50;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadLength = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на некорректное значение Длины резьбы")]
        public void ThreadLength_OutOfDependentMaxParameterRange_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 98;
            parameters.RodLength = 99;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadLength = sourceValue;
                }
            );
        }

        [TestCase(TestName = "Тест на корректное значение Шаг резьбы")]
        public void ThreadStep_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 3;
            var expectedValue = sourceValue;

            //Act
            parameters.ThreadStep = sourceValue;
            var actualValue = parameters.ThreadStep;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(TestName = "Тест на некорректное значение Шаг резьбы")]
        public void ThreadStep_OutOfRangeValue_ThrowsException()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = 1000;

            //Assert
            NUnit.Framework.Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    parameters.ThreadStep = sourceValue;
                }
            );
        }
        
        [TestCase(TestName = "Тест на корректное значение Шайбы")]
        public void Washer_CorrectValue_ReturnsSameValue()
        {
            //Setup
            var parameters = new SelfTappingScrewParameters();
            var sourceValue = true;
            var expectedValue = sourceValue;

            //Act
            parameters.Washer = sourceValue;
            var actualValue = parameters.Washer;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
