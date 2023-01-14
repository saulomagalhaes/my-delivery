using AutoMapper;
using MyDelivery.Application.DTOs.Purchase;
using MyDelivery.Application.DTOs.Validations;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;
using MyDelivery.Domain.Validations;
using System.Runtime.Intrinsics.Arm;

namespace MyDelivery.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private ReadPurchaseDTO readPurchaseDTO;

    public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IPersonRepository personRepository, IMapper mapper)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<ReadPurchaseDTO>> Create(PurchaseDTO purchaseDTO)
    {
        var result = new PurchaseDTOValidator().Validate(purchaseDTO);
        if(!result.IsValid)
            return ResultService.RequestError<ReadPurchaseDTO>("Problema na validação", result);

        var productId = await _productRepository.GetIdByCode(purchaseDTO.Code);
        var personId = await _personRepository.GetIdByDocument(purchaseDTO.Document);
        var purchase = new Purchase(productId, personId);
        
        var data = await _purchaseRepository.Create(purchase);

        readPurchaseDTO = _mapper.Map<ReadPurchaseDTO>(purchaseDTO);
        readPurchaseDTO.Id = data.Id;
        return ResultService.Ok<ReadPurchaseDTO>(readPurchaseDTO, data.Id);
    }
}
