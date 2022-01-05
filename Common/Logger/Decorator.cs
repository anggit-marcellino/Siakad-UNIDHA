using System;

public interface IDecorator
{
    string DecoratedClassName { get; }
    string DecoratedFullClassName { get; }
}

public abstract class Decorator<TDecorated> : IDecorator
{
    private readonly Lazy<string> _decoratedClassName;
    private readonly Lazy<string> _decoratedFullClassName;

    protected string DecoratedClassName => _decoratedClassName.Value;

    protected string DecoratedFullClassName => _decoratedFullClassName.Value;

    protected TDecorated Decorated { get; }

    protected Decorator(TDecorated decorated)
    {
        Decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));

        _decoratedClassName = new Lazy<string>(GetDecoratedClassName);
        _decoratedFullClassName = new Lazy<string>(GetDecoratedFullClassName);
    }

    protected virtual string GetDecoratedClassName() =>
        (Decorated as IDecorator)?.DecoratedClassName
        ?? Decorated?.GetType().Name
        ?? typeof(TDecorated).Name;

    protected virtual string GetDecoratedFullClassName() =>
        (Decorated as IDecorator)?.DecoratedFullClassName
        ?? Decorated?.GetType().FullName
        ?? typeof(TDecorated).FullName;

    string IDecorator.DecoratedClassName => DecoratedClassName;

    string IDecorator.DecoratedFullClassName => DecoratedFullClassName;
}