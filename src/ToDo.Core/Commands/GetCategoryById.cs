using ToDo.Core.Models;
using MediatR;

namespace ToDo.Core.Commands
{
    public class GetCategoryById: IRequest<Category>
    {
        public GetCategoryById(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
