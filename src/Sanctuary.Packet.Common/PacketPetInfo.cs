using Sanctuary.Core.IO;

namespace Sanctuary.Packet.Common;

public class PacketPetInfo : ISerializableType
{
    public int petId;

    public int PetName;

    public int Unknown10;

    public int TintID;
    public string TextureAlias = null!;

    public ulong IconId;

    public void Serialize(PacketWriter writer)
    {
        writer.Write(petId);

        writer.Write(PetName);

        writer.Write(Unknown10);

        writer.Write(TintID);
        writer.Write(TextureAlias);

    }
}