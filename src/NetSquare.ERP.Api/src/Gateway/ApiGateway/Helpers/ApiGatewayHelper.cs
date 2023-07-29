//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Helpers;

/// <summary>
/// Defines the <see cref="ApiGatewayHelper" />.
/// </summary>
public static class ApiGatewayHelper
{
    /// <summary>
    /// Defination of GenerateToken
    /// </summary>
    /// <param name="claims">The IEnumerable of Claim<see cref="IEnumerable<Claim>"/></param>
    /// <param name="claimRightList">The List of UserClaimRight<see cref="UserClaimRight"/></param>
    /// <returns>The JwtSecurityToken</returns>
    public static JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, /*List<Proto.ClaimRight> claimRightList,*/ JwtConfigurations jwtSettings)
    {
        List<Claim> userClaims = new();

        ////for (int i = 0; i < claimRightList.Count; i++)
        ////    if (claimRightList[i]?.Uri != string.Empty) groupClaims.Add(new Claim(type: claimRightList[i].HttpVerb, value: claimRightList[i].Uri ?? string.Empty));

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtSettings.Key!));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims.Union(userClaims),
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}