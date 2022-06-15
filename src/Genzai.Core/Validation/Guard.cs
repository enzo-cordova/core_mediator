namespace Genzai.Core.Validation;

/// <summary>
/// Guard parameters and local variables.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws an exception when assertion is true.
    /// </summary>
    /// <typeparam name="TException">Type of exception.</typeparam>
    /// <param name="assertion">The assertion to evaluate.</param>
    /// <param name="message">Message to throw.</param>
    public static void Against<TException>(bool assertion, string message)
        where TException : Exception
    {
        if (assertion)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }

    /// <summary>
    /// Throws an exception when assertion is true with params.
    /// </summary>
    /// <typeparam name="TException">Type of exception.</typeparam>
    /// <param name="assertion">The assertion to evaluate.</param>
    /// <param name="message">Message template to show.</param>
    /// <param name="args">Params to show in message.</param>
    public static void Against<TException>(bool assertion, string message, params object[] args)
        where TException : Exception
    {
        if (assertion)
        {
            string internalMessage = string.Format(CultureInfo.InvariantCulture, message, args);

            throw (TException)Activator.CreateInstance(typeof(TException), internalMessage);
        }
    }

    /// <summary>
    /// Throws an exception when assertion delegate returns true.
    /// </summary>
    /// <typeparam name="TException">Type of exception.</typeparam>
    /// <param name="assertion">Assertion delegate.</param>
    /// <param name="message">Message to throw.</param>
    public static void Against<TException>(Func<bool> assertion, string message)
        where TException : Exception
    {
        if (assertion == null)
        {
            throw new ArgumentNullException(message);
        }
        else
        {
            // Execute the lambda and if it evaluates to true then throw the exception.
            if (assertion())
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }
    }

    /// <summary>
    /// Throws an InvalidOperationException when object instance not implements TInterface.
    /// </summary>
    /// <typeparam name="TInterface">Interface type.</typeparam>
    /// <param name="instance">Object Instance.</param>
    /// <param name="message">Message to throw.</param>
    public static void Implements<TInterface>(object instance, string message)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(message);
        }
        else
        {
            Implements<TInterface>(instance.GetType(), message);
        }
    }

    /// <summary>
    /// Throws an InvalidOperationException when object instance not implements TInterface.
    /// </summary>
    /// <typeparam name="TInterface">Interface type.</typeparam>
    /// <param name="type">Instance type.</param>
    /// <param name="message">Message to throw.</param>
    public static void Implements<TInterface>(Type type, string message)
    {
        if (!typeof(TInterface).IsAssignableFrom(type))
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Throws an InvalidOperationException when object instance not inherits TBase.
    /// </summary>
    /// <typeparam name="TBase">Base Type object.</typeparam>
    /// <param name="instance">Object Instance.</param>
    /// <param name="message">Message to throw.</param>
    public static void InheritsFrom<TBase>(object instance, string message)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(message);
        }
        else
        {
            InheritsFrom<TBase>(instance.GetType(), message);
        }
    }

    /// <summary>
    /// Throws an InvalidOperationException when object instance not inherits TBase.
    /// </summary>
    /// <typeparam name="TBase">Base Type object.</typeparam>
    /// <param name="type">Instance type.</param>
    /// <param name="message">Message to throw.</param>
    public static void InheritsFrom<TBase>(Type type, string message)
    {
        if (type == null)
        {
            throw new ArgumentNullException(message);
        }
        else
        {
            if (type.BaseType != typeof(TBase))
            {
                throw new InvalidOperationException(message);
            }
        }
    }

    /// <summary>
    /// Throws an exception when the objects are not the same.
    /// </summary>
    /// <typeparam name="TException">Type of exception.</typeparam>
    /// <param name="compare">Object instance to compare.</param>
    /// <param name="instance">Object Instance.</param>
    /// <param name="message">Message to throw.</param>
    public static void IsEqual<TException>(object compare, object instance, string message)
        where TException : Exception
    {
        if (compare != instance)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }

    /// <summary>
    /// Throws ArgumentNullException when instance is null.
    /// </summary>
    /// <param name="instance">Object Instance.</param>
    /// <param name="message">Message to throw.</param>
    public static void IsNotNull(object instance, string message)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(message);
        }
    }

    /// <summary>
    /// Throws ArgumentNullException when string is null or empty.
    /// </summary>
    /// <param name="instance">String to evaluate.</param>
    /// <param name="message">Message to throw.</param>
    public static void IsNotNullNorEmpty(string instance, string message)
    {
        if (string.IsNullOrEmpty(instance))
        {
            throw new ArgumentNullException(message);
        }
    }

    /// <summary>
    /// Throws ArgumentNullException when string is null or white space.
    /// </summary>
    /// <param name="instance">String to evaluate.</param>
    /// <param name="message">Message to throw.</param>
    public static void IsNotNullNorWhiteSpace(string instance, string message)
    {
        if (string.IsNullOrWhiteSpace(instance))
        {
            throw new ArgumentNullException(message);
        }
    }

    /// <summary>
    /// Throws InvalidOperationException when instance is not TType.
    /// </summary>
    /// <typeparam name="TType">Object Type.</typeparam>
    /// <param name="instance">Object Instance.</param>
    /// <param name="message">Message to throw.</param>
    public static void TypeOf<TType>(object instance, string message)
    {
        if (!(instance is TType))
        {
            throw new InvalidOperationException(message);
        }
    }
}