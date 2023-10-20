using Utilities.Enums;

namespace Utilities.Shared;

public interface IStandardServiceResult
{
    ResultType Result { get; set; }
    List<string> Messages { get; set; }
}