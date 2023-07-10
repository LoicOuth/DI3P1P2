using Application.Product.Dto;

namespace Application.Product.Command.UpdateProductPriceByIdCommand;

public record UpdateProductPriceByIdCommand(string ProductId, long NewPrice, string NewCurrency) : IRequest<ProductPriceDto>;