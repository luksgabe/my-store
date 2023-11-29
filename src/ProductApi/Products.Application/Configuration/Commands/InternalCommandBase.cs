namespace Products.Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        public long Id { get; }

        protected InternalCommandBase(long id)
        {
            this.Id = id;
        }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        public long Id { get; }

        protected InternalCommandBase()
        {
            this.Id = 0;
        }

        protected InternalCommandBase(long id)
        {
            this.Id = id;
        }
    }
}
