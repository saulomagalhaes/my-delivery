using AutoMapper;
using MyDelivery.Application.DTOs.Purchase;
using MyDelivery.Application.DTOs.Validations;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private ReadPurchaseDTO readPurchaseDTO;
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultService<ReadPurchaseDTO>> Create(PurchaseDTO purchaseDTO)
    {
        var result = new PurchaseDTOValidator().Validate(purchaseDTO);
        if(!result.IsValid)
            return ResultService.RequestError<ReadPurchaseDTO>("Problema na validação", result);
        try
        {
            await _unitOfWork.BeginTransaction();
            var productId = await _productRepository.GetIdByCode(purchaseDTO.Code);
            if(productId == 0)
            {
                var product = new Product(purchaseDTO.ProductName, purchaseDTO.Code, purchaseDTO.Price ?? 0);
                await _productRepository.Create(product);
                productId = product.Id;
            }
            var personId = await _personRepository.GetIdByDocument(purchaseDTO.Document);
            var purchase = new Purchase(productId, personId);

            var data = await _purchaseRepository.Create(purchase);

            readPurchaseDTO = _mapper.Map<ReadPurchaseDTO>(purchaseDTO);
            readPurchaseDTO.Id = data.Id;
            await _unitOfWork.Commit();
            return ResultService.Ok<ReadPurchaseDTO>(readPurchaseDTO, data.Id);
        }
        catch(Exception ex) 
        {
            await _unitOfWork.RollBack();
            return ResultService.Fail<ReadPurchaseDTO>($"{ex.Message}");
        }
    }
    public async Task<ResultService> Delete(int id)
    {
        var purchase = await _purchaseRepository.GetById(id);
        if (purchase == null)
            return ResultService.Fail("Compra não encontrada");
        await _purchaseRepository.Delete(purchase);
        return ResultService.Ok("NoContent");
    }

    public async Task<ResultService<PurchaseDetailsDTO>> GetById(int id)
    {
        var purchase = await _purchaseRepository.GetById(id);
        if (purchase == null)
            return ResultService.Fail<PurchaseDetailsDTO>("Compra não encontrada");

        return ResultService.Ok<PurchaseDetailsDTO>(_mapper.Map<PurchaseDetailsDTO>(purchase)); 
    }

    public async Task<ResultService<ICollection<PurchaseDetailsDTO>>> GetPurchases(int page, int rows)
    {
        var purchases = await _purchaseRepository.GetPurchases(page, rows);
        return ResultService.Ok<ICollection<PurchaseDetailsDTO>>(_mapper.Map<ICollection<PurchaseDetailsDTO>>(purchases));
    }

    public async Task<ResultService> Update(int id, PurchaseDTO purchaseDTO)
    {
        var result = new PurchaseDTOValidator().Validate(purchaseDTO);
        if (!result.IsValid)
            return ResultService.RequestError("Problema na validação", result);

        var purchase = await _purchaseRepository.GetById(id);
        if (purchase == null)
            return ResultService.Fail("Compra não encontrada");

        var productId = await _productRepository.GetIdByCode(purchaseDTO.Code);
        var personId = await _personRepository.GetIdByDocument(purchaseDTO.Document);
        purchase.Update(id, productId, personId);

        purchase = _mapper.Map(purchaseDTO, purchase);
        await _purchaseRepository.Update(purchase);
        return ResultService.Ok("NoContent");
    }
}
