using AutoMapper;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly UserManager<ApplicationUser> userManager;

        public SaveTransactionCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(SaveTranscaionCommand request, CancellationToken cancellationToken)
        {
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
            await unitOfWork.InventoryTransaction.AddAsync(inventoryTransaction,cancellationToken);
            await unitOfWork.Save(cancellationToken);
            return true;
        }
    }
}
