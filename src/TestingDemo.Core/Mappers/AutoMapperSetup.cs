using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using TestingDemo.Core.ModelsDtos;
using TestingDemo.Core.Entities;

namespace TestingDemo.Core.Mappers
{
    public class AutoMapperSetup : BaseMapper
    {
        private static AutoMapperSetup mapper;

        public static AutoMapperSetup Instance => mapper ?? (mapper = new AutoMapperSetup());

        protected override void ConfigMapping(IMapperConfigurationExpression cfg)
        {
            cfg.AddExpressionMapping();

            cfg.CreateBiDirectionalMappings<Employee, EmployeeDto>(MemberList.Destination)
                    .Ignore(x => x.Timestamp,
                            x => x.CreatedDate,
                            x => x.ModifiedDate,
                            x => x.CreatedBy,
                            x => x.ModifiedBy);
        }

        public TDest MapToType<TDest>(object obj) => Mapper.Map<TDest>(obj);
    }
}
