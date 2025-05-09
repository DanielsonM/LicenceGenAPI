﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LicenceGenAPI.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool authenticated, string created, string expiration, string? accessToken, string? refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken == null ? string.Empty : accessToken;
            RefreshToken = refreshToken == null ? string.Empty : refreshToken;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
