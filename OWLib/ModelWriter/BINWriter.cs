﻿using System;
using System.Collections.Generic;
using System.IO;
using OWLib.Types;

namespace OWLib.ModelWriter {
  public class BINWriter : IModelWriter {
    private static void WriteString(BinaryWriter stream, string str) {
      stream.Write(str);
    }

    public static readonly float Rad2Deg = 360.0f / (float)(Math.PI * 2f);
    
    public string Name => "XNALara XPS Binary";
    public string Format => ".mesh.bin";
    public char[] Identifier => new char[2] { 'L', 'b' };
    public ModelWriterSupport SupportLevel => (ModelWriterSupport.VERTEX | ModelWriterSupport.UV | ModelWriterSupport.BONE | ModelWriterSupport.POSE | ModelWriterSupport.MATERIAL);

    public static OpenTK.Vector3 NormalizeAngles(OpenTK.Vector3 angles) {
      angles.X = NormalizeAngle(angles.X);
      angles.Y = NormalizeAngle(angles.Y);
      angles.Z = NormalizeAngle(angles.Z);
      return angles;
    }

    public static float NormalizeAngle(float angle) {
      while(angle > 360) {
        angle -= 360;
      }
      while(angle < 0) {
        angle += 360;
      }
      return angle;
    }

    public static OpenTK.Vector3 QuaternionToVector(OpenTK.Quaternion q1) {
      float sqw = q1.W * q1.W;
      float sqx = q1.X * q1.X;
      float sqy = q1.Y * q1.Y;
      float sqz = q1.Z * q1.Z;
      float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
      float test = q1.X * q1.W - q1.Y * q1.Z;
      OpenTK.Vector3 v = new OpenTK.Vector3();

      if(test > 0.4995f * unit) { // singularity at north pole
        v.Y = 2f * (float)Math.Atan2(q1.Y, q1.X);
        v.X = (float)Math.PI / 2;
        v.Z = 0;
        return NormalizeAngles(v * Rad2Deg);
      }
      if(test < -0.4995f * unit) { // singularity at south pole
        v.Y = -2f * (float)Math.Atan2(q1.Y, q1.X);
        v.X = (float)Math.PI / 2;
        v.Z = 0;
        return NormalizeAngles(v * Rad2Deg);
      }
      OpenTK.Quaternion q = new OpenTK.Quaternion(q1.W, q1.Z, q1.X, q1.Y);
      v.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W));       // Yaw
      v.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                                             // Pitch
      v.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z));       // Roll
      return NormalizeAngles(v * Rad2Deg);
    }
    
    public void Write(Model model, Stream stream, List<byte> LODs, Dictionary<ulong, List<ImageLayer>> layers, object[] opts) {
      Console.Out.WriteLine("Writing BIN");
      using(BinaryWriter writer = new BinaryWriter(stream)) {
        writer.Write((uint)323232);
        writer.Write((ushort)2);
        writer.Write((ushort)15);
        WriteString(writer, "XNAaraL");
        writer.Write((uint)5);
        WriteString(writer, "OVERWATCH");
        WriteString(writer, "BLIZZARD");
        WriteString(writer, "NULL");
        writer.Write((uint)180); // hash
        writer.Write((uint)1); // items
        // item 1
        writer.Write((uint)1); // type; 1 = pose; 2 = flags; 255 = padding
        if(model.BoneData.Length == 0) {
          writer.Write((uint)0); // size pow 4
          writer.Write((uint)0); // op info; bone count
        } else {
          using(MemoryStream ms = new MemoryStream()) {
            using(StreamWriter poseWriter = new StreamWriter(ms, System.Text.Encoding.ASCII, 4096, true)) {
              for(int i = 0; i < model.BoneData.Length; ++i) {
                OpenTK.Matrix3x4 data = model.PoseData[i];
                OpenTK.Quaternion q = new OpenTK.Quaternion(data.Row0.Xyz, data.Row0.W);
                OpenTK.Vector3 rot = QuaternionToVector(q);
                OpenTK.Vector3 scale = data.Row1.Xyz;
                OpenTK.Vector3 pos = new OpenTK.Vector3(0, 0, 0); // data.Row2.Xyz
                poseWriter.Write(string.Format("bone{0:X}:{1} {2} {3} {4} {5} {6} {7} {8} {9}\n", model.BoneIDs[i], rot.X, rot.Y, rot.Z, pos.X, pos.Y, pos.Z, scale.X, scale.Y, scale.Z));
              }
            }

            writer.Write((uint)ms.Length);
            writer.Write((uint)model.BoneData.Length);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            writer.Write(bytes);
            long n = 4 - (ms.Length % 4);
            if(n < 4) {
              for(long i = 0; i < n; ++i) {
                writer.Write((byte)0);
              }
            }
          }
        }
        /*
        pose data is always ASCII.
        Each line is:
        for each bone:
          boneName:rotx roty rotz posx posy posz scalex scaley scalez
        */

        writer.Write((uint)model.BoneData.Length);
        
        for(int i = 0; i < model.BoneData.Length; ++i) {
          WriteString(writer, string.Format("bone{0:X}", model.BoneIDs[i]));
          short parent = model.BoneHierarchy[i];
          if(parent == -1) {
            parent = (short)i;
          }
          writer.Write(parent);
          OpenTK.Vector3 bonePos = model.BoneData[i].ExtractTranslation();
          writer.Write(bonePos.X);
          writer.Write(bonePos.Y);
          writer.Write(bonePos.Z);
        }
        
        Dictionary<byte, List<int>> LODMap = new Dictionary<byte, List<int>>();
        uint sz = 0;
        for(int i = 0; i < model.Submeshes.Length; ++i) {
          ModelSubmesh submesh = model.Submeshes[i];
          if(LODs != null && !LODs.Contains(submesh.lod)) {
            continue;
          }
          if(!LODMap.ContainsKey(submesh.lod)) {
            LODMap.Add(submesh.lod, new List<int>());
          }
          sz++;
          LODMap[submesh.lod].Add(i);
        }
        writer.Write(sz);
        foreach(KeyValuePair<byte, List<int>> kv in LODMap) {
          Console.Out.WriteLine("Writing LOD {0}", kv.Key);
          foreach(int i in kv.Value) {
            ModelSubmesh submesh = model.Submeshes[i];
            ModelVertex[] vertex = model.Vertices[i];
            ModelVertex[] normal = model.Normals[i];
            ModelUV[][] uv = model.UVs[i];
            ModelIndice[] index = model.Faces[i];
            ModelBoneData[] bones = model.Bones[i];
            WriteString(writer, string.Format("Submesh_{0}.{1}.{2:X16}", i, kv.Key, model.MaterialKeys[submesh.material]));
            writer.Write((uint)uv.Length);
            ulong materialKey = model.MaterialKeys[submesh.material];
            if(layers.ContainsKey(materialKey)) {
              List<ImageLayer> materialLayers = layers[materialKey];
              writer.Write((uint)materialLayers.Count);
              for(int j = 0; j < materialLayers.Count; ++j) {
                writer.Write(string.Format("{0:X12}.dds", APM.keyToIndexID(materialLayers[j].key)));
                uint layer = layers[materialKey][j].layer;
                if(layer == 0) {
                  layer = 1;
                }
                layer = (uint)uv.Length - layers[materialKey][j].layer;
                layer = layer % (uint)uv.Length;
                writer.Write(layer);
              }
            } else {
              writer.Write((uint)uv.Length);
              for(int j = 0; j < uv.Length; ++j) {
                writer.Write(string.Format("{0:X16}_UV{1}.dds", materialKey, j));
                writer.Write((uint)j);
              }
            }
            
            writer.Write((uint)vertex.Length);
            for(int j = 0; j < vertex.Length; ++j) {
              writer.Write(vertex[j].x);
              writer.Write(vertex[j].y);
              writer.Write(vertex[j].z);
              writer.Write(-normal[j].x);
              writer.Write(-normal[j].y);
              writer.Write(-normal[j].z);
              writer.Write((byte)255);
              writer.Write((byte)255);
              writer.Write((byte)255);
              writer.Write((byte)255);
              for(int k = 0; k < uv.Length; ++k) {
                writer.Write((float)uv[k][j].u);
                writer.Write((float)uv[k][j].v);
              }
              if(model.BoneData.Length > 0) {
                writer.Write(model.BoneLookup[bones[j].boneIndex[0]]);
                writer.Write(model.BoneLookup[bones[j].boneIndex[1]]);
                writer.Write(model.BoneLookup[bones[j].boneIndex[2]]);
                writer.Write(model.BoneLookup[bones[j].boneIndex[3]]);
                writer.Write(bones[j].boneWeight[0]);
                writer.Write(bones[j].boneWeight[1]);
                writer.Write(bones[j].boneWeight[2]);
                writer.Write(bones[j].boneWeight[3]);
              }
            }
            writer.Write((uint)index.Length);
            for(int j = 0; j < index.Length; ++j) {
              writer.Write((uint)index[j].v1);
              writer.Write((uint)index[j].v2);
              writer.Write((uint)index[j].v3);
            }
          }
        }
      }
    }
    
    public Stream Write(Model model, List<byte> LODs, Dictionary<ulong, List<ImageLayer>> layers, object[] flags) {
      MemoryStream stream = new MemoryStream();
      Write(model, stream, LODs, layers, flags);
      return stream;
    }
  }
}
