using Sanctuary.Packet;
using Sanctuary.Game.Resources.Definitions;

namespace Sanctuary.Game.Entities;

public class Pet : Npc
{
    public Player Rider { get; init; }
    public PetDefinition Definition { get; init; }

    public Pet(Player player, PetDefinition petDefinition)
    {
        Rider = player;
        Definition = petDefinition;
    }

    public override void OnEntityAdd(IEntity entity)
    {
        // Pet

        base.OnEntityAdd(entity);
    }

    public override void OnEntityRemove(IEntity entity)
    {
        base.OnEntityRemove(entity);
    }

    public override PlayerUpdatePacketAddNpc GetAddNpcPacket()
    {
        var packet = base.GetAddNpcPacket();

        packet.RiderGuid = Rider.Guid;

        return packet;
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}