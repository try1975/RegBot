using AutoMapper.Configuration;
using Common.Service.Interfaces;
using RegBot.Db.Entities;

namespace RegBot.Demo.Ninject
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AccountDataEntity, IAccountData>()
                ;
            cfg.CreateMap<IAccountData, AccountDataEntity>()
                ;
        }
    }
}