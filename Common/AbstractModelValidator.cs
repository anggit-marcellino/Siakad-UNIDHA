using FluentValidation;

namespace Common
{
    public abstract class AbstractModelValidator<T> : AbstractValidator<T> where T : class { }
}
