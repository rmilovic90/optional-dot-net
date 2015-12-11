using System.Collections;
using NUnit.Framework;

namespace Optional.NET.Tests
{
    [TestFixture]
    public class OptionalEqualityTests
    {
        [Test]
        [TestCaseSource(typeof(OptionalEqualityTestCases),
                        nameof(OptionalEqualityTestCases.EqualityTestCases))]
        public bool EqualityTests(Optional<string> optional, object other) =>
            optional.Equals(other);
    }

    public class OptionalEqualityTestCases
    {
        public static IEnumerable EqualityTestCases
        {
            get
            {
                yield return new TestCaseData(Optional<string>.Of("test"), Optional<string>.Of("test"))
                    .SetName("Equal objects")
                    .SetDescription("Compared object has same type and value")
                    .Returns(true);

                yield return new TestCaseData(Optional<string>.Of("test"), Optional<string>.Of("test2"))
                    .SetName("Not equal objects")
                    .SetDescription("Compared object has same type and different value")
                    .Returns(false);

                yield return new TestCaseData(Optional<string>.Of("test"), null)
                    .SetName("NULL object")
                    .SetDescription("Compared object is NULL")
                    .Returns(false);

                yield return new TestCaseData(Optional<string>.Of("test"), "test")
                    .SetName("Object of different type")
                    .SetDescription("Compared object is not of Optional<T> type")
                    .Returns(false);

                yield return new TestCaseData(Optional<string>.Empty, Optional<string>.Of("test"))
                    .SetName("Empty Optional")
                    .SetDescription("Optional object is empty")
                    .Returns(false);

                yield return new TestCaseData(Optional<string>.Of("test"), Optional<string>.Empty)
                    .SetName("Empty Other Optional")
                    .SetDescription("Other optional object is empty")
                    .Returns(false);
            }
        }
    }
}