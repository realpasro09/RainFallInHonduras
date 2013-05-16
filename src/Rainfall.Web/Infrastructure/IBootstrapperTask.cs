using System;

namespace Rainfall.Web.Infrastructure
{
    public interface IBootstrapperTask<T>
    {
        Action<T> Task { get; }
    }
}