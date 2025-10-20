﻿namespace PropostaFacil.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
