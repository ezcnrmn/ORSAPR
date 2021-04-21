using System;
using NUnit.Framework;

namespace CADSelfTappingScrew.UnitTests
{
    [TestFixture]
    public class SelfTappingScrewParametersTest
    {
        [TestCase(TestName = "blabla")]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
    }
}
