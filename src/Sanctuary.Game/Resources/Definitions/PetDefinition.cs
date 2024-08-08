namespace Sanctuary.Game.Resources.Definitions;

public class PetDefinition
{
    public int PetID { get; set; }

    public string PetName { get; set; } = null!;

    public int NameID { get; set; }

    public int IconID { get; set; }

    public int TintID { get; set; }

    public string TextureAlias { get; set; } = null!;

    public int PetModelID { get; set; }

    public int PetItemDefinitionID { get; set; }
}