namespace MintProgram;

public record GuardError(string Title, string Detail) { }
public class GuardException : Exception
{
    public IEnumerable<GuardError> GuardErrors { get; }
    public GuardException(GuardError headError, params IEnumerable<GuardError> tailErrors) :
    base(string.Join(Environment.NewLine, Enumerable.Concat([headError], tailErrors).Select(e => $"{e.Title}: {e.Detail}")))
    {
        GuardErrors = Enumerable.Concat([headError], tailErrors);
    }

}

public abstract class GuardOf<T>
{
    protected abstract T Value { get; }
    protected abstract T Guard(T value);
    public T Guard()
    {
        return Guard(Value);
    }
}