using FluentValidation.Results;
using Genzai.WebCore.Errors;

namespace Genzai.WebCore.Validations;

/// <summary>
/// Validation errors utility class
/// </summary>
public static class ValidationErrorUtils
{
    /// <summary>
    /// It returns validation errors from validation result
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    /// <param name="validationResult">Validation Result</param>
    /// <returns>Validation errors from validation result</returns>
    public static IList<ApplicationError> GetValidationErrors<TModel>(ValidationResult validationResult)
    {
        IList<ApplicationError> result = new List<ApplicationError>();
        //Lanza excepcion
        foreach (ValidationFailure error in validationResult.Errors)
        {
            string message = error.ErrorMessage;
            string codeWithArgs = error.ErrorCode;
            string code = "";
            IList<string> codeArgs = new List<string>();
            string type = typeof(TModel).Name.ToLower();
            if (codeWithArgs != null)
            {
                string[] codeWithArgsAr = codeWithArgs.Split(';');
                code = codeWithArgsAr[0];
                for (int i = 1; i < codeWithArgsAr.Length; i++)
                {
                    codeArgs.Add(codeWithArgsAr[i]);
                }
                IDictionary<string, string> paramsDictionary = GetErrorParamsDictionary(codeArgs);
                result.Add(new ApplicationError(message, type, code, paramsDictionary));
            }

        }
        return result;
    }

    private static IDictionary<string, string> GetErrorParamsDictionary(IList<string> parametersList)
    {
        IDictionary<string, string> result = null;
        if (parametersList == null || parametersList.Count == 0)
        {
            return result;
        }
        result = new Dictionary<string, string>();
        foreach (string parameter in parametersList)
        {
            int index = parameter.IndexOf('=');
            if (index != -1)
            {
                string[] parameterAr = parameter.Split('=');
                if (parameterAr.Length == 2)
                {
                    string parameterName = parameterAr[0];
                    string parameterValue = parameterAr[1];
                    result.Add(parameterName, parameterValue);
                }

            }
        }

        return result;
    }
}
