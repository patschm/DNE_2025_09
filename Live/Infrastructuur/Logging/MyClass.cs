using Microsoft.Extensions.Logging;

namespace Logging;

internal class MyClass
{
    private ILogger<MyClass> _logger;

    public MyClass(ILogger<MyClass> logger)
    {
        _logger = logger;
    }

    public void DoeIets()
    {
        _logger.LogCritical("Kritiek");
        _logger.LogError("Fout!");
        _logger.LogWarning("Waarschuwing");
        _logger.LogInformation("Info");
        _logger.LogDebug("Debugger");
        _logger.LogTrace("Trees");
    }
}
