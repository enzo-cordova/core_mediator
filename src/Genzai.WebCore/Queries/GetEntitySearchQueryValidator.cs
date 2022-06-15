using FluentValidation;
using Genzai.EfCore.Search;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Responses;
using System.Globalization;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Entity search query validator
/// </summary>
/// <typeparam name="TEntitySearchRequest">Search request</typeparam>
/// <typeparam name="TEntitySearchResponse">Search response</typeparam>
public abstract class GetEntitySearchQueryValidator<TEntitySearchRequest, TEntitySearchResponse> :
        AbstractValidator<GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>>
    where TEntitySearchRequest : EntitySearch
    where TEntitySearchResponse : IEntitySearchResponse
{
    /// <summary>
    /// Constructor
    /// </summary>
    protected GetEntitySearchQueryValidator()
    {
        //Numero de pagina positivo
        RuleFor(property => property.SearchRequest.PageNumber)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(string.Format(
                CultureInfo.InvariantCulture, WebCoreLocalStrings.ParameterIsNull,
                nameof(GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>.SearchRequest.PageNumber)));
        //Tamaño de pagina positivo
        RuleFor(property => property.SearchRequest.PageSize)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(string.Format(
                CultureInfo.InvariantCulture, WebCoreLocalStrings.ParameterIsNull,
                nameof(GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>.SearchRequest.PageSize)));
    }
}
