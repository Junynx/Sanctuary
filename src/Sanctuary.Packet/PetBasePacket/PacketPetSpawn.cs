using System;

using Sanctuary.Core.IO;

namespace Sanctuary.Packet;

public class PacketPetSpawn : PetBasePacket, IDeserializable<PacketPetSpawn>
{
    public new const byte OpCode = 6;

    public int petId;

    public PacketPetSpawn() : base(OpCode)
    {
    }

    public static bool TryDeserialize(ReadOnlySpan<byte> data, out PacketPetSpawn value)
    {
        value = new PacketPetSpawn();

        var reader = new PacketReader(data);

        if (!value.TryRead(ref reader))
            return false;

        if (!reader.TryRead(out value.petId))
            return false;

        return reader.RemainingLength == 0;
    }
}