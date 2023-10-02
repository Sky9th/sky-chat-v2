// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Respawn.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Sky9th.Protobuf {

  /// <summary>Holder for reflection information generated from Respawn.proto</summary>
  public static partial class RespawnReflection {

    #region Descriptor
    /// <summary>File descriptor for Respawn.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RespawnReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1SZXNwYXduLnByb3RvEg9Ta3k5dGguUHJvdG9idWYiZgoHUmVzcGF3bhIR",
            "CgluZXR3b3JrSUQYASACKAkSFQoEdHlwZRgCIAIoCToHUmVzcGF3bhIxCgty",
            "ZXNwYXduVHlwZRgDIAIoDjIcLlNreTl0aC5Qcm90b2J1Zi5SZXNwYXduVHlw",
            "ZSoZCgtSZXNwYXduVHlwZRIKCgZQbGF5ZXIQAUIsChpjb20uc2t5OXRoLmdh",
            "bWUuY2hhdC5wcm90b0IMUmVzcGF3blByb3RvUAE="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Sky9th.Protobuf.RespawnType), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Sky9th.Protobuf.Respawn), global::Sky9th.Protobuf.Respawn.Parser, new[]{ "NetworkID", "Type", "RespawnType" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum RespawnType {
    [pbr::OriginalName("Player")] Player = 1,
  }

  #endregion

  #region Messages
  public sealed partial class Respawn : pb::IMessage<Respawn>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Respawn> _parser = new pb::MessageParser<Respawn>(() => new Respawn());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Respawn> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Sky9th.Protobuf.RespawnReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Respawn() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Respawn(Respawn other) : this() {
      _hasBits0 = other._hasBits0;
      networkID_ = other.networkID_;
      type_ = other.type_;
      respawnType_ = other.respawnType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Respawn Clone() {
      return new Respawn(this);
    }

    /// <summary>Field number for the "networkID" field.</summary>
    public const int NetworkIDFieldNumber = 1;
    private readonly static string NetworkIDDefaultValue = "";

    private string networkID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string NetworkID {
      get { return networkID_ ?? NetworkIDDefaultValue; }
      set {
        networkID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "networkID" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasNetworkID {
      get { return networkID_ != null; }
    }
    /// <summary>Clears the value of the "networkID" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearNetworkID() {
      networkID_ = null;
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 2;
    private readonly static string TypeDefaultValue = global::System.Text.Encoding.UTF8.GetString(global::System.Convert.FromBase64String("UmVzcGF3bg=="), 0, 7);

    private string type_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Type {
      get { return type_ ?? TypeDefaultValue; }
      set {
        type_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "type" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasType {
      get { return type_ != null; }
    }
    /// <summary>Clears the value of the "type" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearType() {
      type_ = null;
    }

    /// <summary>Field number for the "respawnType" field.</summary>
    public const int RespawnTypeFieldNumber = 3;
    private readonly static global::Sky9th.Protobuf.RespawnType RespawnTypeDefaultValue = global::Sky9th.Protobuf.RespawnType.Player;

    private global::Sky9th.Protobuf.RespawnType respawnType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Sky9th.Protobuf.RespawnType RespawnType {
      get { if ((_hasBits0 & 1) != 0) { return respawnType_; } else { return RespawnTypeDefaultValue; } }
      set {
        _hasBits0 |= 1;
        respawnType_ = value;
      }
    }
    /// <summary>Gets whether the "respawnType" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasRespawnType {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "respawnType" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearRespawnType() {
      _hasBits0 &= ~1;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Respawn);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Respawn other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (NetworkID != other.NetworkID) return false;
      if (Type != other.Type) return false;
      if (RespawnType != other.RespawnType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasNetworkID) hash ^= NetworkID.GetHashCode();
      if (HasType) hash ^= Type.GetHashCode();
      if (HasRespawnType) hash ^= RespawnType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasNetworkID) {
        output.WriteRawTag(10);
        output.WriteString(NetworkID);
      }
      if (HasType) {
        output.WriteRawTag(18);
        output.WriteString(Type);
      }
      if (HasRespawnType) {
        output.WriteRawTag(24);
        output.WriteEnum((int) RespawnType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasNetworkID) {
        output.WriteRawTag(10);
        output.WriteString(NetworkID);
      }
      if (HasType) {
        output.WriteRawTag(18);
        output.WriteString(Type);
      }
      if (HasRespawnType) {
        output.WriteRawTag(24);
        output.WriteEnum((int) RespawnType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasNetworkID) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(NetworkID);
      }
      if (HasType) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Type);
      }
      if (HasRespawnType) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) RespawnType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Respawn other) {
      if (other == null) {
        return;
      }
      if (other.HasNetworkID) {
        NetworkID = other.NetworkID;
      }
      if (other.HasType) {
        Type = other.Type;
      }
      if (other.HasRespawnType) {
        RespawnType = other.RespawnType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            NetworkID = input.ReadString();
            break;
          }
          case 18: {
            Type = input.ReadString();
            break;
          }
          case 24: {
            RespawnType = (global::Sky9th.Protobuf.RespawnType) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            NetworkID = input.ReadString();
            break;
          }
          case 18: {
            Type = input.ReadString();
            break;
          }
          case 24: {
            RespawnType = (global::Sky9th.Protobuf.RespawnType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
