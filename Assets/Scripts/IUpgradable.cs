using System.Collections.Generic;

public interface IUpgradable
{
    public List<Upgrade> Upgrades { get; }

    public void RefreshUpgrades();
    public void AddUpgrade(Upgrade upgrade);
    public void RemoveUpgrade(Upgrade upgrade);
}
