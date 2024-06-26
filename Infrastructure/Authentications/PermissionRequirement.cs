﻿using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentications;

internal class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
