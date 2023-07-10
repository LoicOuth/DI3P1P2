namespace Application.Common.Models;

public record AccessTokenModel(string AccessToken, int ExpiresIn, int RefreshExpiresIn, string TokenType, int NotBeforePolicy);