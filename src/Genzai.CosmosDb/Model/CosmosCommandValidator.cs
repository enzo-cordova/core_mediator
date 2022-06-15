namespace Genzai.CosmosDb.Model;

/// <summary>
/// Generic cosmos command validator.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public abstract class CosmosCommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : CosmosCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosCommandValidator{TCommand}"/> class.
    /// </summary>
    protected CosmosCommandValidator()
    {
        this.RuleFor(command => command.Id).NotEmpty().WithMessage(
            string.Format(CultureInfo.InvariantCulture, LocalStrings.CommandValidationMessage, nameof(CosmosCommand.Id)));
        this.RuleFor(command => command.PartitionKey).NotEmpty().WithMessage(
            string.Format(CultureInfo.InvariantCulture, LocalStrings.CommandValidationMessage, nameof(CosmosCommand.PartitionKey)));
    }
}