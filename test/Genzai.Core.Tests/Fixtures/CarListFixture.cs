namespace Genzai.Core.Tests.Fixtures;

/// <summary>
/// Car list Fixture.
/// </summary>
public class CarListFixture : IDisposable
{
    private bool disposedValue = false;

    /// <summary>
    /// Car list.
    /// </summary>
    public IEnumerable<CarClassTest> CarList { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarListFixture"/> class.
    /// </summary>
    public CarListFixture()
    {
        this.CarList = ObjectsMocks.GetCarListMock();
    }

    /// <summary>
    /// Dispose Method.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    /// <param name="disposing">Disposing param.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.CarList = null;
            }

            disposedValue = true;
        }
    }
}