namespace MilkMan.Shared.Interfaces;

public interface ILoggerManager
{
    void Debug(string message);
    void Information(string message);
    void Warning(string message);
    void Error(string message);
    void Fatal(string message);
}