namespace Products.Application.Configuration.Commands
{
    public abstract class CommandBase<TResult> : Command, ICommand<TResult>
    {
        public long Id { get; }

    }
}
