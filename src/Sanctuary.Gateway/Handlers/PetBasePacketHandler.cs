﻿using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Sanctuary.Packet;
using Sanctuary.Core.IO;
using Sanctuary.Packet.Common.Attributes;

namespace Sanctuary.Gateway.Handlers;

[PacketHandler]
public static class PetBasePacketHandler
{
    private static ILogger _logger = null!;

    public static void ConfigureServices(IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger(nameof(PetBasePacketHandler));
    }

    public static bool HandlePacket(GatewayConnection connection, PacketReader reader)
    {
        if (!reader.TryRead(out byte opCode))
        {
            _logger.LogError("Failed to read opcode from packet. ( Data: {data} )", Convert.ToHexString(reader.Span));
            return false;
        }

        return opCode switch
        {
            PacketPetSpawn.OpCode => PacketPetActivateHandler.HandlePacketPet(connection, reader.Span),
            _ => false
        };
    }
}