using AutoMapper;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Transaction
{
    public class SaveTranscaionCommand :IRequest<bool>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }
    }
    public class SaveTransactionCommandHandler : IRequestHandler<SaveTranscaionCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveTransactionCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,
            UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
           this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(SaveTranscaionCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("User not found");
            }

            bool Exist = await unitOfWork.Product.IsExit(e => e.ID == request.createTransactionDTO.ProductId);
            if (!Exist)            
                throw new Exception("Product not found");

            if (request.createTransactionDTO.FromWarehouseId != null)
            {
                Exist = await unitOfWork.Warehouse.IsExit(e => e.ID == request.createTransactionDTO.FromWarehouseId);
                if (!Exist)
                {
                    throw new Exception("From Warehouse not found");
                }
            }
            if (request.createTransactionDTO.ToWarehouseId != null)
            {
                Exist = await unitOfWork.Warehouse.IsExit(e => e.ID == request.createTransactionDTO.ToWarehouseId);
                if (!Exist)
                {
                    throw new Exception("To Warehouse not found");
                }
            }
            InventoryTransaction inventoryTransaction=mapper.Map<InventoryTransaction>(request.createTransactionDTO);
            inventoryTransaction.UserId = Convert.ToInt32(userId);
            await unitOfWork.InventoryTransaction.AddAsync(inventoryTransaction,cancellationToken);
            await unitOfWork.Save(cancellationToken);
            return true;
        }
    }
}
