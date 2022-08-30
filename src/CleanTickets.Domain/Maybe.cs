namespace CleanTickets.Domain;

public readonly record struct Maybe<T>
{
    private readonly T _value;

    public Maybe(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
        HasValue = true;
    }

    public bool HasValue { get; }

    public T Value
    {
        get
        {
            if (!HasValue)
            {
                throw new InvalidOperationException("Maybe<T> does not hold a value");
            }

            return _value;
        }
    }

    public static Maybe<T> NoValue => default;
}

public static class Maybe
{
    public static Maybe<T> Of<T>(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new Maybe<T>(value);
    }

    public static Maybe<T> Wrap<T>(T? value)
    {
        if (value == null)
        {
            return Maybe<T>.NoValue;
        }

        return Of(value);
    }
}
