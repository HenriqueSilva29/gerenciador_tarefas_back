using Domain.ToDoItems;
using FluentValidation;

namespace Domain.Validations.SortHelperValidators
{
    public class SortHelperValidator : AbstractValidator<ToDoItem>
    {
        public SortHelperValidator()
        {
            //RuleFor()
        }
    }
}
