using OpenTracing;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.Logger
{
    public abstract class JaegerDecorator<TDecorated> : Decorator<TDecorated>
    {
        private ITracer _tracer;

        protected JaegerDecorator(TDecorated decorated, ITracer tracer) : base(decorated)
        {
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
        }

        protected virtual IScope StartScope(string methodName)
        {
            return _tracer
                .BuildSpan($"{DecoratedClassName}.{methodName}")
                .WithTag("method", methodName)
                .WithTag("class", DecoratedFullClassName)
                .StartActive(true);
        }

        protected async Task Decorate(
            Func<Task> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }
        protected async Task Decorate(
            Func<IScope, Task> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                await action(scope);
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected async Task<TReturn> Decorate<TReturn>(
            Func<Task<TReturn>> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected async Task<TReturn> Decorate<TReturn>(
            Func<IScope, Task<TReturn>> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                return await action(scope);
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected TReturn Decorate<TReturn>(
            Func<TReturn> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected TReturn Decorate<TReturn>(
            Func<IScope, TReturn> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                return action(scope);
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected void Decorate(
            Action action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected void Decorate(
            Action<IScope> action,
            [CallerMemberName] string methodName = "")
        {
            using var scope = StartScope(methodName);

            try
            {
                action(scope);
            }
            catch (Exception ex)
            {
                HandleException(ex, scope);
                throw;
            }
        }

        protected virtual void HandleException(Exception ex, IScope scope)
        {
            scope.Span.SetTag("error", true);
            scope.Span.Log($"Exception: {ex.Message}");
        }
    }
}
