﻿namespace Weather.Model;

public class User
{
    public string UserId { get; set; }
    public string UserName { get; set; }

    public PermissibleLimits UserPermissibleLimits { get; set; }
    
}