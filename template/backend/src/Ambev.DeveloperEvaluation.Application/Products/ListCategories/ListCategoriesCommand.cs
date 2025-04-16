using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories
{
    public class ListCategoriesCommand : IRequest<List<string>>
    {
    }
}
