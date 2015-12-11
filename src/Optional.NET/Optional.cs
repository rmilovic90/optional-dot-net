using System;

namespace Optional.NET
{
    public class Optional<T> where T : class
    {
        readonly T _value;

        private Optional() {}

        private Optional(T value)
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

        public Optional<T> Filter(Func<T, bool> predicate) =>
            IsPresent ? (predicate.Invoke(GetValue()) ? this : Empty) : this;

        public Optional<TMapped> Map<TMapped>(Func<T, TMapped> mapper) where TMapped : class =>
            IsPresent ? Optional<TMapped>.Of(mapper.Invoke(GetValue())) : Optional<TMapped>.Empty;

        public T OrElseGet(T fallbackValue) => _value ?? fallbackValue;

        public T OrElseGet(Func<T> function) => _value ?? function.Invoke();

        public T OrElseThrow<TException>(Func<TException> function) where TException : Exception
        {
            if (IsPresent) return GetValue();

            throw function.Invoke();
        }

        public override bool Equals(object obj)
        {
            var optional = obj as Optional<T>;

            if (optional == null) return false;

            if (IsPresent && optional.IsPresent)
            {
                return GetValue().Equals(optional.GetValue());
            }

            return false;
        }

        public override int GetHashCode() => IsPresent ? _value.GetHashCode() : 0;

        public override string ToString() => IsPresent ? $"Optional[{ _value }]" : "Optional.Empty";
    }
}