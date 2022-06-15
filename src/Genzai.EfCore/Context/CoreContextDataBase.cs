using Genzai.EfCore.Constants;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;

namespace Genzai.EfCore.Context;

/// <summary>
/// Core context database
/// </summary>
/// <typeparam name="TContext">Context</typeparam>
public abstract class CoreContextDataBase<TContext> : ContextDataBase<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// Encription provider
    /// </summary>
    protected readonly IEncryptionProvider? _provider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    /// <param name="mediator"></param>
    /// <param name="claimsPrincipal"></param>
    protected CoreContextDataBase(DbContextOptions<TContext> options, IMediator mediator, ClaimsPrincipal claimsPrincipal) : base(options, mediator, claimsPrincipal)
    {
        string? aesProvideKey = Environment.GetEnvironmentVariable(EncryptionConstants.AesProvideKey);
        string? aesProvideIV = Environment.GetEnvironmentVariable(EncryptionConstants.AesProvideIv);
        if (!string.IsNullOrEmpty(aesProvideKey) && !string.IsNullOrEmpty(aesProvideIV))
        {
            _provider = new AesProvider(StringToByteArray(aesProvideKey), StringToByteArray(aesProvideIV));
        }
    }

    private static byte[] StringToByteArray(string hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return bytes;
    }
}
