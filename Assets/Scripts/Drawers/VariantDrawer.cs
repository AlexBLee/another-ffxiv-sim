using UnityEngine;

public class VariantDrawer : Drawer
{
    private MechanicVariantList _mechanicVariantList;

    public MechanicVariantList MechanicVariantList => _mechanicVariantList;

    public void SetMechanicVariantList(MechanicVariantList mechanicVariantList)
    {
        _mechanicVariantList = mechanicVariantList;
    }
}
