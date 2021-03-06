﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Optional.NET.Tests
{
    [TestFixture]
    public class OptionalTests
    {
        [Test]
        public void Optional_WhenEmpty_DoesNotHaveValuePresent()
        {
            var optional = Optional<string>.Empty;

            Assert.That(optional.IsPresent, Is.False);
        }

        [Test]
        public void Optional_OfValue_HasValuePresent()
        {
            var optional = Optional<string>.Of("test");

            Assert.That(optional.IsPresent, Is.True);
        }

        [Test]
        public void Optional_WhenPresent_ReturnsValue()
        {
            const string value = "test";

            var optional = Optional<string>.Of(value);

            Assert.That(optional.GetValue(), Is.EqualTo(value));
        }

        [Test]
        public void GetValue_WhenNotPresent_ThrowsMissingValueException()
        {
            var optional = Optional<string>.Empty;

            Assert.That(() => optional.GetValue(), Throws.InstanceOf<MissingValueException>());
        }

        [Test]
        public void Optional_WhenPresent_ExecutesAction()
        {
            var actionIsExecuted = false;

            var optional = Optional<string>.Of("test");

            optional.IfPresent(v => { actionIsExecuted = true; });

            Assert.That(actionIsExecuted, Is.True);
        }

        [Test]
        public void Optional_WhenNotPresent_DoesNotExecuteAction()
        {
            var actionIsExecuted = false;

            var optional = Optional<string>.Empty;

            optional.IfPresent(v => { actionIsExecuted = true; });

            Assert.That(actionIsExecuted, Is.False);
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsCurrentInstanceWhenFiltered()
        {
            var optional = Optional<string>.Empty;

            var result = optional.Filter(v => v.Contains("test"));

            Assert.That(result, Is.SameAs(optional));
        }

        [Test]
        public void Optional_WhenPredicateIsTrue_ReturnsCurrentInstanceWhenFiltered()
        {
            var optional = Optional<string>.Of("testing");

            var result = optional.Filter(v => v.Contains("test"));

            Assert.That(result, Is.SameAs(optional));
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsEmptyOptionalOfMappingType()
        {
            var optional = Optional<List<int>>.Empty;

            var result = optional.Map(t => t.ToString());

            Assert.That(result, Is.InstanceOf(typeof(Optional<string>)));
            Assert.That(result.IsPresent, Is.False);
        }

        [Test]
        public void Optional_WhenPresent_ReturnsOptionalOfMappingType()
        {
            var optional = Optional<List<int>>.Of(new List<int> { 1, 2 });

            var result = optional.Map(t => t.ToString());

            Assert.That(result, Is.InstanceOf(typeof(Optional<string>)));
            Assert.That(result.IsPresent, Is.True);
        }

        [Test]
        public void Optional_WhenPredicateIsFalse_ReturnsEmptyOptional()
        {
            var optional = Optional<string>.Of("testing");

            var result = optional.Filter(v => v.Contains("aaa"));

            Assert.That(result.IsPresent, Is.False);
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValue()
        {
            const string value = "test";

            var optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseGet("test 2");

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsFallbackValue()
        {
            const string fallbackValue = "test";

            var optional = Optional<string>.Empty;

            var retrievedValue = optional.OrElseGet(fallbackValue);

            Assert.That(retrievedValue, Is.EqualTo(fallbackValue));
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValueInsteadOfFuctionInvocationValue()
        {
            const string value = "test";

            var optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseGet(() => "test 2");

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ReturnsValueOfFunctionInvocation()
        {
            const string fallbackValue = "test";

            var optional = Optional<string>.Empty;

            var retrievedValue = optional.OrElseGet(() => fallbackValue);

            Assert.That(retrievedValue, Is.EqualTo(fallbackValue));
        }

        [Test]
        public void Optional_WhenPresent_ReturnsActualValueInsteadOfThrowingException()
        {
            const string value = "test";

            var optional = Optional<string>.Of(value);

            var retrievedValue = optional.OrElseThrow(() => new NotSupportedException());

            Assert.That(retrievedValue, Is.EqualTo(value));
        }

        [Test]
        public void Optional_WhenNotPresent_ThrowsException()
        {
            var optional = Optional<string>.Empty;

            Assert.That(() => optional.OrElseThrow(() => new NotSupportedException()),
                Throws.InstanceOf<NotSupportedException>());
        }
    }
}