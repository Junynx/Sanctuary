﻿using System;
using System.Numerics;

using Sanctuary.Core.IO;

namespace Sanctuary.Packet;

public struct PlayerUpdatePacketJump : ISerializablePacket, IDeserializable<PlayerUpdatePacketJump>
{
    public const short OpCode = 164;

    public ulong Guid;

    public Vector4 Position;
    public Quaternion Rotation;

    public byte State;
    public byte Unknown;

    public float VerticalVelocity;

    public byte[] Serialize()
    {
        using var writer = new PacketWriter();

        writer.Write(OpCode);

        writer.Write(Guid);

        writer.Write(Position, true);
        writer.Write(Rotation, true);

        writer.Write(State);
        writer.Write(Unknown);

        writer.Write(VerticalVelocity);

        return writer.Buffer;
    }

    public static bool TryDeserialize(ReadOnlySpan<byte> data, out PlayerUpdatePacketJump value)
    {
        value = default;

        var reader = new PacketReader(data);

        if (!reader.TryRead(out short opCode) && opCode != OpCode)
            return false;

        if (!reader.TryRead(out value.Guid))
            return false;

        if (!reader.TryRead(out value.Position, true))
            return false;

        if (!reader.TryRead(out value.Rotation, true))
            return false;

        if (!reader.TryRead(out value.State))
            return false;

        if (!reader.TryRead(out value.Unknown))
            return false;

        if (!reader.TryRead(out value.VerticalVelocity))
            return false;

        return reader.RemainingLength == 0;
    }

    public override string ToString()
    {
        return $"{nameof(Guid)}: {Guid}, {nameof(Position)}: {Position}, {nameof(Rotation)}: {Rotation}, {nameof(State)}: {State}, {nameof(Unknown)}: {Unknown}, {nameof(VerticalVelocity)}: {VerticalVelocity}";
    }
}