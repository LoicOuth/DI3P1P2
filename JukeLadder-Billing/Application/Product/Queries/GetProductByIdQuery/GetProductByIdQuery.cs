using Application.Product.Dto;

namespace Application.Product.Queries.GetProductByIdQuery;
public record GetProductByIdQuery(string ProductId) : IRequest<ProductPriceDto>;

