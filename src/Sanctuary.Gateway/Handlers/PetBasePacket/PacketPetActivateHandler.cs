using System;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Sanctuary.Game;
using Sanctuary.Packet;
using Sanctuary.Packet.Common.Attributes;

namespace Sanctuary.Gateway.Handlers;

[PacketHandler]
public static class PacketPetActivateHandler
{
    private static ILogger _logger = null!;
    private static IResourceManager _resourceManager = null!;

    public static void ConfigureServices(IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger(nameof(PacketPetActivateHandler));

        _resourceManager = serviceProvider.GetRequiredService<IResourceManager>();
    }

    public static bool HandlePacketPet(GatewayConnection connection, ReadOnlySpan<byte> data)
    {
        if (!PacketPetSpawn.TryDeserialize(data, out var packet))
        {
            _logger.LogError("Failed to deserialize {packet}.", nameof(PacketPetSpawn));
            return false;
        }

        _logger.LogTrace("Received {name} packet. ( {packet} )", nameof(PacketPetSpawn), packet);

        var petInfo = connection.Player.Pets.SingleOrDefault(x => x.petId == packet.petId);

        if (petInfo is null)
            return true;

        if (!_resourceManager.Pets.TryGetValue(packet.petId, out var petDefinition))
            return true;

        if (!connection.Player.Zone.TryCreatePet(connection.Player, petDefinition, out var pet))
            return true;

        pet.Position = connection.Player.Position;
        pet.Rotation = connection.Player.Rotation;

        pet.NameId = petDefinition.NameID;
        pet.ModelId = petDefinition.PetID;

        connection.Player.OnEntityAdd(pet);

        foreach (var visibleEntity in connection.Player.VisibleEntities)
        {
            visibleEntity.Value.OnEntityAdd(pet);
        }
        return true;
    }
}