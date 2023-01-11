﻿using MyDelivery.Application.DTOs;

namespace MyDelivery.Application.Services.Contracts;

public interface IPersonService
{
    Task<ResultService<PersonDTO>> Create(PersonDTO personDTO);

}