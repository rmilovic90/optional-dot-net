using System;
using NUnit.Framework;

namespace Optional.NET.Tests
{
    [TestFixture]
    public class OptionalTests
    {
        [Test]
        public void Optional_WhenEmpty_DoesNotHaveValuePresent()
        {
            Optional<string> optional = Optional<string>.Empty;

            Assert.That(optional.IsPresent, Is.False);
        }

        [Test]
        public void Optional_OfValue_HasValuePresent()
        {
            Optional<string> optional = Optional<string>.Of("test");

            Assert.That(optional.IsPresent, Is.True);
        }

        [Test]
        public void Optional_WhenPresent_ReturnsValue()
        {
            string value = "test";

            Optional<string> optional = Optional<string>.Of(value);

            Assert.That(optional.GetValue(), Is.EqualTo(value));
        }

        [Test]
        public void GetValue_WhenNotPresent_ThrowsMissingValueException()
        {
            Optional<string> optional = Optional<string>.Empty;

            Assert.That(() => optional.GetValue(), Throws.InstanceOf<MissingValueException>());
        }

        [Test]
        public void Optional_WhenPresent_ExecutesAction()
        {
            bool actionIsExecuted = false;

            Optional<string> optional = Optional<string>.Of("test");

            optional.IfPresent((v) => { actionIsExecuted = true; });

            Assert.That(actionIsExecuted, Is.True);
        }

        [Test]
        public void Optional_WhenNotPresent_DoesNotExecuteAction()
        {
            bool actionIsExecuted = false;

            Optional<string> optional = Optional<string>.Empty;

            optional.IfPresent((v) => { actionIsExecuted = true; });

            Assert.That(actionIsExecuted, Is.False);
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValue()
        {
            string value = "test";

            Optional<string> optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseGet("test 2");

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsFallbackValue()
        {
            string fallbackValue = "test";

            Optional<string> optional = Optional<string>.Empty;

            var retrievedValue = optional.OrElseGet(fallbackValue);

            Assert.That(retrievedValue, Is.EqualTo(fallbackValue));
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValueInsteadOfFuctionInvocationValue()
        {
            string value = "test";

            Optional<string> optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseGet(() => "test 2");

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsValueOfFunctionInvocation()
        {
            string fallbackValue = "test";

            Optional<string> optional = Optional<string>.Empty;

            var retrievedValue = optional.OrElseGet(() => fallbackValue);

            Assert.That(retrievedValue, Is.EqualTo(fallbackValue));
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValueInsteadOfThrowingException()
        {
            string value = "test";

            Optional<string> optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseThrow<NotSupportedException>(() => new NotSupportedException());

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ThrowsException()
        {
            Optional<string> optional = Optional<string>.Empty;

            Assert.That(() => optional.OrElseThrow<NotSupportedException>(() => new NotSupportedException()),
                Throws.InstanceOf<NotSupportedException>());
        }
    }
}