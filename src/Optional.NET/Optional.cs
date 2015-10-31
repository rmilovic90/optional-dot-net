﻿using System;

namespace Optional.NET
{
    public class Optional<T> : IEquatable<T> where T : class
    {
        readonly T _value;

        public static bool operator ==(Optional<T> optional1, Optional<T> optional2)
        {
            if (((object) optional1) == null || ((object) optional2) == null)
                return Equals(optional1, optional2);

            return optional1.Equals(optional2);
        }

        public static bool operator !=(Optional<T> optional1, Optional<T> optional2)
        {
            if (((object) optional1) == null || ((object) optional2) == null)
                return !Equals(optional1, optional2);

            return !optional1.Equals(optional2);
        }

        public Optional() {}

        public Optional(T value)
        {
            _value = value;
        }

        public static Optional<T> Empty => new Optional<T>();

        public static Optional<T> Of(T value) => new Optional<T>(value);

        public bool IsPresent => _value != null;

        public T GetValue()
        {
            if (IsPresent) return _value;

            throw new MissingValueException("Value is not present.");
        }

        public void IfPresent(Action<T> action)
        {
            if (IsPresent) action.Invoke(_value);
        }

        public T OrElseGet(T fallbackValue) => _value ?? fallbackValue;

        public T OrElseGet(Func<T> function) => _value ?? function.Invoke();

        public T OrElseThrow<TE>(Func<TE> function) where TE : Exception
        {
            if (IsPresent) return GetValue();

            throw function.Invoke();
        }

        public bool Equals(T other)
        {
            if (other == null) return false;

            return IsPresent && _value.Equals(other);
        }

        public override bool Equals(object obj)
        {
            var optional = obj as Optional<T>;

            if (optional == null) return false;

            return optional.IsPresent && Equals(optional.GetValue());
        }

        public override int GetHashCode() => IsPresent ? _value.GetHashCode() : 0;

        public override string ToString() => IsPresent ? $"Optional[{ _value }]" : "Optional.Empty";
    }
}