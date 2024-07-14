﻿namespace OnlineStore.Application.Interfaces;
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}