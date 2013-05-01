using System;

namespace Rainfall.Web
{
    public interface IBootstrapperTask<T>
    {
        Action<T> Task { get; }
    }
}