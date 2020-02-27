using System.Collections.Generic;

public interface IHaveHealthMods
{
    List<HealthMod> HealthMods { get; }
}

public interface IHaveRecoveryMods
{
    List<HealthMod> RecoveryMods { get; }
}

public interface IHaveFertilityMods
{
    List<StatMod> FertMods { get; }
}

public interface IHaveVirilityMods
{
    List<StatMod> ViriMods { get; }
}