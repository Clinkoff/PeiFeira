using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Usuarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Domain.Interfaces;

public interface IBaseRepository<T> where T : class, IBaseEntity
{
    // T é o generico, para que todas as outras interfaces possam passar e usar esses métodos. Basicamente eu estou fazendo um contrato que todas as outras interfaces vão seguir.
    // <T> = Tipo genérico(pode ser Usuario, Equipe, etc.) - where T : class, IBaseEntity = T deve ser uma classe que implementa IBaseEntity  
    // IEnumerable<T> = Lista de itens do tipo T - Task<T?> = Retorna de forma assíncrona, pode ser null
    Task<T?> GetByIdAsync(Guid id);                    // Buscar por ID
    Task<IEnumerable<T>> GetAllAsync();                // Buscar todos
    Task<IEnumerable<T>> GetActiveAsync();             // Buscar só ativos (IsActive = true)
    Task<bool> ExistsAsync(Guid id);                   // Verificar se existe

    Task<T> CreateAsync(T entity);                     // Criar novo
    Task<T> UpdateAsync(T entity);                     // Atualizar existente
    Task<bool> DeleteAsync(Guid id);                   // Deletar fisicamente
    Task<bool> SoftDeleteAsync(Guid id);               // Deletar logicamente (IsActive = false)

    Task<int> CountAsync();                            // Contar todos
    Task<int> CountActiveAsync();                      // Contar só ativos
}