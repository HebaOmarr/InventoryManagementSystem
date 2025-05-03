using AutoMapper;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Products
{
 
    public class DeleteProductCommand :IRequest<bool>
    {
        public int ID { get; set; }
      
    }
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.Product.Delete(e => e.ID == request.ID,cancellationToken);
            if (result)
            {
                await unitOfWork.Save(cancellationToken);

             return true;
            }
           return false;

        }
    }
}
