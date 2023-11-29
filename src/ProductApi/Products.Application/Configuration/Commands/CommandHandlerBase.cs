using FluentValidation.Results;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Configuration.Commands
{
    public abstract class CommandHandlerBase
    {
        protected ValidationResult ValidationResult;
        protected IUnitOfWork _unitOfWork;
        public CommandHandlerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> Commit(string message)
        {
            if (!(await _unitOfWork.CommitAsync() > 0))
            {
                AddError(message);
            }

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit()
        {
            return await Commit("There was an error saving data").ConfigureAwait(continueOnCapturedContext: false);
        }


    }
}
