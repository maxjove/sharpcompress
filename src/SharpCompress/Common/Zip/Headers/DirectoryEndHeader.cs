using System.IO;

namespace SharpCompress.Common.Zip.Headers;

internal class DirectoryEndHeader : ZipHeader
{
    public DirectoryEndHeader() : base(ZipHeaderType.DirectoryEnd) { }

    internal override void Read(BinaryReader reader)
    {
        VolumeNumber = reader.ReadUInt16();
        FirstVolumeWithDirectory = reader.ReadUInt16();
        TotalNumberOfEntriesInDisk = reader.ReadUInt16();
        TotalNumberOfEntries = reader.ReadUInt16();
        DirectorySize = reader.ReadUInt32();
        DirectoryStartOffsetRelativeToDisk = reader.ReadUInt32();
        CommentLength = reader.ReadUInt16();
        Comment = reader.ReadBytes(CommentLength);
    }

    public ushort VolumeNumber { get; private set; }

    public ushort FirstVolumeWithDirectory { get; private set; }

    public ushort TotalNumberOfEntriesInDisk { get; private set; }

    public uint DirectorySize { get; private set; }

    public uint DirectoryStartOffsetRelativeToDisk { get; private set; }

    public ushort CommentLength { get; private set; }

    public byte[]? Comment { get; private set; }

    public ushort TotalNumberOfEntries { get; private set; }

    public bool IsZip64 =>
        TotalNumberOfEntriesInDisk == ushort.MaxValue
        || DirectorySize == uint.MaxValue
        || DirectoryStartOffsetRelativeToDisk == uint.MaxValue;
}
