﻿namespace GastosResidenciais.Communication.Requests.UserRequests;

public class RequestCreateUserJson
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
