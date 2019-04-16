using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Common.Exceptions;
using TestingDemo.Core.Infrastructure;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.Mappers;
using TestingDemo.Core.ModelsDtos;
using TestingDemo.Core.Entities;

namespace TestingDemo.ServiceLayer
{
    /// <summary>
    /// Generic service class that contains most of what you would need for a basic crud stuff!
    /// It talks to the 
    /// </summary>
    /// <typeparam name="T">Dto's</typeparam>
    /// <typeparam name="U">Entity Framework classes</typeparam>
    public abstract class GenericService<Dto, Entity> : IGenericService<Dto> where Dto : BaseDto where Entity : BaseEntity
    {
        public readonly IGenericRepository<Entity> repository;

        public GenericService(IGenericRepository<Entity> repository)
        {
            this.repository = repository;
        }

        public virtual async Task<GenericResult<Dto>> GetByIdAsync(int id)
        {
            try
            {
                //TODO: Here we can add validation and caching 

                Entity result = await repository.GetByIdAsync(id).ConfigureAwait(false);

                if (result == null)
                    return new GenericResult<Dto>($"No {typeof(Entity).Name} with id '{id}' found.");

                return new GenericResult<Dto>
                {
                    Value = AutoMapperSetup.Instance.MapToType<Dto>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);

                return new GenericResult<Dto>(ex);
            }
        }

        public async Task<GenericResult<Dto>> GetByIdAsync(int id, params Expression<Func<Dto, object>>[] includeExpressions)
        {
            try
            {
                Expression<Func<Entity, object>> entityExpression = AutoMapperSetup.Instance.MapToType<Expression<Func<Entity, object>>>(includeExpressions);

                Entity result = await repository.GetByIdAsync(id, entityExpression);

                if (result == null)
                {
                    var message = ($"No {typeof(Entity).Name} with id '{id}' and expressions found.");
                    ExceptionHelper.LogError(message, includeExpressions);
                    return new GenericResult<Dto>(message);
                }

                return new GenericResult<Dto>
                {
                    Value = AutoMapperSetup.Instance.MapToType<Dto>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<Dto>(ex);
            }
        }

        public virtual async Task<GenericResult<Dto>> AddAsync(Dto dto)
        {
            try
            {
                Entity entity = AutoMapperSetup.Instance.MapToType<Entity>(dto);

                Entity result = await repository.AddAsync(entity).ConfigureAwait(false);

                return new GenericResult<Dto>
                {
                    //Note that we could do Value = dto;
                    Value = AutoMapperSetup.Instance.MapToType<Dto>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<Dto>(ex);
            }
        }

        public virtual async Task<GenericResult<int>> CountAsync()
        {
            try
            {
                return new GenericResult<int>
                {
                    Value = await repository.CountAsync().ConfigureAwait(false)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<int>(ex);
            }
        }

        public virtual async Task<GenericResult<int>> DeleteAsync(int id)
        {
            try
            {
                return new GenericResult<int>
                {
                    Value = await repository.DeleteAsync(id).ConfigureAwait(false)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<int>(ex);
            }
        }

        public virtual void Dispose()
        {
            repository.Dispose();
        }

        public virtual async Task<GenericResult<IEnumerable<Dto>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Entity> result = await repository.GetAllAsync().ConfigureAwait(false);

                if (result == null)
                {
                    var message = ($"No matching {typeof(Entity).Name} was found");
                    ExceptionHelper.LogError(message);
                    return new GenericResult<IEnumerable<Dto>>(message);
                }

                return new GenericResult<IEnumerable<Dto>>
                {
                    Value = AutoMapperSetup.Instance.MapToType<IEnumerable<Dto>>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<IEnumerable<Dto>>(ex);
            }
        }

        public virtual async Task<GenericResult<IEnumerable<Dto>>> GetByMatchAsync(Expression<Func<Dto, bool>> match)
        {
            try
            {
                Expression<Func<Entity, bool>> matchMap = AutoMapperSetup.Instance.MapToType<Expression<Func<Entity, bool>>>(match);

                IEnumerable<Entity> result = await repository.GetByMatchAsync(matchMap).ConfigureAwait(false);

                if (result == null)
                {
                    var message = ($"No matching {typeof(Entity).Name} was found.");
                    ExceptionHelper.LogError(message, match);
                    return new GenericResult<IEnumerable<Dto>>(message);
                }

                return new GenericResult<IEnumerable<Dto>>
                {
                    Value = AutoMapperSetup.Instance.MapToType<IEnumerable<Dto>>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<IEnumerable<Dto>>(ex);
            }
        }

        public virtual async Task<GenericResult<Dto>> GetOneByMatchAsync(Expression<Func<Dto, bool>> match)
        {
            try
            {
                Expression<Func<Entity, bool>> matchMap = AutoMapperSetup.Instance.MapToType<Expression<Func<Entity, bool>>>(match);

                Entity result = await repository.GetOneByMatchAsync(matchMap).ConfigureAwait(false);

                var response = new GenericResult<Dto>
                {
                    Value = AutoMapperSetup.Instance.MapToType<Dto>(result)
                };

                return response;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<Dto>(ex);
            }
        }

        public virtual async Task<GenericResult<int>> SaveAsync()
        {
            try
            {
                var result = await repository.SaveAsync().ConfigureAwait(false);

                return new GenericResult<int>
                {
                    Value = result
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<int>(ex);
            }
        }

        public virtual async Task<GenericResult<Dto>> UpdateAsync(Dto dto)
        {
            try
            {
                Entity entity = AutoMapperSetup.Instance.MapToType<Entity>(dto);

                Entity result = await repository.UpdateAsync(entity).ConfigureAwait(false);

                if (result == null)
                {
                    var message = $"Could not update entity. No matching {typeof(Entity).Name} with Id: {dto.Id} was found.";
                    ExceptionHelper.LogError(message, dto);
                    return new GenericResult<Dto>(message);
                }

                return new GenericResult<Dto>
                {
                    Value = AutoMapperSetup.Instance.MapToType<Dto>(result)
                };
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogError(ex);
                return new GenericResult<Dto>(ex);
            }
        }
    }
}