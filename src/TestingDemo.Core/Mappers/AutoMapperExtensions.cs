using AutoMapper;
using System;
using System.Linq.Expressions;
using TestingDemo.Core.Infrastructure;

namespace TestingDemo.Core.Mappers
{
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// Extension methods for GenericResult.
    /// </summary>
    public static class AutoMapperExtensions
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Maps to generic result.
        /// </summary>
        /// <typeparam name="TSource">The type of the source</typeparam>
        /// <typeparam name="TDest">The type of the destination.</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>Returns a new GenericResult of the provided type.</returns>
        public static GenericResult<TDest> MapToGenericResult<TSource, TDest>(this GenericResult<TSource> result)
        {
            return new GenericResult<TDest>(Mapper.Map<TDest>(result.Value), result.ErrorMessage);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Creates two mappings from source to destination and from destination to source.
        /// </summary>
        /// <typeparam name="TDto">The second type.</typeparam>
        /// <typeparam name="TModel">The first type.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns>
        /// The mapping expression that maps from T2 to T1.
        /// </returns>
        public static IMappingExpression<TModel, TDto> CreateBiDirectionalMappings<TDto, TModel>(this IMapperConfigurationExpression config, MemberList memberList)
        {
            config.CreateMap<TDto, TModel>(memberList);
            return config.CreateMap<TModel, TDto>(memberList);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Creates two mappings from source to destination and from destination to source.
        /// </summary>
        /// <typeparam name="TInterface">The second type.</typeparam>
        /// <typeparam name="TDto">The first type.</typeparam>
        /// <typeparam name="TClass">Construct as class</typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns>
        /// The mapping expression that maps from T2 to T1.
        /// </returns>
        public static IMappingExpression<TDto, TInterface> CreateBiDirectionalMappings<TInterface, TDto, TClass>(this IMapperConfigurationExpression config) where TClass : TInterface, new()
        {
            config.CreateMap<TInterface, TDto>();
            return config.CreateMap<TDto, TInterface>().ConstructUsing(dto => new TClass());
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Ignores the specified members to ignore.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="mappingExpression">The mapping expression.</param>
        /// <param name="membersToIgnore">The members to ignore.</param>
        public static void Ignore<TModel, TDto>(this IMappingExpression<TDto, TModel> mappingExpression, params Expression<Func<TModel, object>>[] membersToIgnore)
        {
            foreach (Expression<Func<TModel, object>> property in membersToIgnore)
            {
                mappingExpression.ForMember(property, cfg => cfg.Ignore());
            }
        }
    }
}
