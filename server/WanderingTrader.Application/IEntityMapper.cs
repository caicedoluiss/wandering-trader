namespace WanderingTrader.Application;

public interface IEntityMapper<TFromEntity, TToEntity> where TToEntity : class, new()
{
    TToEntity Map(TFromEntity fromEntity, TToEntity? toEntity = null);
}